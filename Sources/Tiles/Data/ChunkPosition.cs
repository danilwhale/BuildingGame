namespace BuildingGame.Tiles.Data;

public record ChunkPosition(int X, int Y)
{
    public void WorldToTile(int x, int y, out int tx, out int ty)
    {
        tx = x - X * Chunk.Size;
        ty = y - Y * Chunk.Size;
    }
}