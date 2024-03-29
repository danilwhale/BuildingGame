using System.Numerics;

namespace BuildingGame.UI;

public class Element : IDisposable
{
    private bool _active = true;
    private Rectangle _Area;

    private RenderTexture2D _controlTexture;

    private bool _ignorePause;

    private Vector2 _localPosition;

    private Element? _parent;

    private bool _visible = true;

    private short _zIndex;

    public List<Element> Children = new();

    public ElementId Id;

    public Alignment Origin = Alignment.TopLeft;
    public float Rotation = 0;
    public string TooltipText = string.Empty;

    public Element(ElementId id)
    {
        Id = id;
        Area = new Rectangle(0, 0, 128, 128);
        GuiManager.Add(this);
    }

    public Rectangle Area
    {
        get => _Area;
        set
        {
            _Area = value;

            UnloadRenderTexture(_controlTexture);
            _controlTexture = LoadRenderTexture((int)value.Width, (int)value.Height);
        }
    }

    public Vector2 GlobalPosition
    {
        get => new(_Area.X, _Area.Y);
        set
        {
            _Area = new Rectangle(value.X, value.Y, _Area.Width, _Area.Height);

            foreach (var child in Children) child.GlobalPosition = GlobalPosition + child.LocalPosition;
        }
    }

    public Vector2 Size
    {
        get => new(_Area.Width, _Area.Height);
        set
        {
            if (value == new Vector2(Area.Width, Area.Height)) return;

            Area = new Rectangle(_Area.X, _Area.Y, value.X, value.Y);
        }
    }

    public Vector2 LocalPosition
    {
        get => Parent != null ? _localPosition : GlobalPosition;
        set
        {
            if (Parent == null)
            {
                GlobalPosition = value;
            }
            else
            {
                GlobalPosition = Parent.GlobalPosition + value;
                _localPosition = value;
            }
        }
    }

    public Element? Parent
    {
        get => _parent;
        set
        {
            if (value == null && _parent != null)
                _parent.Children.Remove(this);
            else if (value != null && _parent == null) value.Children.Add(this);

            _parent = value;
        }
    }

    public short ZIndex
    {
        get
        {
            if (Parent == null) return _zIndex;
            return (short)(Parent.ZIndex + _zIndex);
        }
        set
        {
            if (Parent == null)
            {
                _zIndex = value;
                return;
            }

            _zIndex = (short)(Parent.ZIndex + value);
        }
    }

    public bool Active
    {
        get => _active;
        set
        {
            foreach (var child in Children) child.Active = value;

            _active = value;
        }
    }

    public bool Visible
    {
        get => _visible;
        set
        {
            foreach (var child in Children) child.Visible = value;

            _visible = value;
        }
    }

    public bool IgnorePause
    {
        get => _ignorePause;
        set
        {
            foreach (var child in Children) child.IgnorePause = value;

            _ignorePause = value;
        }
    }

    public void Dispose()
    {
        UnloadRenderTexture(_controlTexture);
    }

    public virtual void Update()
    {
    }

    public void Draw()
    {
        var offset = Origin switch
        {
            Alignment.TopLeft => new Vector2(0, 0),
            Alignment.TopRight => new Vector2(_Area.Width, 0),
            Alignment.TopCenter => new Vector2(_Area.Width / 2, 0),
            Alignment.CenterLeft => new Vector2(0, _Area.Height / 2),
            Alignment.CenterRight => new Vector2(_Area.Width, _Area.Height / 2),
            Alignment.Center => new Vector2(_Area.Width / 2, _Area.Height / 2),
            Alignment.BottomLeft => new Vector2(0, _Area.Height),
            Alignment.BottomRight => new Vector2(_Area.Width, _Area.Height),
            Alignment.BottomCenter => new Vector2(_Area.Width / 2, _Area.Height),
            _ => new Vector2(0, 0)
        };

        BeginTextureMode(_controlTexture);
        ClearBackground(Color.Blank);

        Render();

        EndTextureMode();

        DrawTexturePro(
            _controlTexture.Texture,
            new Rectangle(
                0.25f, 0.25f,
                _Area.Width - 0.25f,
                -_Area.Height +
                0.25f // we need to negate render texture height because opengl uses bottom-left instead of top-left
            ),
            _Area, offset, Rotation,
            Color.White
        );
    }

    protected virtual void Render()
    {
        ClearBackground(Color.Black);
        DrawText(":(", 0, 0, 16, Color.White);
    }

    public bool IsUnderMouse()
    {
        return CheckCollisionPointRec(GetMousePosition(), Area) && Visible && Active;
    }

    public bool IsClicked()
    {
        return IsUnderMouse() && IsMouseButtonReleased(MouseButton.Left) && Visible && Active;
    }

    public bool IsPressed()
    {
        return IsUnderMouse() && IsMouseButtonDown(MouseButton.Left) && Visible && Active;
    }
}