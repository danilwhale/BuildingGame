namespace BuildingGame.Tiles;

public class Tile
{
    internal static Tile[] DefaultTiles = GenerateTiles();
    internal static readonly Texture2D Unknown = LoadTextureFromImage(GenImageChecked(48, 48, 24, 24, Color.MAGENTA, Color.BLACK));

    public static Tile GetTile(byte type)
    {
        if (type - 1 > DefaultTiles.Length - 1)
        {
            return new Tile(Vector2.Zero, "unknown", "?") { IsUnknown = true };
        }
        return DefaultTiles[type - 1];
    }
    public static Tile GetTile(string id) => DefaultTiles.First(t => t.Id.ToLower() == id.ToLower());
    public static byte GetNId(string id) => (byte)((byte)Array.IndexOf(DefaultTiles, GetTile(id)) + 1);
    public static string GetId(byte id) => GetTile(id).Id;

    public Vector2 AtlasOffset { get; }
    public Vector2 Size { get; }
    public string Id { get; }
    public string DisplayName { get; }
    public bool IsUnknown { get; set; }

    public Tile(Vector2 atlasOffset, string id, string displayName)
    {
        AtlasOffset = atlasOffset;
        Size = new Vector2(1, 1);
        Id = id;
        DisplayName = displayName;
    }

    public Tile(Vector2 atlasOffset, Vector2 size, string id, string displayName)
    {
        AtlasOffset = atlasOffset;
        Size = size;
        Id = id;
        DisplayName = displayName;
    }

    public void Draw(int x, int y, TileFlags flags)
    {
        Draw(x, y, flags, Color.WHITE);
    }

    public void Draw(int x, int y, TileFlags flags, Color tint)
    {
        if (IsUnknown)
        {
            DrawTexturePro(
                Unknown,
                new Rectangle(0.25f, 0.25f, 48 - 0.25f, 48 - 0.25f),
                new Rectangle(x * 48, y * 48, Size.X * 48, Size.Y * 48),
                new Vector2(24, 24),
                flags.Rotation,
                tint
            );
            return;
        }
        DrawTexturePro(
            Program.atlas,
            new Rectangle(AtlasOffset.X * 16 + 0.25f, AtlasOffset.Y * 16 + 0.25f, Size.X * 16 - 0.25f, Size.Y * 16 - 0.25f),
            new Rectangle(x * 48, y * 48, Size.X * 48, Size.Y * 48),
            new Vector2(24, 24),
            flags.Rotation,
            tint
        );
    }

    internal static Tile[] GenerateTiles(string atlasText = "assets/atlas.txt")
    {
        List<Tile> tiles = new List<Tile>();
        var ids = ConfigParser.ParseFourth(atlasText);

        for (int i = 0; i < ids.Count; i++)
        {
            var id = ids.Keys.ToArray()[i];
            var kv = ids[id];
            var coords = ConfigParser.ToVector2i(kv.left);
            var size = ConfigParser.ToVector2i(kv.middle);
            var dn = kv.right;

            tiles.Add(new Tile(coords, size, id, dn));
        }

        return tiles.ToArray();
    }

    public static implicit operator Tile(string id) => Tile.GetTile(id);
    public static implicit operator Tile(byte nid) => Tile.GetTile(nid);
}