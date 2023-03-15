using BuildingGame.GuiElements.Brushes;

namespace BuildingGame.GuiElements;

public class Tooltip : Control
{
    public IBrush? Background { get; set; }
    public float TextSize { get; set; }
    private string _currentTrigger;

    public Tooltip(string name, float size = 18)
        : base(name)
    {
        ZIndex = 100;
        TextSize = size;
        Active = false;
        _currentTrigger = "";
    }

    public override void Update()
    {
        base.Update();

        var mpos = GetMousePosition() + new Vector2(0, 16);
        var size = MeasureTextEx(Gui.GuiFont, _currentTrigger, TextSize, 1);
        Area = new Rectangle(
            mpos.X - 16,
            mpos.Y,
            size.X + 16,
            size.Y + 16
        );

        bool isTriggered = false;
        foreach (var trigger in Gui.GetControls())
        {
            if (trigger.Control.Tooltip != null && trigger.Control.IsMouseHovered())
            {
                _currentTrigger = trigger.Control.Tooltip;
                isTriggered = true;
                break;
            }
        }
        Active = isTriggered;
    }

    public override void Draw()
    {
        if (Background != null)
            Background.FillArea(Area);
        DrawTextEx(Gui.GuiFont, _currentTrigger, new Vector2(Area.x + 8, Area.y + 8), TextSize, 1, Color.WHITE);
    }
}