using System.Numerics;

namespace BuildingGame.Tiles.Atlas;

public struct JsonVec2
{
    public float X { get; set; }
    public float Y { get; set; }

    public static implicit operator Vector2(JsonVec2 vec) => new Vector2(vec.X, vec.Y);
}