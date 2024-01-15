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
        base.Update();
        
        Element? el = GuiManager.GetElementUnderMouse(el => el.IsUnderMouse() && !string.IsNullOrWhiteSpace(el.TooltipText));

        if (el != null)
        {
            Text = el.TooltipText;
            Visible = true;
        }
        else Visible = false;

        Position = GetMousePosition() + new Vector2(16);
    }
}