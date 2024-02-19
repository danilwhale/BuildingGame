using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class TextBox : TextElement
{
    private readonly OutlineBrush _brush;

    private bool _focused;

    public Range CharacterRange = new(32, 125);
    public int MaxCharacters = 16;

    public TextBox(ElementId id) : base(id)
    {
        _brush = new OutlineBrush(Color.Black, Color.LightGray);
        TextColor = Color.Black;
    }

    public event Action<string>? OnTextUpdate;

    public override void Update()
    {
        base.Update();

        if (IsMouseButtonPressed(MouseButton.Left))
        {
            _focused = IsUnderMouse();
            _brush.LineColor = _focused ? Color.Gray : Color.Black;
            // _brush.LineThick = _focused ? 1.5f : 1;

            GuiManager.IsFocused = _focused;
        }

        BackgroundBrush = _brush;

        GatherInput();
    }

    private void GatherInput()
    {
        if (!_focused) return;

        var c = GetCharPressed();
        if (c >= CharacterRange.Start.Value && c <= CharacterRange.End.Value && Text.Length < MaxCharacters)
            Text += char.ConvertFromUtf32(c);

        if (IsKeyPressedRepeat(KeyboardKey.Backspace) && Text.Length > 0)
            Text = Text.Remove(Text.Length - 1);

        OnTextUpdate?.Invoke(Text);
    }
}