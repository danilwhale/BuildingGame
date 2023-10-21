using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class TextElement : Element
{
    public IBrush? BackgroundBrush;

    public Color TextColor = WHITE;
    public string Text = string.Empty;
    public float TextSize = 10;
    public Alignment TextAlignment = Alignment.TopLeft;

    public float Padding = 8f;

    public TextElement(string name) : base(name)
    {
    }

    protected override void Render()
    {
        Vector2 fontMeasure = MeasureTextEx(GuiManager.Font, Text, TextSize, TextSize / GuiManager.FontSize);
        float width = MathF.Max(Area.width, fontMeasure.X) + Padding;
        float height = MathF.Max(Area.height, fontMeasure.Y) + Padding;

        float xLeft = Area.x + Padding;
        float xRight = Area.x + Area.width - fontMeasure.X - Padding * 2;
        float xCenter = Area.x + Area.width / 2 - fontMeasure.X / 2;
        float yTop = Area.y + Padding;
        float yBottom = Area.y + Area.height - fontMeasure.Y - Padding * 2;
        float yCenter = Area.y + Area.height / 2 - fontMeasure.Y / 2;

        Vector2 xy = TextAlignment switch
        {
            Alignment.TopLeft => new Vector2(xLeft, yTop),
            Alignment.TopRight => new Vector2(xRight, yTop),
            Alignment.TopCenter => new Vector2(xCenter, yTop),
            Alignment.CenterLeft => new Vector2(xLeft, yCenter),
            Alignment.CenterRight => new Vector2(xRight, yCenter),
            Alignment.Center => new Vector2(xCenter, yCenter),
            Alignment.BottomLeft => new Vector2(xLeft, yBottom),
            Alignment.BottomRight => new Vector2(xRight, yBottom),
            Alignment.BottomCenter => new Vector2(xCenter, yBottom)
        };

        BackgroundBrush?.FillArea(new Rectangle(Area.x - Padding, Area.y - Padding, width, height));
        DrawText(Text, xy.X, xy.Y, TextSize, TextColor);
    }
}