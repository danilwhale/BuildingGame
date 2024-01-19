using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles;

public struct Chunk
{
    public const int Size = 16;
    public const float ViewSize = Size * Tile.RealTileSize;

    public World World;
    public readonly int X, Y;

    private TileInfo[][] _tiles;

    public Chunk(World world, int x, int y)
    {
        World = world;
        X = x;
        Y = y;

        _tiles = new TileInfo[Size][];
        for (int i = 0; i < Size; i++)
        {
            _tiles[i] = new TileInfo[Size];
            for (int j = 0; j < Size; j++)
            {
                _tiles[i][j] = 0;
            }
        }
    }

    public void Update()
    {
        for (int x = Size - 1; x >= 0; x--)
        {
            for (int y = Size - 1; y >= 0; y--)
            {
                TileInfo tile = _tiles[x][y];
                if (tile == 0) continue;
                Tiles.GetTile(tile).Update(World, tile, X * Size + x, Y * Size + y);
            }
        }
    }

    public void Draw()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                TileInfo tile = _tiles[x][y];
                if (tile == 0) continue;
                Tiles.GetTile(tile).Draw(World, tile, X * Size + x, Y * Size + y, Color.WHITE);
            }
        }
    }

    public TileInfo this[int x, int y]
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