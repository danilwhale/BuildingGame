using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public static class ScreenManager
{
    public static Screen? CurrentScreen;

    public static void Switch(Screen newScreen)
    {
        UIInterfaceManager.Destroy();
        Free();
        
        newScreen.IsCurrent = true;
        Initialize();
        UIInterfaceManager.Initialize();
    }

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