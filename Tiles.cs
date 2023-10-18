namespace BuildingGame;

public static class Tiles
{
    private static Dictionary<TileKind, Tile> _tiles = new Dictionary<TileKind, Tile>()
    {
        { TileKind.Dirt, new Tile() }
    };

    public static Tile GetTile(TileKind kind)
    {
        if (kind == TileKind.Air) throw new ArgumentException("TileKind could not be an Air", nameof(kind));
        return _tiles[kind];
    }
}