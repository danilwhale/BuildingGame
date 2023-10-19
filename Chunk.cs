namespace BuildingGame;

public struct Chunk
{
    public const int Size = 16;
    public const float ViewSize = Size * Tile.RealTileSize;

    public World World;
    public readonly int X, Y;

    private byte[][] _tiles;

    public Chunk(World world, int x, int y)
    {
        World = world;
        X = x;
        Y = y;

        _tiles = new byte[Size][];
        for (int i = 0; i < Size; i++)
        {
            _tiles[i] = new byte[Size];
        }
    }

    public void Update()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                byte tile = _tiles[x][y];
                if (tile == 0) continue;
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
                byte tile = _tiles[x][y];
                if (tile == 0) continue;
                Tiles.GetTile(tile).Draw(World, X * Size + x, Y * Size + y);
            }
        }
    }

    public byte this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size)
                return 0;
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