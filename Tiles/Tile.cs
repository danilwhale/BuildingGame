using System.Numerics;
using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles;

public class Tile
{
    public const int TileSize = 16;
    public const float TileUpscale = 3;
    public const float RealTileSize = TileSize * TileUpscale;
    public const float AtlasFraction = 0.25f;

    public readonly Vector2 TexCoord;
    public readonly Vector2 Size = Vector2.One;

    public Tile()
    {
        TexCoord = Vector2.Zero;
    }

    public Tile(Vector2 texCoord)
    {
        TexCoord = texCoord;
    }

    public Tile(Vector2 texCoord, Vector2 size)
    {
        TexCoord = texCoord;
        Size = size;
    }

    public Tile(float tx, float ty)
    {
        TexCoord = new Vector2(tx, ty);
    }

    public virtual void Update(World world, TileInfo info, int x, int y)
    {
    }

    public virtual void Draw(World world, TileInfo info, int x, int y)
    {
        DrawTexturePro(Resources.GetTexture("Atlas.png"),
            // we add a fraction to the source rectangle, so we wont see flickering parts of atlas
            new Rectangle(
                TexCoord.X * TileSize + AtlasFraction,
                TexCoord.Y * TileSize + AtlasFraction,
                Size.X * TileSize - AtlasFraction,
                Size.Y * TileSize - AtlasFraction
            ),
            new Rectangle(
                x * TileSize * TileUpscale,
                y * TileSize * TileUpscale,
                Size.X * TileSize * TileUpscale,
                Size.Y * TileSize * TileUpscale
            ),
            Vector2.Zero, 0, WHITE
        );
    }

    public virtual void DrawPreview(float x, float y)
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
            Vector2.Zero, 0, new Color(255, 255, 255, 120)
        );
    }
}