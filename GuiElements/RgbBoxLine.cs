using BuildingGame.GuiElements.Brushes;

namespace BuildingGame.GuiElements;

public class RgbBoxLine : Control
{
    public Color DefaultColor { get; }
    public byte R { get; private set; }
    public byte G { get; private set; }
    public byte B { get; private set; }

    private InputBox _rBox;
    private InputBox _gBox;
    private InputBox _bBox;
    private BackgroundBlock _colorPreview;
    private TextBlock _resetButton;

    public RgbBoxLine(string name, Vector2 position, Color defaultColor)
        : base(name)
    {
        DefaultColor = defaultColor;
        Area = new Rectangle(position.X, position.Y, (48 + 4) * 5, 18);
        _colorPreview = new BackgroundBlock(Name + "_colorprev", (ColorBrush)Color.WHITE)
        {
            Area = new Rectangle(position.X, position.Y, 32, 18)
        };

        Range numRange = new Range('\u0030', '\u0039');

        _rBox = new InputBox(Name + "_rbox", numRange, textSize: 18)
        {
            Area = new Rectangle(position.X + 48 + 4, position.Y, 0, 0),
        };
        _rBox.ResizeTo(3);

        _gBox = new InputBox(Name + "_gbox", numRange, textSize: 18)
        {
            Area = new Rectangle(position.X + (48 + 4) * 2, position.Y, 0, 0),
        };
        _gBox.ResizeTo(3);

        _bBox = new InputBox(Name + "_bbox", numRange, textSize: 18)
        {
            Area = new Rectangle(position.X + (48 + 4) * 3, position.Y, 0, 0)
        };
        _bBox.ResizeTo(3);

        _resetButton = new TextBlock(Name + "_reset", "reset", new Vector2(position.X + (48 + 4) * 4, position.Y), 18);
        _resetButton.Color = Color.WHITE;
        _resetButton.Clicked += () =>
        {
            ImportColor(DefaultColor);
        };

        _colorPreview.Adapt(_ => new Vector2(Area.x, Area.y));
        _rBox.Adapt(_ => new Vector2(Area.x + 48 + 4, Area.y));
        _gBox.Adapt(_ => new Vector2(Area.x + (48 + 4) * 2, Area.y));
        _bBox.Adapt(_ => new Vector2(Area.x + (48 + 4) * 3, Area.y));
        _resetButton.Adapt(_ => new Vector2(Area.x + (48 + 4) * 4, Area.y));

        Children.Add(_colorPreview);
        Children.Add(_rBox);
        Children.Add(_gBox);
        Children.Add(_bBox);
        Children.Add(_resetButton);
        // Gui.PutControl(id, this);
    }

    public override void Update()
    {
        base.Update();

        if (byte.TryParse(_rBox.Text, out var r)) R = r;
        if (byte.TryParse(_gBox.Text, out var g)) G = g;
        if (byte.TryParse(_bBox.Text, out var b)) B = b;

        _colorPreview.Background = (ColorBrush)new Color(R, G, B, (byte)255);
    }

    public Color ExportColor() => new Color(R, G, B, (byte)255);
    public void ImportColor(Color color)
    {
        R = color.r;
        G = color.g;
        B = color.b;

        _rBox.Text = R.ToString();
        _gBox.Text = G.ToString();
        _bBox.Text = B.ToString();
        _colorPreview.Background = (ColorBrush)new Color(R, G, B, (byte)255);
    }
}