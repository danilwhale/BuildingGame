global using System.Numerics;
global using BuildingGame.GuiElements;
global using BuildingGame.GuiElements.Brushes;
global using BuildingGame.Screens;
global using BuildingGame.TilePacks;
global using BuildingGame.Tiles;
global using Raylib_cs;
global using Serilog;
global using static Raylib_cs.Raylib;
global using static Raylib_cs.Raymath;
using System.Diagnostics;

namespace BuildingGame;

#nullable disable
internal class Program
{
    internal static int WIDTH = 800;
    static int preWidth = 800;
    internal static int HEIGHT = 600;
    static int preHeight = 600;

    internal static Texture2D origAtlas;
    internal static Texture2D atlas;
    internal static Texture2D check;

    internal static bool mustClose = false;

    internal static MenuScreen menuScreen;
    internal static GameScreen gameScreen;
    internal static WorldSelectScreen worldSelectScreen;
    internal static CreateWorldScreen createWorldScreen;
    internal static Screen currentScreen;
    internal static SelectPackScreen selectPackScreen;

    internal static string version = "";

    private static bool _isFullScreen = false;

    private static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/latest.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();
        
        AppDomain.CurrentDomain.UnhandledException += (sender, ev) =>
        {
            Log.Fatal("something fatal happened on code side.\nexception: " + ev.ExceptionObject.ToString());
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

        Log.Information("loading assets...");

        var versionInfo = FileVersionInfo.GetVersionInfo(Path.Join(AppContext.BaseDirectory, "buildinggame.exe"));
        version = versionInfo.FileVersion;

        origAtlas = LoadTexture("assets/atlas.png");
        atlas = origAtlas;
        check = LoadTexture("assets/check.png");
        SetTextureFilter(atlas, TextureFilter.TEXTURE_FILTER_POINT);
        SetTextureFilter(check, TextureFilter.TEXTURE_FILTER_POINT);

        Image icon = LoadImage("assets/icon.png");
        SetWindowIcon(icon);

        Settings.Load();
        TilePackManager.LoadPacks();
        Log.Information("loaded packs and settings");
        if (Settings.CurrentPack != "default")
            TilePackManager.ApplyPack(Settings.CurrentPack);
        else
            TilePackManager.SetDefaultPack();
        Log.Information("toggled pack to 'currentPack' value in settings (" + Settings.CurrentPack + ")");

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

        Log.Information("initialized screens");
        Log.Information("going to the event loop");

        while (!WindowShouldClose())
        {
            // SetWindowTitle("BuldingGame - " + GetFPS() + " FPS");
            if (mustClose) break;
            if (IsKeyPressed(KeyboardKey.KEY_F11))
            {
                if (_isFullScreen)
                {
                    WIDTH = preWidth;
                    HEIGHT = preHeight;
                    ClearWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);
                    SetWindowPosition(GetMonitorWidth(GetCurrentMonitor()) / 2 - WIDTH / 2,
                                      GetMonitorHeight(GetCurrentMonitor()) / 2 - HEIGHT / 2);
                }
                else
                {
                    preWidth = WIDTH;
                    preHeight = HEIGHT;

                    WIDTH = GetMonitorWidth(GetCurrentMonitor());
                    HEIGHT = GetMonitorHeight(GetCurrentMonitor());

                    SetWindowPosition(0, 0);
                    SetWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);
                }
                SetWindowSize(WIDTH, HEIGHT);
                _isFullScreen = !_isFullScreen;



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

        Log.Information("saving world...");
        gameScreen.World.Save();
        Log.Information("saving settings...");
        Settings.Save();

        Log.Information("unloading textures...");
        UnloadImage(icon);
        UnloadTexture(check);
        UnloadTexture(origAtlas);
        TilePackManager.UnloadPacks();
        Log.Information("goodbye!");
        CloseWindow();
    }
}