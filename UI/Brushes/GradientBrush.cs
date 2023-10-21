namespace BuildingGame.UI.Brushes;

public class GradientBrush : IBrush
{
    public Color ColorA;
    public Color ColorB;
    public GradientDirection Direction;
    
    public GradientBrush(Color colorA, Color colorB, GradientDirection direction = GradientDirection.Vertical)
    {
        ColorA = colorA;
        ColorB = colorB;
        Direction = direction;
    }
    
    public void FillArea(Rectangle area)
    {
        int x = (int)area.x;
        int y = (int)area.y;
        int width = (int)area.width;
        int height = (int)area.height;
        
        switch (Direction)
        {
            case GradientDirection.Vertical:
                DrawRectangleGradientV(x, y, width, height, ColorA, ColorB);
                break;
            case GradientDirection.Horizontal:
                DrawRectangleGradientH(x, y, width, height, ColorA, ColorB);
                break;
        }
    }
}