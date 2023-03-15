namespace BuildingGame.TilePacks;

public struct TilePack
{
    public string Root { get; } = string.Empty;
    public string Name { get; } = string.Empty;
    public bool IsVanilla { get; } = true;

    public string AtlasPath { get; } = string.Empty;
    public string TileAtlasPath { get; } = string.Empty;

    internal Texture2D Atlas { get; set; }

    public TilePack(string path)
    {
        Atlas = Program.atlas;
        try
        {
            Root = path;

            // try to get some data from pack
            if (File.Exists(Path.Combine(path, "info.txt")))
            {
                // parse info file
                var data = ConfigParser.Parse(Path.Combine(path, "info.txt"));

                // get pack name
                if (data.TryGetValue("name", out var name)) Name = name;
                // if pack is vanilla, it won't use custom atlas.txt
                if (data.TryGetValue("isVanilla", out var isVanillaStr) && 
                    bool.TryParse(isVanillaStr, out var isVanilla)) IsVanilla = isVanilla;
            }
            else
            {
                Console.WriteLine("invalid pack. check if pack is valid using TilePack.Check(string) before creating pack");
                return;
            }
            // try get atlas
            if (File.Exists(Path.Combine(path, "atlas.png")))
            {
                AtlasPath = Path.Combine(path, "atlas.png");
                Atlas = LoadTexture(AtlasPath);
            }
            // try get tile atlas or ignore if pack is vanilla
            if (File.Exists(Path.Combine(path, "atlas.txt")) && !IsVanilla)
                TileAtlasPath = Path.Combine(path, "atlas.txt");
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }

    public static bool Check(string path)
    {
        return File.Exists(Path.Combine(path, "info.txt"));
    }

    public void Apply()
    {
        if (AtlasPath != string.Empty)
        {
            Program.atlas = Atlas;
        }
        if (TileAtlasPath != string.Empty && !IsVanilla)
        {
            Tile.DefaultTiles = Tile.GenerateTiles(TileAtlasPath);
        }
    }

    public void Unload()
    {
        UnloadTexture(Atlas);
    }
}