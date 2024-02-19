namespace BuildingGame.Tiles.Data;

public struct TileFlags
{
    public static TileFlags Default => new(TileRotation.Up);

    public TileRotation Rotation;

    public TileFlags(TileRotation rotation)
    {
        Rotation = rotation;
    }

    public TileFlags(float rotation)
    {
        Rotation = FloatAsRotation(rotation);
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

    public void FlipRotation()
    {
        Rotation = Rotation switch
        {
            TileRotation.Up => TileRotation.Down,
            TileRotation.Down => TileRotation.Up,
            TileRotation.Left => TileRotation.Right,
            TileRotation.Right => TileRotation.Left
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