namespace BuildingGame.UI.Screens;

public static class ScreenManager
{
    public static Screen? CurrentScreen;

    public static void Initialize()
    {
        CurrentScreen?.Initialize();
    }

    public static void Update()
    {
        CurrentScreen?.Update();
    }

    public static void Draw()
    {
        CurrentScreen?.Draw();
    }

    public static void Free()
    {
        CurrentScreen?.Dispose();
        CurrentScreen = null;
    }

}