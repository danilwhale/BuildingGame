namespace BuildingGame.GuiElements;

public class InputBox : Control
{
    public string? Text { get; set; } = string.Empty;
    public Range CharacterRange { get; set; } = new Range(32, 125);
    public float TextSize { get; set; } = 12f;
    public int MaxCharacters { get; set; } = 3;
    private bool _focused = false;

    public InputBox(string name, int maxChars = 3, float textSize = 12)
        : base(name)
    {
        MaxCharacters = maxChars;
        TextSize = textSize;
    }

    public InputBox(string name, Range characterRange, int maxChars = 3, float textSize = 12)
        : base(name)
    {
        CharacterRange = characterRange;
        MaxCharacters = maxChars;
        TextSize = textSize;
    }

    public override void Update()
    {
        base.Update();

        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            if (IsMouseHovered()) _focused = true;
            else _focused = false;
        }

        if (_focused)
        {
            int @char = GetCharPressed();
            if (@char >= CharacterRange.Start.Value && @char <= CharacterRange.End.Value && Text != null && Text.Length < MaxCharacters)
            {
                Text += char.ConvertFromUtf32(@char);
            }
        }
        if (IsKeyPressed(KeyboardKey.KEY_BACKSPACE) && Text != null && Text.Length != 0 && _focused)
        {
            Text = Text.Remove(Text.Length - 1);
            Log.Information(Text);
        }
    }

    public override void Draw()
    {
        DrawRectangleRec(Area, Color.LIGHTGRAY);
        DrawRectangleLinesEx(Area, 1, !_focused ? Color.BLACK : Color.GRAY);
        DrawTextEx(Gui.GuiFont, Text, new Vector2(Area.x + 8, Area.y), TextSize, 1, Color.BLACK);
    }

    public void ResizeTo(int charCount)
    {
        var size = MeasureTextEx(Gui.GuiFont, CopyString('@', charCount), TextSize, 1);
        Area = new Rectangle(
            Area.x,
            Area.y,
            size.X,
            size.Y
        );
        MaxCharacters = charCount;
    }

    private string CopyString(char c, int count)
    {
        string str = "";
        for (int i = 0; i < count; i++)
            str += c;
        return str;
    }
}