namespace BuildingGame.UI.Brushes;

public class SolidBrush : IBrush
{
    public Color Color;

    public SolidBrush(Color color)
    {
        Color = color;
    }
    
    public void FillArea(Rectangle area)
    {
        RlGl.rlSetBlendMode(0);
        DrawRectangleRec(area, Color);
    }
}