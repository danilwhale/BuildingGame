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

SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
InitWindow(1024, 768, "building game");

// set window icon
Image icon = LoadImage("Assets/Icon.png");
SetWindowIcon(icon);
UnloadImage(icon);

BGWorld21Format.Register();
BGWorld2Format.Register();
LvlFormat.Register();

WorldIO.AddDeserializerAsBackupable(new BGWorld2Format.Deserializer());
WorldIO.AddDeserializerAsBackupable(new LvlFormat.Deserializer());

World world = new World();
world.Load();   

Player player = new Player(world, world.PlayerPosition, world.PlayerZoom, 50, 0.1f);

BlockMenu menu = new BlockMenu();
GameHud hud = new GameHud(menu);

UIInterfaceManager.Initialize();

while (!WindowShouldClose())
{
    if (IsKeyPressed(KeyboardKey.KEY_R))
        Tiles.Reload();
    
    player.Update();
    world.Update();
    GuiManager.Update();
    UIInterfaceManager.Update();
    
    BeginDrawing();
    ClearBackground(SKYBLUE);
    
    BeginMode2D(player.Camera);
    {
        world.Draw(player);
    }
    EndMode2D();
    
    GuiManager.Draw();
    
    EndDrawing();
}

UIInterfaceManager.Destroy();

player.PushCameraInfo();
world.Save();

Resources.Free();
CloseWindow();