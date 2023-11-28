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
    public bool AutoExtend = true;

    public float Padding = 0;

    public TextElement(string name) : base(name)
    {
    }

    protected override void Render()
    {
        Vector2 fontMeasure = MeasureTextEx(GuiManager.Font, Text, TextSize, TextSize / GuiManager.FontSize);
        float width = (AutoExtend ? MathF.Max(Area.width, fontMeasure.X) : Area.width) - Padding - 1;
        float height = (AutoExtend ? MathF.Max(Area.height, fontMeasure.Y) : Area.height) - Padding - 1;

        float xLeft = Padding + 4;
        float xRight = Area.width - fontMeasure.X - Padding * 2;
        float xCenter = Area.width / 2 - fontMeasure.X / 2;
        float yTop = Padding + 4;
        float yBottom = Area.height - fontMeasure.Y - Padding * 2;
        float yCenter = Area.height / 2 - fontMeasure.Y / 2;

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

        BackgroundBrush?.FillArea(new Rectangle(Padding, Padding, width, height));
        DrawText(Text, xy.X, xy.Y, TextSize, TextColor);
    }
}