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
            _controlTexture = LoadRenderTexture((int)value.width, (int)value.height);
        }
    }

    public Vector2 Position
    {
        get => new Vector2(_Area.x, _Area.y);
        set => _Area = new Rectangle(value.X, value.Y, _Area.width, _Area.height);
    }

    public Vector2 Size
    {
        get => new Vector2(_Area.width, _Area.height);
        set => Area = new Rectangle(_Area.x, _Area.y, value.X, value.Y);
    }

    public string Name;
    public byte ZIndex;

    public bool Active = true;
    public bool Visible = true;

    public Alignment Origin = Alignment.TopLeft;
    public float Rotation = 0;
    public string TooltipText = string.Empty;

    private RenderTexture _controlTexture;

    public Element(string name)
    {
        Name = name;
        Area = new Rectangle(0, 0, 128, 128);
        GuiManager.Add(this);
    }

    public virtual void Update()
    {
    }

    public void Draw()
    {
        BeginTextureMode(_controlTexture);
        ClearBackground(BLANK);

        Render();

        EndTextureMode();

        Vector2 offset = Origin switch
        {
            Alignment.TopLeft => new Vector2(0, 0),
            Alignment.TopRight => new Vector2(_Area.width, 0),
            Alignment.TopCenter => new Vector2(_Area.width / 2, 0),
            Alignment.CenterLeft => new Vector2(0, _Area.height / 2),
            Alignment.CenterRight => new Vector2(_Area.width, _Area.height / 2),
            Alignment.Center => new Vector2(_Area.width / 2, _Area.height / 2),
            Alignment.BottomLeft => new Vector2(0, _Area.height),
            Alignment.BottomRight => new Vector2(_Area.width, _Area.height),
            Alignment.BottomCenter => new Vector2(_Area.width / 2, _Area.height),
            _ => new Vector2(0, 0)
        };

        DrawTexturePro(
            _controlTexture.texture,
            new Rectangle(
                -1, 0, 
                _Area.width + 1, -_Area.height - 1 // we need to negate render texture height because opengl uses bottom-left instead of top-left
                ), 
            _Area, offset, Rotation,
            WHITE
            );
    }

    protected virtual void Render()
    {
        ClearBackground(BLACK);
        DrawText(":(", 0, 0, 16, WHITE);
    }

    public bool IsHovered()
    {
        return CheckCollisionPointRec(GetMousePosition(), Area);
    }

    public bool IsClicked()
    {
        return IsHovered() && IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT);
    }

    public bool IsPressed()
    {
        return IsHovered() && IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT);
    }

    public void Dispose()
    {
        UnloadRenderTexture(_controlTexture);
    }
}