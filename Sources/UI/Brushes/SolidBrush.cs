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
        Rlgl.SetBlendMode(0);
        DrawRectangleRec(area, Color);
    }
}