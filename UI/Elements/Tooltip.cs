using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class Tooltip : Element
{
    public string Text;
    public float TextSize;
    private GradientBrush _brush;
    
    public Tooltip(ElementId id) : base(id)
    {
        _brush = new GradientBrush(new Color(0, 0, 0, 50), new Color(16, 0, 16, 127));
        Visible = false;
    }

    public override void Update()
    {
        base.Update();
        
        Element? el = GuiManager.GetElementUnderMouse(el => el.IsUnderMouse() && !string.IsNullOrWhiteSpace(el.TooltipText));

        if (el != null)
        {
            Text = el.TooltipText;
            Visible = true;
        }
        else Visible = false;

        var pos = GetMousePosition() + new Vector2(0, 16);
        var size = MeasureTextEx(GetFontDefault(), Text, TextSize, GetSpacing(TextSize));

        Position = pos with { X = pos.X - 16.0f };
        Size = size + new Vector2(16);
    }

    protected override void Render()
    {
        _brush.FillArea(new Rectangle(0, 0, Size.X, Size.Y));
        DrawText(Text, 8.0f, 8.0f, TextSize, Color.WHITE);
    }
}