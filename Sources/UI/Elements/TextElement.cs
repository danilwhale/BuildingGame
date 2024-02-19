using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class TextElement : Element
{
    public bool AutoExtend = true;
    public IBrush? BackgroundBrush;

    public float Padding = 0;
    public string Text = string.Empty;
    public Alignment TextAlignment = Alignment.TopLeft;

    public Color TextColor = Color.White;
    public float TextSize = 10;

    public TextElement(ElementId id) : base(id)
    {
    }

    public Vector2 GetTextSize()
    {
        return MeasureTextEx(GuiManager.Font, Text, TextSize, TextSize / GuiManager.FontSize);
    }

    public override void Update()
    {
        var textSize = GetTextSize();
        if (AutoExtend && (Size.X < textSize.X || Size.Y < textSize.Y)) Size = textSize + new Vector2(Padding + 4.0f);
    }

    protected override void Render()
    {
        var textSize = GetTextSize();
        var width = Area.Width - Padding - 1;
        var height = Area.Height - Padding - 1;

        var xLeft = Padding + 4;
        var xRight = Area.Width - textSize.X - Padding * 2;
        var xCenter = Area.Width / 2 - textSize.X / 2;
        var yTop = Padding + 4;
        var yBottom = Area.Height - textSize.Y - Padding * 2;
        var yCenter = Area.Height / 2 - textSize.Y / 2;

        var xy = TextAlignment switch
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