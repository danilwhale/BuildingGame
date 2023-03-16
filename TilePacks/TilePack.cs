namespace BuildingGame.TilePacks;

public struct TilePack
{
    public const byte PACK_FORMAT = 0;

    public string Root { get; } = string.Empty;
    public string Name { get; } = string.Empty;
    public byte Version { get; } = 0;
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
            if (Check(path))
            {
                // parse info file
                var data = ConfigParser.Parse(Path.Combine(path, "info.txt"));

                // get pack name
                if (data.TryGetValue("name", out var name)) Name = name;
                // if pack is vanilla, it won't use custom atlas.txt
                if (data.TryGetValue("isVanilla", out var isVanillaStr) && 
                    bool.TryParse(isVanillaStr, out var isVanilla)) IsVanilla = isVanilla;
                if (data.TryGetValue("supportedVersion", out var supportedVersionStr) &&
                    byte.TryParse(supportedVersionStr, out var supportedVersion))
                    Version = supportedVersion;
            }
            else
            {
                Log.Error("invalid pack. check if pack is valid using TilePack.Check(string) before creating pack");
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
        catch (Exception ex) { Log.Error(ex.ToString()); }
    }

    public static bool Check(string path)
    {
        return File.Exists(Path.Combine(path, "info.txt"));
    }

    public void Apply()
    {
        if (Version != PACK_FORMAT)
        {
            Log.Warning("\n|-------\n" + 
                        $"|> {Name} is not supported for this version.\n" +
                         "|> if you're sure, that it supports this version\n" + 
                         "|> try changing 'supportedVersion' value to " + PACK_FORMAT + 
                         "\n|-------");
            return;
        }
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

    public override string ToString()
    {
        return $"Name: {Name}; IsVanilla: {IsVanilla}; PackFormat: {Version}";
    }
}