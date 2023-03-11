namespace BuildingGame.GuiElements;

public class CheckBox : Control
{
    public bool Checked { get; set; }
    public string? Text { get; set; }
    public float TextSize { get; set; }

    public CheckBox(string name, string text, Vector2 position, float textSize)
        : base(name)
    {
        Checked = false;
        Text = text;
        TextSize = textSize;

        var size = MeasureTextEx(Gui.GuiFont, Text, TextSize, 1);
        Area = new Rectangle(position.X, position.Y, size.X + 18 + 8, 18 + Math.Abs(size.Y - 18));
    }

    public override void Update()
    {
        base.Update();

        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && IsMouseHovered())
            Checked = !Checked;
    }

    public override void Draw()
    {
        DrawRectangle((int)Area.x, (int)Area.y, 18, 18, Color.LIGHTGRAY);
        DrawRectangleLines((int)Area.x, (int)Area.y, 18, 18, Color.GRAY);
        if (Checked)
            DrawTexture(Program.check, (int)Area.x, (int)Area.y, Color.WHITE);
        DrawTextEx(Gui.GuiFont, Text, new Vector2(Area.x + 8 + 18, Area.y), TextSize, 1, Color.WHITE);
    }
}