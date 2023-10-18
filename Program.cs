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
TileKind[] tiles = Enum.GetValues<TileKind>();

while (!WindowShouldClose())
{
    player.Update();
    
    BeginDrawing();
    ClearBackground(SKYBLUE);
    
    BeginMode2D(player.Camera);
    {
        for (int i = 1; i < tiles.Length; i++)
        {
            Tiles.GetTile(tiles[i]).Draw(world, i, 0);
        }
    }
    EndMode2D();
    
    EndDrawing();
}

Resources.Free();
CloseWindow();