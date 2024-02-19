using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class Tooltip : Element
{
    private readonly GradientBrush _brush;
    public string Text;
    public float TextSize;

    public Tooltip(ElementId id) : base(id)
    {
        _brush = new GradientBrush(new Color(0, 0, 0, 50), new Color(16, 0, 16, 127));
        Visible = false;
    }

    public override void Update()
    {
        base.Update();

        var el = GuiManager.GetElementUnderMouse(el => el.IsUnderMouse() && !string.IsNullOrWhiteSpace(el.TooltipText));

        if (el != null)
        {
            Text = el.TooltipText;
            Visible = true;
        }
        else
        {
            Visible = false;
        }

        var pos = GetMousePosition() + new Vector2(0, 16);
        var size = MeasureTextEx(GetFontDefault(), Text, TextSize, TextSize / GuiManager.FontSize);

        GlobalPosition = pos with { X = pos.X - 16.0f };
        Size = size + new Vector2(16);
    }

    protected override void Render()
    {
        _brush.FillArea(new Rectangle(0, 0, Size.X, Size.Y));
        DrawTextEx(GuiManager.Font, Text, new Vector2(8.0f), TextSize, TextSize / GuiManager.FontSize, Color.White);
    }
}