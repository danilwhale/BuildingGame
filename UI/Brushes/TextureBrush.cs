using System.Numerics;

namespace BuildingGame.UI.Brushes;

public class TextureBrush : IBrush
{
    public Texture Texture;
    public Color Tint = WHITE;
    public Rectangle CropArea;

    public TextureBrush(Texture texture)
    {
        Texture = texture;
        CropArea = new Rectangle(0, 0, texture.width, texture.height);
    }
    
    public void FillArea(Rectangle area)
    {
        DrawTexturePro(Texture, CropArea, area, Vector2.Zero, 0, Tint);
    }
}