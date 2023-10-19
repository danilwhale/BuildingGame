namespace BuildingGame;

public struct World
{
    public readonly int Width;
    public readonly int Height;
    public readonly int ChunkWidth;
    public readonly int ChunkHeight;

    private Chunk[][] _chunks;

    public World(int width, int height)
    {
        Width = width;
        Height = height;
        ChunkWidth = width / Chunk.Size;
        ChunkHeight = height / Chunk.Size;

        _chunks = new Chunk[ChunkWidth][];
        for (int x = 0; x < ChunkWidth; x++)
        {
            _chunks[x] = new Chunk[ChunkHeight];
            for (int y = 0; y < ChunkHeight; y++)
            {
                _chunks[x][y] = new Chunk(this, x, y);
            }
        }
    }

    public void Update()
    {
        for (int x = 0; x < ChunkWidth; x++)
        {
            for (int y = 0; y < ChunkHeight; y++)
            {
                _chunks[x][y].Update();
            }
        }
    }

    public void Draw(Player player)
    {
        Rectangle view = player.GetViewRectangle();

        for (int x = 0; x < ChunkWidth; x++)
        {
            for (int y = 0; y < ChunkHeight; y++)
            {
                if (CheckCollisionRecs(new Rectangle(x * Chunk.ViewSize, y * Chunk.ViewSize, Chunk.ViewSize, Chunk.ViewSize), view))
                    _chunks[x][y].Draw();
            }
        }
    }

    public ChunkPosition WorldToChunk(int x, int y)
    {
        int cx = x / Chunk.Size;
        int cy = y / Chunk.Size;

        return new ChunkPosition(cx, cy);
    }

    public byte this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return 0;

            ChunkPosition pos = WorldToChunk(x, y);
            pos.WorldToTile(x, y, out int tx, out int ty);

            return _chunks[pos.X][pos.Y][tx, ty];
        }
        set
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            ChunkPosition pos = WorldToChunk(x, y);
            pos.WorldToTile(x, y, out int tx, out int ty);

            _chunks[pos.X][pos.Y][tx, ty] = value;
        }
    }
}