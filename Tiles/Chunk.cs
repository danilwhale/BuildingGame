namespace BuildingGame.Tiles;

public class Chunk
{
    public const int SIZE = 16;

    public TileInfo[,] Tiles { get; }

    public Chunk()
    {
        Tiles = new TileInfo[SIZE, SIZE];

        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                Tiles[x, y] = 0;
            }
        }
    }

    public void Draw(int wx, int wy)
    {
        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                TileInfo tile = Tiles[x, y];

                if (tile != null && tile > 0)
                {
                    Tile.GetTile(tile.Type).Draw(wx + x, wy + y, tile.Flags);
                }
                    
            }
        }
    }

    public bool IsValidTile(int x, int y)
    {
        return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
    }
}