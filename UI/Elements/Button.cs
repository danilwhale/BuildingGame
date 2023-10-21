namespace BuildingGame.UI.Elements;

public class Button : TextElement
{
    public const string HoverText = "> ";
    
    public event Action? OnClick;
    
    public Button(string name) : base(name)
    {
    }

    public override void Update()
    {
        if (IsClicked())
        {
            OnClick?.Invoke();
        }

        if (IsHovered() && !Text.StartsWith(HoverText))
        {
            Text = HoverText + Text;
        }
        else if (!IsHovered() && Text.StartsWith(HoverText))
        {
            Text = Text.Replace(HoverText, null);
        }
    }
}