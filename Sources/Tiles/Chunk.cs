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
                TileInfo info = _tiles[x][y];
                if (info == 0) continue;
                if (!Tiles.TryGetTile(info, out var tile)) continue;
                
                tile.OnUpdate(World, info, X * Size + x, Y * Size + y);
            }
        }

        for (var i = 0; i < Size * Size; i++)
        {
            var x = Random.Shared.Next(0, Size);
            var y = Random.Shared.Next(0, Size);
            
            TileInfo info = _tiles[x][y];
            if (info == 0) continue;
            if (!Tiles.TryGetTile(info, out var tile)) continue;
            
            tile.OnRandomUpdate(World, info, x, y);
        }
    }

    public void Draw()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                TileInfo info = _tiles[x][y];
                if (info== 0) continue;
                if (!Tiles.TryGetTile(info, out var tile)) continue;
                
                tile.Draw(World, info, X * Size + x, Y * Size + y, Color.White);
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

            var oldTileInfo = _tiles[x][y];
            
            _tiles[x][y] = value;

            if (!Tiles.TryGetTile(oldTileInfo, out var oldTile)) return;
            if (!Tiles.TryGetTile(value, out var tile)) return;
            
            oldTile.OnInfoUpdate(World, oldTileInfo, value, x, y);
            tile.OnPlace(World, value, x, y);

            NotifyTileInfoUpdate(value, oldTileInfo, x, y, x - 1, y);
            NotifyTileInfoUpdate(value, oldTileInfo, x, y, x + 1, y);
            NotifyTileInfoUpdate(value, oldTileInfo, x, y, x, y - 1);
            NotifyTileInfoUpdate(value, oldTileInfo, x, y, x, y + 1);
        }
    }

    private void NotifyTileInfoUpdate(TileInfo newInfo, TileInfo oldInfo, int x, int y, int nx, int ny)
    {
        var tileInfo = World[Size * X + nx, Size * Y + ny];

        if (!Tiles.TryGetTile(tileInfo, out var tile)) return;
        
        tile.OnNeighbourInfoUpdate(
            World, 
            oldInfo, 
            newInfo, 
            Size * X + nx, 
            Size * Y + ny, 
            Size * X + x, 
            Size * Y + y
            );
    }
}