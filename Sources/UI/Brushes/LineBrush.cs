namespace BuildingGame.UI.Brushes;

public class LineBrush : IBrush
{
    public Color Color;
    public float LineThick = 1;

    public LineBrush(Color color)
    {
        Color = color;
    }

    public void FillArea(Rectangle area)
    {
        DrawRectangleLinesEx(area, LineThick, Color);
    }
}