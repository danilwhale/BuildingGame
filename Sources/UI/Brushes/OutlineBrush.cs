namespace BuildingGame.UI.Brushes;

public class OutlineBrush : IBrush
{
    public Color LineColor;
    public Color FillColor;
    public float LineThick = 1;

    public OutlineBrush(Color lineColor, Color fillColor)
    {
        LineColor = lineColor;
        FillColor = fillColor;
    }
    
    public void FillArea(Rectangle area)
    {
        DrawRectangleRec(area, FillColor);
        DrawRectangleLinesEx(area, LineThick, LineColor);
    }
}