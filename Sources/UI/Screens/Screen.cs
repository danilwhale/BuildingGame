using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class Screen : IDisposable
{
    public static readonly ElementComparer Comparer = new();

    public List<Element> Elements = new();

    public List<UIInterface> Interfaces = new();

    public bool IsCurrent
    {
        get => ReferenceEquals(this, ScreenManager.CurrentScreen);
        set => ScreenManager.CurrentScreen = this;
    }

    public IOrderedEnumerable<Element> ElementsSorted => Elements.OrderBy(e => e, Comparer);

    public void Dispose()
    {
        for (var i = 0; i < Elements.Count; i++)
        {
            // handle list modifying
            if (i >= Elements.Count) break;

            Elements[i].Dispose();
        }

        for (var i = 0; i < Interfaces.Count; i++)
        {
            // handle list modifying
            if (i >= Interfaces.Count) break;

            Interfaces[i].Destroy();
        }

        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Initialize()
    {
    }

    public virtual void Update()
    {
        for (var i = 0; i < Elements.Count; i++)
        {
            // handle list modifying
            if (i >= Elements.Count) break;

            var element = Elements[i];
            if (!element.Active) continue;

            element.Update();
        }

        for (var i = 0; i < Interfaces.Count; i++)
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
}