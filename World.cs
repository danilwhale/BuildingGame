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
                _chunks[x][y] = new Chunk();
            }
        }
    }
    
    
}