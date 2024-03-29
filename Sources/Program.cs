﻿global using static Raylib_cs.Raylib;
global using Raylib_cs;
using System.Runtime.CompilerServices;
using BuildingGame;
using BuildingGame.Tiles;
using BuildingGame.Tiles.Dynamic;
using BuildingGame.Tiles.IO;
using BuildingGame.Tiles.Packs;
using BuildingGame.UI;
using BuildingGame.UI.Interfaces;
using BuildingGame.UI.Screens;

// remove this when raylib-cs will have rl 5.0 functions
[assembly: DisableRuntimeMarshalling]

internal class Program
{
    public static bool Running = true;
    public static bool Paused = false;

    public static void Main(string[] args)
    {
        SetConfigFlags(ConfigFlags.VSyncHint);
        SetConfigFlags(ConfigFlags.ResizableWindow);
        InitWindow(1024, 768, "building game");
        SetExitKey(KeyboardKey.Null);

        Initialize();
        
        while (Running && !WindowShouldClose())
        {
            Update();
            Draw();
        }

        Closing();
        CloseWindow();
    }

    public static void LoadWindowIcon()
    {
        var icon = Resources.GetImage("Icon.png");
        SetWindowIcon(icon);
    }

    private static void Initialize()
    {
        Settings.Load();

        TilePackManager.Load();

        var pack = TilePackManager.Find(Settings.CurrentTilePack);
        if (string.IsNullOrWhiteSpace(pack.Path)) pack = TilePackManager.Find("Default");

        TilePackManager.Apply(pack);

        BGWorld21Format.Register();
        BGWorld2Format.Register();
        LvlFormat.Register();

        WorldIO.AddDeserializerAsBackupable(new BGWorld2Format.Deserializer());
        WorldIO.AddDeserializerAsBackupable(new LvlFormat.Deserializer());

        Tiles.RegisterCustomTile("sand", new SandTile());
        Tiles.RegisterCustomTile("water", new WaterTile());
        Tiles.RegisterCustomTile("lava", new LavaTile());
        Tiles.RegisterCustomTile("infection_block", new InfectionTile());

        ScreenManager.CurrentScreen = new MenuScreen();

        ScreenManager.Initialize();
        UIInterfaceManager.Initialize();
    }

    private static void Update()
    {
        ScreenManager.Update();
        UIInterfaceManager.Update();
    }

    private static void Draw()
    {
        BeginDrawing();

        ScreenManager.Draw();

        EndDrawing();
    }

    private static void Closing()
    {
        ScreenManager.Free();
        UIInterfaceManager.Destroy();

        Settings.Save();

        Resources.Free();
    }
}