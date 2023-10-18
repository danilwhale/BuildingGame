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

Player player = new Player(new Vector2(0), 50, 0.1f);
World world = new World(256, 256);

for (int x = 0; x < world.Width; x++)
{
    for (int y = 0; y < world.Height; y++)
    {
        world[x, y] = (TileKind)Random.Shared.Next(41);
    }
}

while (!WindowShouldClose())
{
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