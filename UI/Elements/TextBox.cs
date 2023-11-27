using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class TextBox : TextElement
{
    public int MaxCharacters = 16;
    
    private bool _focused;
    private OutlineBrush _brush;
    
    public TextBox(string name) : base(name)
    {
        _brush = new OutlineBrush(BLACK, LIGHTGRAY);
        TextColor = BLACK;
    }

    public override void Update()
    {
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            _focused = IsHovered();
            _brush.LineColor = _focused ? GRAY : BLACK;
            // _brush.LineThick = _focused ? 1.5f : 1;

            GuiManager.IsFocused = _focused;
        }

        BackgroundBrush = _brush;
        
        GatherInput();
    }

    private void GatherInput()
    {
        if (!_focused) return;

        int c = GetCharPressed();
        if (c is >= 32 and <= 125 && Text.Length < MaxCharacters)
            Text += char.ConvertFromUtf32(c);

        if (IsKeyPressedRepeat((int)KeyboardKey.KEY_BACKSPACE) && Text.Length > 0)
            Text = Text.Remove(Text.Length - 1);
    }
}