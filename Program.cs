global using static ZeroElectric.Vinculum.Raylib;
global using ZeroElectric.Vinculum;
using System.Numerics;
using BuildingGame;

SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
InitWindow(1024, 768, "building game");

// set window icon
Image icon = LoadImage("Assets/Icon.png");
SetWindowIcon(icon);
UnloadImage(icon);

World world = new World(256, 256);
Player player = new Player(world, new Vector2(0), 50, 0.1f);

while (!WindowShouldClose())
{
    if (IsKeyPressed(KeyboardKey.KEY_R))
        Tiles.Reload();
    
    player.Update();
    world.Update();
    
    BeginDrawing();
    ClearBackground(SKYBLUE);
    
    BeginMode2D(player.Camera);
    {
        world.Draw(player);
    }
    EndMode2D();
    
    EndDrawing();
}

Resources.Free();
CloseWindow();