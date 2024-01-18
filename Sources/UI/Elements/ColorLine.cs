using System.Numerics;
using BuildingGame.Translation;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class ColorLine : Element
{
    public byte R
    {
        get
        {
            if (!byte.TryParse(_redBox.Text, out var r)) return 0;
            return r;
        }
        set
        {
            _redBox.Text = value.ToString();
            ((OutlineBrush)_colorPreview.Brush!).FillColor.R = value;
        }
    }
    
    public byte G
    {
        get
        {
            if (!byte.TryParse(_greenBox.Text, out var g)) return 0;
            return g;
        }
        set
        {
            _greenBox.Text = value.ToString();
            ((OutlineBrush)_colorPreview.Brush!).FillColor.G = value;
        }
    }
    
    public byte B
    {
        get
        {
            if (!byte.TryParse(_blueBox.Text, out var b)) return 0;
            return b;
        }
        set
        {
            _blueBox.Text = value.ToString();
            ((OutlineBrush)_colorPreview.Brush!).FillColor.B = value;
        }
    }

    public Color Color
    {
        get => new Color(R, G, B, (byte)255);
        set => (R, G, B) = (value.R, value.G, value.B);
    }

    public Color DefaultColor = Color.WHITE;
    
    private Panel _colorPreview;
    private TextBox _redBox;
    private TextBox _greenBox;
    private TextBox _blueBox;
    private Button _resetButton;

    public event Action<Color>? OnColorUpdate; 
    
    public ColorLine(ElementId id) : base(id)
    {
        var translation = TranslationContainer.Default;
        
        _colorPreview = new Panel(new ElementId(id, "colorPreview"))
        {
            Brush = new OutlineBrush(Color.DARKGRAY, DefaultColor),
            LocalPosition = new Vector2(0, 0),
            Parent = this,
            Size = new Vector2(40.0f, 20.0f)
        };

        _redBox = new TextBox(new ElementId(id, "redBox"))
        {
            TextSize = 16.0f,
            Size = new Vector2(40.0f, 20.0f),
            CharacterRange = new Range(48, 57),
            Text = DefaultColor.R.ToString(),
            Parent = this,
            LocalPosition = new Vector2(_colorPreview.LocalPosition.X + _colorPreview.Size.X + 8, 0),
        };

        _greenBox = new TextBox(new ElementId(id, "greenBox"))
        {
            TextSize = 16.0f,
            Size = new Vector2(40.0f, 20.0f),
            CharacterRange = new Range(48, 57),
            Text = DefaultColor.G.ToString(),
            Parent = this,
            LocalPosition = new Vector2(_redBox.LocalPosition.X + _redBox.Size.X + 8, 0)
        };

        _blueBox = new TextBox(new ElementId(id, "blueBox"))
        {
            TextSize = 16.0f,
            Size = new Vector2(40.0f, 20.0f),
            CharacterRange = new Range(48, 57),
            Text = DefaultColor.B.ToString(),
            Parent = this,
            LocalPosition = new Vector2(_greenBox.LocalPosition.X + _greenBox.Size.X + 8, 0)
        };

        _resetButton = new Button(new ElementId(id, "resetButton"))
        {
            TextSize = 16.0f,
            Size = new Vector2(64.0f, 20.0f),
            Text = translation.GetTranslatedName("reset_button"),
            ShowHoverText = false,
            Parent = this,
            LocalPosition = new Vector2(_blueBox.LocalPosition.X + _blueBox.Size.X + 8, 0)
        };
        _resetButton.OnClick += () =>
        {
            Color = DefaultColor;
        };
        
        _redBox.OnTextUpdate += RedBoxOnTextUpdate;
        _greenBox.OnTextUpdate += GreenBoxOnTextUpdate;
        _blueBox.OnTextUpdate += BlueBoxOnTextUpdate;

        Size = _colorPreview.Size with { X = _colorPreview.Size.X + _redBox.Size.X + _greenBox.Size.X + _blueBox.Size.X + 8 * 3 };
    }

    private void RedBoxOnTextUpdate(string arg2)
    {
        _ = int.TryParse(arg2, out var r);
        r = Math.Clamp(r, 0, 255);
        
        _redBox.Text = r.ToString();
        ((OutlineBrush)_colorPreview.Brush!).FillColor.R = (byte)r;
        
        OnColorUpdate?.Invoke(Color);
    }
    
    private void GreenBoxOnTextUpdate(string arg2)
    {
        _ = int.TryParse(arg2, out var g);
        g = Math.Clamp(g, 0, 255);
        
        _greenBox.Text = g.ToString();
        ((OutlineBrush)_colorPreview.Brush!).FillColor.G = (byte)g;
        
        OnColorUpdate?.Invoke(Color);
    }
    
    private void BlueBoxOnTextUpdate(string arg2)
    {
        _ = int.TryParse(arg2, out var b);
        b = Math.Clamp(b, 0, 255);

        ((OutlineBrush)_colorPreview.Brush!).FillColor.B = (byte)b;
        _blueBox.Text = b.ToString();
        
        OnColorUpdate?.Invoke(Color);
    }
    
    public override void Update()
    {
    //     _colorPreview.GlobalPosition = GlobalPosition + new Vector2(0, Size.Y / 2 - _colorPreview.Size.Y / 2);
    //     _redBox.GlobalPosition = _colorPreview.GlobalPosition + new Vector2(_colorPreview.Size.X + 8, 0);
    //     _greenBox.GlobalPosition = _redBox.GlobalPosition + new Vector2(+ _redBox.Size.X + 8, 0);
    //     _blueBox.GlobalPosition = _greenBox.GlobalPosition + new Vector2(_greenBox.Size.X + 8, 0);
    //     _resetButton.GlobalPosition = _blueBox.GlobalPosition + new Vector2(_blueBox.Size.X + 8, 0);
    //
    //     _colorPreview.Visible = _redBox.Visible = _greenBox.Visible = _blueBox.Visible = _resetButton.Visible = Visible;
    //     _colorPreview.Active = _redBox.Active = _greenBox.Active = _blueBox.Active = _resetButton.Active = Active;
    }

    protected override void Render()
    {
        
    }
}