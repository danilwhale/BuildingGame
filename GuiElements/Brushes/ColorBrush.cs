namespace BuildingGame.GuiElements.Brushes;

public class ColorBrush : IBrush
{
    public Color Color { get; set; }

    public ColorBrush(Color color)
    {
        Color = color;
    }

    public void FillArea(Rectangle area)
    {
        DrawRectangleRec(area, Color);
    }

    public static implicit operator Color(ColorBrush bg) => bg.Color;
    public static implicit operator ColorBrush(Color color) => new ColorBrush(color);
}