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

while (!WindowShouldClose())
{
    player.Update();
    
    BeginDrawing();
    ClearBackground(SKYBLUE);
    
    BeginMode2D(player.Camera);
    {
        DrawRectangle(0, 0, 128, 128, RED);
    }
    EndMode2D();
    
    EndDrawing();
}

CloseWindow();