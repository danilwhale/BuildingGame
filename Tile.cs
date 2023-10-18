using System.Numerics;

namespace BuildingGame;

public class Tile
{
    public readonly Vector2 TexCoord;

    public Tile()
    {
        TexCoord = Vector2.Zero;
    }
    
    public Tile(Vector2 texCoord)
    {
        TexCoord = texCoord;
    }

    public virtual void Update(World world, int x, int y) {}

    public virtual void Draw(World world, int x, int y)
    {
        DrawTexturePro(
            );
    }
}