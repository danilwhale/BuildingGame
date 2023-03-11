namespace BuildingGame.GuiElements.Brushes;

public class GradientBrush : IBrush
{
    public Color ColorA { get; set; }
    public Color ColorB { get; set; }

    public GradientBrush(Color colorA, Color colorB)
    {
        ColorA = colorA;
        ColorB = colorB;
    }

    public void FillArea(Rectangle area)
    {
        DrawRectangleGradientV((int)area.x, (int)area.y, (int)area.width, (int)area.height, ColorA, ColorB);
    }
}