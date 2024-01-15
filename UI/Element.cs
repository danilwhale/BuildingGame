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

    public Vector2 Position
    {
        get => new Vector2(_Area.X, _Area.Y);
        set => _Area = new Rectangle(value.X, value.Y, _Area.Width, _Area.Height);
    }

    public Vector2 Size
    {
        get => new Vector2(_Area.Width, _Area.Height);
        set => Area = new Rectangle(_Area.X, _Area.Y, value.X, value.Y);
    }

    public ElementId Id;
    public short ZIndex;

    public bool Active = true;
    public bool Visible = true;

    public Alignment Origin = Alignment.TopLeft;
    public float Rotation = 0;
    public string TooltipText = string.Empty;

    public bool IgnorePause = false;

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
        BeginTextureMode(_controlTexture);
        ClearBackground(Color.BLANK);

        Render();

        EndTextureMode();

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

        DrawTexturePro(
            _controlTexture.Texture,
            new Rectangle(
                0, 0, 
                _Area.Width, -_Area.Height // we need to negate render texture height because opengl uses bottom-left instead of top-left
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