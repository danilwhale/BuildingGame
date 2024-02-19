using System.Numerics;

namespace BuildingGame.UI.Interfaces;

public class UIInterface
{
    private bool _active = true;
    private bool _visible = true;

    protected List<Element> Elements = new();
    protected bool IgnorePause;
    protected Vector2 Position;

    public UIInterface()
    {
        UIInterfaceManager.Add(this);
    }

    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
            foreach (var el in Elements) el.Visible = value;
        }
    }

    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            foreach (var el in Elements) el.Active = value;
        }
    }

    public bool CanIgnorePause => IgnorePause;

    public virtual void Initialize()
    {
    }

    public virtual void Configure()
    {
    }

    public virtual void Update()
    {
        if (IsWindowResized()) Resized();
        if (!_active) return;
    }

    public virtual void Resized()
    {
    }

    public virtual void Destroy()
    {
        DestroyElements();
    }

    protected void DestroyElements()
    {
        foreach (var el in Elements) GuiManager.Remove(el);

        Elements.Clear();
    }
}