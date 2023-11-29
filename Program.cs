global using static ZeroElectric.Vinculum.Raylib;
global using ZeroElectric.Vinculum;
using System.Numerics;
using BuildingGame;
using BuildingGame.Tiles;
using BuildingGame.Tiles.IO;
using BuildingGame.UI;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Interfaces;

internal class Program
{
    public static bool Paused = false;
    
    private static World _world;
    private static Player _player;
    private static BlockMenu _menu;
    private static GameHud _hud;
    private static PauseScreen _pause;

    public static void Main(string[] args)
    {
        SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        InitWindow(1024, 768, "building game");
        SetExitKey(KeyboardKey.KEY_NULL);

        // set window icon
        Image icon = LoadImage("Assets/Icon.png");
        SetWindowIcon(icon);
        UnloadImage(icon);

        Initialize();

        while (!WindowShouldClose())
        {
            Update();
            Draw();
        }

        Closing();
        CloseWindow();
    }

    private static void Initialize()
    {
        BGWorld21Format.Register();
        BGWorld2Format.Register();
        LvlFormat.Register();

        WorldIO.AddDeserializerAsBackupable(new BGWorld2Format.Deserializer());
        WorldIO.AddDeserializerAsBackupable(new LvlFormat.Deserializer());

        _world = new World();
        _world.Load();

        _player = new Player(_world, _world.PlayerPosition, _world.PlayerZoom, 50, 0.1f);

        _menu = new BlockMenu();
        _hud = new GameHud(_menu);
        _pause = new PauseScreen();

        UIInterfaceManager.Initialize();
    }

    private static void Update()
    {
        if (!Paused)
        {
            if (IsKeyPressed(KeyboardKey.KEY_R))
                Tiles.Reload();

            _player.Update();
            _world.Update();
        }
        
        GuiManager.Update();
        UIInterfaceManager.Update();
    }

    private static void Draw()
    {
        BeginDrawing();
        ClearBackground(SKYBLUE);

        BeginMode2D(_player.Camera);
        {
            _world.Draw(_player);
            _player.Draw();
        }
        EndMode2D();

        GuiManager.Draw();

        EndDrawing();
    }
    
    private static void Closing()
    {
        UIInterfaceManager.Destroy();

        _player.PushCameraInfo();
        _world.Save();

        Resources.Free();
    }
}