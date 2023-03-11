using BuildingGame.GuiElements.Brushes;

namespace BuildingGame.GuiElements;

public class ImageArea : Control
{
    public Texture2D Image { get; set; }
    public Rectangle ImageSourceRect { get; set; }
    public IBrush? Background { get; set; }
    public Color Tint { get; set; }

    public ImageArea(string name, Texture2D image, Rectangle imageSourceRect, Color tint)
        : base(name)
    {
        Image = image;
        ImageSourceRect = imageSourceRect;
        Tint = tint;
    }

    public override void Draw()
    {
        if (Background != null)
            Background.FillArea(Area);
        DrawTexturePro(Image, ImageSourceRect, Area, Vector2.Zero, 0, Tint);
    }
}