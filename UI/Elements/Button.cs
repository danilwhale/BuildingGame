namespace BuildingGame.UI.Elements;

public class Button : TextElement
{
    public const string HoverText = "> ";

    public bool ShowHoverText = true;
    public event Action? OnClick;
    
    public Button(ElementId id) : base(id)
    {
    }

    public override void Update()
    {
        base.Update();
        
        if (IsClicked())
        {
            OnClick?.Invoke();
        }

        if (ShowHoverText && IsHovered() && !Text.StartsWith(HoverText))
        {
            Text = HoverText + Text;
        }
        else if (!IsHovered() && Text.StartsWith(HoverText))
        {
            Text = Text.Replace(HoverText, null);
        }
    }
}