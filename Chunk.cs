namespace BuildingGame;

public struct Chunk
{
    public const int Size = 16;

    public World World;
    public readonly int X, Y;

    public Chunk(World world, int x, int y)
    {
        World = world;
        X = x;
        Y = y;
    }

    public void Update()
    {
        
    }

    public void Draw()
    {
        
    }
}