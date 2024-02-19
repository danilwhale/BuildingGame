namespace BuildingGame.UI.Interfaces;

public static class UIInterfaceManager
{
    private static readonly List<UIInterface> _interfaces = new();

    public static int Add(UIInterface uiInterface)
    {
        _interfaces.Add(uiInterface);
        return _interfaces.Count - 1;
    }

    public static void Remove(int id)
    {
        _interfaces.RemoveAt(id);
    }

    public static void Initialize()
    {
        foreach (var i in _interfaces) i.Initialize();
    }

    public static void Update()
    {
        foreach (var i in _interfaces)
        {
            if (!i.CanIgnorePause && Program.Paused) continue;
            i.Update();
        }
    }

    public static void Destroy()
    {
        foreach (var i in _interfaces) i.Destroy();
        _interfaces.Clear();
    }
}