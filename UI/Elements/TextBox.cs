using System.Runtime.InteropServices;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class TextBox : TextElement
{
    public int MaxCharacters = 16;
    
    private bool _focused;
    private OutlineBrush _brush;
    
    public TextBox(ElementId id) : base(id)
    {
        _brush = new OutlineBrush(Color.BLACK, Color.LIGHTGRAY);
        TextColor = Color.BLACK;
    }

    public override void Update()
    {
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            _focused = IsHovered();
            _brush.LineColor = _focused ? Color.GRAY : Color.BLACK;
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

        
        if (IsKeyPressedRepeat(KeyboardKey.KEY_BACKSPACE) && Text.Length > 0)
            Text = Text.Remove(Text.Length - 1);
    }
}