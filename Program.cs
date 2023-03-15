global using System.Numerics;
global using BuildingGame.GuiElements;
global using BuildingGame.GuiElements.Brushes;
global using BuildingGame.Screens;
global using BuildingGame.TilePacks;
global using BuildingGame.Tiles;
global using Raylib_cs;
global using static Raylib_cs.Raylib;
global using static Raylib_cs.Raymath;

namespace BuildingGame;

#nullable disable
internal class Program
{
    public static int WIDTH = 800;
    static int preWidth = 800;
    public static int HEIGHT = 600;
    static int preHeight = 600;

    public static Texture2D origAtlas;
    public static Texture2D atlas;
    public static Texture2D check;

    public static bool mustClose = false;

    public static MenuScreen menuScreen;
    public static GameScreen gameScreen;
    public static WorldSelectScreen worldSelectScreen;
    public static CreateWorldScreen createWorldScreen;
    public static Screen currentScreen;
    public static SelectPackScreen selectPackScreen;



    private static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, ev) =>
        {
            File.WriteAllText("crash.txt", ev.ExceptionObject.ToString());
        };
        InitWindow(WIDTH, HEIGHT, "BuildingGame");
        SetExitKey(KeyboardKey.KEY_NULL);
        SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        SetWindowState(ConfigFlags.FLAG_VSYNC_HINT);
        SetWindowMinSize(800, 600);

        Gui.SetGuiFont("assets/font.ttf");

        BeginDrawing();
        ClearBackground(new Color(20, 20, 20, 255));

        DrawTextEx(Gui.GuiFont, "Loading assets...", new Vector2(16, HEIGHT - 18 - 16), 18, 1, Color.WHITE);

        EndDrawing();

        origAtlas = LoadTexture("assets/atlas.png");
        atlas = origAtlas;
        check = LoadTexture("assets/check.png");
        SetTextureFilter(atlas, TextureFilter.TEXTURE_FILTER_POINT);
        SetTextureFilter(check, TextureFilter.TEXTURE_FILTER_POINT);

        Image icon = LoadImage("assets/icon.png");
        SetWindowIcon(icon);

        Settings.Load();
        TilePackManager.LoadPacks();
        if (Settings.CurrentPack != "default")
            TilePackManager.ApplyPack(Settings.CurrentPack);
        else
            TilePackManager.SetDefaultPack();

        gameScreen = new GameScreen();
        gameScreen.Initialize();

        menuScreen = new MenuScreen();
        menuScreen.Initialize();

        worldSelectScreen = new WorldSelectScreen();
        worldSelectScreen.Initialize();

        createWorldScreen = new CreateWorldScreen();
        createWorldScreen.Initialize();

        selectPackScreen = new SelectPackScreen();
        selectPackScreen.Initialize();

        currentScreen = menuScreen;

        while (!WindowShouldClose())
        {
            // SetWindowTitle("BuldingGame - " + GetFPS() + " FPS");
            if (mustClose) break;
            if (IsKeyPressed(KeyboardKey.KEY_F11) ||
                (IsKeyPressed(KeyboardKey.KEY_LEFT_ALT) && IsKeyPressed(KeyboardKey.KEY_ENTER)))
            {
                if (IsWindowFullscreen())
                {
                    WIDTH = preWidth;
                    HEIGHT = preHeight;
                }
                else
                {
                    preWidth = WIDTH;
                    preHeight = HEIGHT;

                    WIDTH = GetMonitorWidth(GetCurrentMonitor());
                    HEIGHT = GetMonitorHeight(GetCurrentMonitor());
                }
                SetWindowSize(WIDTH, HEIGHT);

                ToggleFullscreen();

            }

            // take screenshot ONLY if player is in game right now
            if (IsKeyPressed(KeyboardKey.KEY_F2) && currentScreen == gameScreen) Screenshot.Create();
            // toggle gui only if player is in game right now
            if (IsKeyPressed(KeyboardKey.KEY_F1) && currentScreen == gameScreen)
            {
                Gui.ProcessGui = !Gui.ProcessGui;
                gameScreen.showPreTile = !gameScreen.showPreTile;
            }

            WIDTH = GetScreenWidth();
            HEIGHT = GetScreenHeight();
            if (IsWindowResized())
            {
                var bgPanel = (BackgroundBlock)Gui.GetControl("bgPanel");
                var tooltip = (Tooltip)Gui.GetControl("tileTooltip");
                // gameScreen.RecreateTileMenu(bgPanel, tooltip);
                gameScreen.camera.offset = new Vector2(Program.WIDTH / 2, Program.HEIGHT);
            }

            currentScreen.Update();

            Gui.Update();

            BeginDrawing();

            currentScreen.Draw();

            Gui.Draw();
            DrawTextEx(Gui.GuiFont, GetFPS().ToString(), Vector2.Zero, 18, 1, Color.LIME);

            EndDrawing();
        }

        gameScreen.World.Save();
        Settings.Save();

        UnloadImage(icon);
        UnloadTexture(check);
        UnloadTexture(origAtlas);
        TilePackManager.UnloadPacks();
        CloseWindow();
    }


}