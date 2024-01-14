using System.Numerics;

namespace BuildingGame.UI.Brushes;

public class TextureBrush : IBrush
{
    public Texture2D Texture;
    public Color Tint = Color.WHITE;
    public Rectangle CropArea;

    public TextureBrush(Texture2D texture)
    {
        Texture = texture;
        CropArea = new Rectangle(0, 0, texture.Width, texture.Height);
    }
    
    public void FillArea(Rectangle area)
    {
        DrawTexturePro(Texture, CropArea, area, Vector2.Zero, 0, Tint);
    }
}