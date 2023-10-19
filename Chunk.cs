namespace BuildingGame;

public struct Chunk
{
    public const int Size = 16;
    public const float ViewSize = Size * Tile.RealTileSize;

    public World World;
    public readonly int X, Y;

    private TileKind[][] _tiles;

    public Chunk(World world, int x, int y)
    {
        World = world;
        X = x;
        Y = y;

        _tiles = new TileKind[Size][];
        for (int i = 0; i < Size; i++)
        {
            _tiles[i] = new TileKind[Size];
        }
    }

    public void Update()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                TileKind tile = _tiles[x][y];
                if (tile == TileKind.Air) continue;
                Tiles.GetTile(tile).Update(World, X * Size + x, Y * Size + y);
            }
        }
    }

    public void Draw()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                TileKind tile = _tiles[x][y];
                if (tile == TileKind.Air) continue;
                Tiles.GetTile(tile).Draw(World, X * Size + x, Y * Size + y);
            }
        }
    }

    public TileKind this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size)
                return TileKind.Air;
            return _tiles[x][y];
        }
        set
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size)
                return;
            _tiles[x][y] = value;
        }
    }
}