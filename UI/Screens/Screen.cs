using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class Screen : IDisposable
{
    public static readonly ElementComparer Comparer = new ElementComparer();
    
    public bool IsCurrent
    {
        get => ReferenceEquals(this, ScreenManager.CurrentScreen);
        set => ScreenManager.CurrentScreen = this;
    }
    
    public List<Element> Elements = new List<Element>();
    public IOrderedEnumerable<Element> ElementsSorted => Elements.OrderBy(e => e, Comparer);
    
    public List<UIInterface> Interfaces = new List<UIInterface>();
    
    public virtual void Initialize()
    {
        
    }

    public virtual void Update()
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            // handle list modifying
            if (i >= Elements.Count) break;

            var element = Elements[i];
            if (!element.Active) continue;

            element.Update();
        }

        for (int i = 0; i < Interfaces.Count; i++)
        {
            // handle list modifying
            if (i >= Interfaces.Count) break;
            
            Interfaces[i].Update();
        }
    }

    public virtual void Draw()
    {
        foreach (var el in ElementsSorted)
        {
            if (!el.Active || !el.Visible) continue;
            el.Draw();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        
    }

    public void Dispose()
    {
        for (int i = 0; i < Elements.Count; i++)
        {
            // handle list modifying
            if (i >= Elements.Count) break;

            Elements[i].Dispose();
        }
        
        for (int i = 0; i < Interfaces.Count; i++)
        {
            // handle list modifying
            if (i >= Interfaces.Count) break;
            
            Interfaces[i].Destroy();
        }
        
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}