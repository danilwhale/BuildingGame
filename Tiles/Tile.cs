namespace BuildingGame.Tiles;

public class Tile
{
    public static Tile[] DefaultTiles = GenerateTiles();

    public static Tile GetTile(byte type) => DefaultTiles[type - 1];
    public static Tile GetTile(string id) => DefaultTiles.First(t => t.Id.ToLower() == id.ToLower());
    public static byte GetNId(string id) => (byte)((byte)Array.IndexOf(DefaultTiles, GetTile(id)) + 1);
    public static string GetId(byte id) => GetTile(id).Id;

    public Vector2 AtlasOffset { get; }
    public Vector2 Size { get; }
    public string Id { get; }
    public string DisplayName { get; }

    public Tile(Vector2 atlasOffset, string id, string dn)
    {
        AtlasOffset = atlasOffset;
        Size = new Vector2(1, 1);
        Id = id;
        DisplayName = dn;
    }

    public Tile(Vector2 atlasOffset, Vector2 size, string id, string dn)
    {
        AtlasOffset = atlasOffset;
        Size = size;
        Id = id;
        DisplayName = dn;
    }

    public void Draw(int x, int y, TileFlags flags)
    {
        Draw(x, y, flags, Color.WHITE);
    }

    public void Draw(int x, int y, TileFlags flags, Color tint)
    {
        Raylib.DrawTexturePro(
            Program.atlas,
            new Rectangle(AtlasOffset.X * 16, AtlasOffset.Y * 16, Size.X * 16, Size.Y * 16),
            new Rectangle(x * 48, y * 48, Size.X * 48, Size.Y * 48),
            new Vector2(24, 24),
            flags.Rotation,
            tint
        );
    }

    public static Tile[] GenerateTiles(string atlasText = "assets/atlas.txt")
    {
        List<Tile> tiles = new List<Tile>();
        var ids = ConfigParser.ParseTriple(atlasText);
        
        for (int i = 0; i < ids.Count; i++)
        {
            var id = ids.Keys.ToArray()[i];
            var kv = ids[id];
            var coords = ConfigParser.ToVector2i(kv.m);
            var dn = kv.r;

            tiles.Add(new Tile(coords, id, dn));
        }

        return tiles.ToArray();
    }

    public static implicit operator Tile(string id) => Tile.GetTile(id);
    public static implicit operator Tile(byte nid) => Tile.GetTile(nid);
}