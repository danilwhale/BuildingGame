using System.Numerics;

namespace BuildingGame.UI.Elements;

public class Tooltip : TextElement
{
    public Tooltip(ElementId id) : base(id)
    {
        Visible = false;
        TextAlignment = Alignment.CenterLeft;
    }

    public override void Update()
    {
        Element? el = GuiManager.GetElementUnderMouse();

        if (el != null && el.TooltipText != string.Empty)
        {
            Text = el.TooltipText;
            Visible = true;

            Vector2 measure = MeasureTextEx(GuiManager.Font, Text, TextSize, TextSize / GuiManager.FontSize);
            Vector2 size = measure + new Vector2(Padding * 4, Padding * 2);
            if (Size != size) Size = size;
        }
        else Visible = false;

        Position = GetMousePosition() + new Vector2(16);
    }
}