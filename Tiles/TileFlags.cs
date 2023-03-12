namespace BuildingGame.Tiles;

public struct TileFlags
{
    public static TileFlags Default => new TileFlags(0, false);
    public float Rotation { get; set; }
    public bool Flip { get; set; }

    public TileFlags(float rotation, bool flip)
    {
        Rotation = rotation;
        Flip = flip;
    }
}