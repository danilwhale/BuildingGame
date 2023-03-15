using BuildingGame.Tiles;

namespace BuildingGame.TilePacks;

public static class TilePackManager
{
    public static List<TilePack> TilePacks { get; } = new List<TilePack>();
    public static event Action? PackChanged;

    public static void LoadPacks(string root = "packs")
    {
        TilePacks.Clear();
        
        if (!Directory.Exists(root))
        {
            Directory.CreateDirectory(root);
            return;
        }

        foreach (var packFolder in Directory.GetDirectories(root).Where(s => TilePack.Check(s)))
        {
            var pack = new TilePack(packFolder);
            Log.Information(pack.ToString());
            TilePacks.Add(pack);
        }
    }

    public static void SetDefaultPack()
    {
        Program.atlas = Program.origAtlas;
        Tile.DefaultTiles = Tile.GenerateTiles();
        PackChanged?.Invoke();
    }

    public static void ApplyPack(string name)
    {
        var pack = TilePacks.Find(m => m.Name == name);
        if (pack.Equals(default(TilePack))) return;
        pack.Apply();
        PackChanged?.Invoke();
    }

    internal static void UnloadPacks()
    {
        foreach (var pack in TilePacks) pack.Unload();
    }
}