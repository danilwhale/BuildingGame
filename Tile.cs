using System.Numerics;

namespace BuildingGame;

public class Tile
{
    public const int TileSize = 16;
    public const float TileUpscale = 3;
    public const float AtlasFraction = 0.25f;

    public readonly Vector2 TexCoord;

    public Tile()
    {
        TexCoord = Vector2.Zero;
    }

    public Tile(Vector2 texCoord)
    {
        TexCoord = texCoord;
    }

    public virtual void Update(World world, int x, int y)
    {
    }

    public virtual void Draw(World world, int x, int y)
    {
        DrawTexturePro(Resources.GetTexture("Atlas.png"),
            // we add a fraction to the source rectangle, so we wont see flickering parts of atlas
            new Rectangle(
                TexCoord.X * TileSize + AtlasFraction,
                TexCoord.Y * TileSize + AtlasFraction,
                TileSize - AtlasFraction,
                TileSize - AtlasFraction
            ),
            new Rectangle(
                x * TileSize * TileUpscale,
                y * TileSize * TileUpscale,
                TileSize * TileUpscale,
                TileSize * TileUpscale
            ),
            Vector2.Zero, 0, WHITE
        );
    }
}