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
        var x = (int)area.X;
        var y = (int)area.Y;
        var width = (int)area.Width;
        var height = (int)area.Height;

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