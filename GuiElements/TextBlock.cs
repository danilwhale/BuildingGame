namespace BuildingGame.GuiElements;

public class TextBlock : Control
{
    public string? Text { get; set; }
    public float Size { get; set; } = 12f;
    public Font Font { get; set; } = Gui.GuiFont;
    public Color Color { get; set; } = Color.BLACK;
    protected bool _centeredScreen = false;

    public TextBlock(string name, string text, Vector2 position, float size = 12)
        : base(name)
    {
        Text = text;
        Area = new Rectangle(position.X, position.Y, 0, 0);
        Size = size;
        SetupCollision();
    }

    public void Center()
    {
        Area = new Rectangle(
            Area.x + Area.width / 2 - MeasureTextEx(Font, Text, Size, 1).X / 2,
            Area.y,
            Area.width,
            Area.height
        );
    }
    public void CenterScreen()
    {
        Area = new Rectangle(
            Program.WIDTH / 2 - MeasureTextEx(Font, Text, Size, 1).X / 2,
            Area.y,
            Area.width,
            Area.height
        );
        _centeredScreen = true;
    }

    public void SetupCollision(string? text = null)
    {
        if (text == null) text = Text;
        if (text != null)
        {
            var size = MeasureTextEx(Font, text, Size, 1);

            Area = new Rectangle(
                Area.x,
                Area.y,
                size.X,
                size.Y
            );
        }
    }
    public override void Draw()
    {
        DrawTextEx(Font, Text, new Vector2(Area.x, Area.y), Size, 1, Color);
    }
}