namespace BuildingGame;

// TODO: implement rotation enum instead of storing 32-bit floating point, because there are only 0, 90, 180, 270 degrees rotations available in-game
public record TileFlags(float Rotation, bool Flip)
{
    public static TileFlags Default => new TileFlags(0, false);
}