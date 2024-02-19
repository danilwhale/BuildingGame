namespace BuildingGame.Tiles.Data;

public record ChunkPosition(int X, int Y)
{
    /// <summary>
    /// Calculates local to chunk position from world position
    /// </summary>
    /// <param name="x">World X</param>
    /// <param name="y">World Y</param>
    /// <param name="tx">Output tile X position</param>
    /// <param name="ty">Output tile Y position</param>
    public void WorldToTile(int x, int y, out int tx, out int ty)
    {
        tx = x - X * Chunk.Size;
        ty = y - Y * Chunk.Size;
    }
}