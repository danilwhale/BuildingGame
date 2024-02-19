namespace BuildingGame.UI.Elements;

public class Button : TextElement
{
    public const string HoverText = "> ";

    public bool ShowHoverText = true;

    public Button(ElementId id) : base(id)
    {
    }

    public event Action? OnClick;

    public override void Update()
    {
        base.Update();

        if (IsClicked()) OnClick?.Invoke();

        if (ShowHoverText && IsUnderMouse() && !Text.StartsWith(HoverText))
            Text = HoverText + Text;
        else if (!IsUnderMouse() && Text.StartsWith(HoverText)) Text = Text.Replace(HoverText, null);
    }
}