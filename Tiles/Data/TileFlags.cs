namespace BuildingGame.Tiles.Data;

public struct TileFlags
{
    public static TileFlags Default => new TileFlags(TileRotation.Up, false);

    public TileRotation Rotation;
    public bool Flip;
    
    public TileFlags(TileRotation rotation, bool flip)
    {
        Rotation = rotation;
        Flip = flip;
    }

    public TileFlags(float rotation, bool flip)
    {
        Rotation = FloatAsRotation(rotation);
        Flip = flip;
    }

    public float RotationAsFloat()
    {
        return Rotation switch
        {
            TileRotation.Up => 0,
            TileRotation.Left => 90,
            TileRotation.Down => 180,
            TileRotation.Right => 270
        };
    }

    private static TileRotation FloatAsRotation(float rotation)
    {
        if (rotation is > 0 and <= 90) return TileRotation.Left;
        if (rotation is > 90 and <= 180) return TileRotation.Down;
        if (rotation is > 180 and <= 270) return TileRotation.Right;
        
        return TileRotation.Up;
    }
}