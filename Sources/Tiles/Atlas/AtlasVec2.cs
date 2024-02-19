using System.Numerics;

namespace BuildingGame.Tiles.Atlas;

public struct AtlasVec2
{
    public AtlasVec2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float X { get; set; }
    public float Y { get; set; }

    public static implicit operator Vector2(AtlasVec2 vec)
    {
        return new Vector2(vec.X, vec.Y);
    }
}