using System.Numerics;

namespace BuildingGame.UI;

public class Element : IDisposable
{
    private Rectangle _Area;

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
        get => new Vector2(_Area.X, _Area.Y);
        set
        {
            _Area = new Rectangle(value.X, value.Y, _Area.Width, _Area.Height);

            foreach (var child in Children)
            {
                child.GlobalPosition = GlobalPosition + child.LocalPosition;
            }
        }
    }

    public Vector2 Size
    {
        get => new Vector2(_Area.Width, _Area.Height);
        set
        {
            if (value == new Vector2(Area.Width, Area.Height)) return;
            
            Area = new Rectangle(_Area.X, _Area.Y, value.X, value.Y);
        }
    }

    private Vector2 _localPosition;
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

    private Element? _parent;

    public Element? Parent
    {
        get => _parent;
        set
        {
            if (value == null && _parent != null)
            {
                _parent.Children.Remove(this);
            }
            else if (value != null && _parent == null)
            {
                value.Children.Add(this);
            }

            _parent = value;
        }
    }

    public List<Element> Children = new List<Element>();

    public ElementId Id;

    private short _zIndex;

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
            return;
        }
    }

    private bool _active = true;

    public bool Active
    {
        get => _active;
        set
        {
            foreach (var child in Children)
            {
                child.Active = value;
            }

            _active = value;
        }
    }

    private bool _visible = true;

    public bool Visible
    {
        get => _visible;
        set
        {
            foreach (var child in Children)
            {
                child.Visible = value;
            }

            _visible = value;
        }
    }

    public Alignment Origin = Alignment.TopLeft;
    public float Rotation = 0;
    public string TooltipText = string.Empty;

    private bool _ignorePause = false;

    public bool IgnorePause
    {
        get => _ignorePause;
        set
        {
            foreach (var child in Children)
            {
                child.IgnorePause = value;
            }

            _ignorePause = value;
        }
    }

    private RenderTexture2D _controlTexture;

    public Element(ElementId id)
    {
        Id = id;
        Area = new Rectangle(0, 0, 128, 128);
        GuiManager.Add(this);
    }

    public virtual void Update()
    {
    }

    public void Draw()
    {
        Vector2 offset = Origin switch
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
        ClearBackground(Color.BLANK);

        Render();

        EndTextureMode();
            
        DrawTexturePro(
            _controlTexture.Texture,
            new Rectangle(
                0.25f, 0.25f, 
                _Area.Width - 0.25f, -_Area.Height + 0.25f // we need to negate render texture height because opengl uses bottom-left instead of top-left
            ), 
            _Area, offset, Rotation,
            Color.WHITE
        );
    }

    protected virtual void Render()
    {
        ClearBackground(Color.BLACK);
        DrawText(":(", 0, 0, 16, Color.WHITE);
    }

    public bool IsUnderMouse()
    {
        return CheckCollisionPointRec(GetMousePosition(), Area) && Visible && Active;
    }

    public bool IsClicked()
    {
        return IsUnderMouse() && IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT) && Visible && Active;
    }

    public bool IsPressed()
    {
        return IsUnderMouse() && IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && Visible && Active;
    }

    public void Dispose()
    {
        UnloadRenderTexture(_controlTexture);
    }
}