global using System.Numerics;
global using Raylib_cs;
global using static Raylib_cs.Raylib;
global using static Raylib_cs.Raymath;

namespace BuildingGame;

internal class Program
{
    public const int WIDTH = 800;
    public const int HEIGHT = 600;


    public static Texture2D atlas;
    public static Texture2D check;

    public static bool mustClose = false;

    public static MenuScreen menuScreen = null!;
    public static GameScreen gameScreen = null!;
    public static WorldSelectScreen worldSelectScreen = null!;
    public static Screen currentScreen = null!;

    private static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, ev) =>
        {
            File.WriteAllText("crash.txt", ev.ExceptionObject.ToString());
        };
        InitWindow(WIDTH, HEIGHT, "BuildingGame");
        SetTargetFPS(60);
        SetExitKey(KeyboardKey.KEY_NULL);

        Gui.SetGuiFont("assets/font.ttf");

        BeginDrawing();
        ClearBackground(new Color(20, 20, 20, 255));

        DrawTextEx(Gui.GuiFont, "Loading assets...", new Vector2(16, HEIGHT - 18 - 16), 18, 1, Color.WHITE);

        EndDrawing();

        atlas = LoadTexture("assets/atlas.png");
        check = LoadTexture("assets/check.png");

        Image icon = LoadImage("assets/icon.png");
        SetWindowIcon(icon);

        Settings.Load();

        gameScreen = new GameScreen();
        gameScreen.Initialize();

        menuScreen = new MenuScreen();
        menuScreen.Initialize();

        worldSelectScreen = new WorldSelectScreen();
        worldSelectScreen.Initialize();

        currentScreen = menuScreen;

        while (!WindowShouldClose())
        {
            // SetWindowTitle("BuldingGame - " + GetFPS() + " FPS");
            if (mustClose) break;

            currentScreen.Update();

            Gui.Update();

            BeginDrawing();

            currentScreen.Draw();

            Gui.Draw();

            EndDrawing();
        }

        gameScreen.World.Save();
        Settings.Save();

        UnloadImage(icon);
        UnloadTexture(check);
        UnloadTexture(atlas);
        CloseWindow();
    }


}