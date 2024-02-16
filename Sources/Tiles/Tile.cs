using System.Numerics;
using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles;

public class Tile
{
    public const int TickCount = 20;
    public const int TileSize = 16;
    public const float TileUpscale = 3;
    public const float RealTileSize = TileSize * TileUpscale;
    public const float AtlasFraction = 0.25f;

    public Vector2 TexCoord;
    public Vector2 Size = Vector2.One;
    public string TranslationKey = "";

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
    
    public Tile(Vector2 texCoord, Vector2 size, string translationKey)
    {
        TexCoord = texCoord;
        Size = size;
        TranslationKey = translationKey;
    }

    public Tile(float tx, float ty)
    {
        TexCoord = new Vector2(tx, ty);
    }

    public virtual void OnPlace(World world, TileInfo info, int x, int y)
    {
    }

    public virtual void OnUpdate(World world, TileInfo info, int x, int y)
    {
        info.Data.TickTimer -= GetFrameTime();
        
        if (info.Data.TickTimer > 0.0f) return;

        info.Data.TickTimer = 1.0f / TickCount;
        info.Data.CurrentTick--;
        if (info.Data.CurrentTick < 0) info.Data.CurrentTick = TickCount;
        OnTick(world, info, x, y);
    }

    public virtual void OnRandomUpdate(World world, TileInfo info, int x, int y)
    {
    }

    protected virtual void OnTick(World world, TileInfo info, int x, int y)
    {
    }

    public virtual void OnNeighbourInfoUpdate(World world, TileInfo oldInfo, TileInfo newInfo, int x, int y,
        int neighbourX, int neighbourY)
    {
    }

    public virtual void OnInfoUpdate(World world, TileInfo oldInfo, TileInfo newInfo, int x, int y)
    {
    }

    public virtual void Draw(World world, TileInfo info, float x, float y, Color tint)
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
            new Vector2(RealTileSize / 2), info.Flags.RotationAsFloat(), tint
        );
    }

    public virtual void DrawPreview(World world, TileInfo info, float x, float y)
    {
        Draw(world, info, x, y, new Color(255, 255, 255, 120));
    }

    public override bool Equals(object? obj)
    {
        return obj is Tile t && t.TexCoord == TexCoord && t.Size == Size && string.Equals(t.TranslationKey, TranslationKey, StringComparison.CurrentCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TexCoord, Size, TranslationKey);
    }
}