using System.Numerics;
using ZeroElectric.Vinculum;

namespace BuildingGame;

public class Player
{
    public Camera2D Camera;
    public float Speed;
    public float LerpSpeed;

    private Vector2 _targetPosition;
    private float _targetZoom;

    public Player(Vector2 position, float speed, float lerpSpeed)
    {
        Camera = new Camera2D
        {
            offset = new Vector2(GetScreenWidth(), GetScreenHeight()) / 2,
            target = position,
            zoom = 1.0f
        };
        _targetPosition = position;
        _targetZoom = 1.0f;
        Speed = speed;
        LerpSpeed = lerpSpeed;
    }

    public void Update()
    {
        Camera.target = Vector2.Lerp(Camera.target, _targetPosition, LerpSpeed); // lerp camera position to target position
        Camera.zoom = (Camera.zoom * (1.0f - LerpSpeed)) + (_targetZoom * LerpSpeed); // lerp camera zoom to target zoom
        
        // zoom in/zoom out camera
        float scrollDelta = GetMouseWheelMove();
        if (scrollDelta < 0) ZoomOut(Speed * 0.1f);
        if (scrollDelta > 0) ZoomIn(Speed * 0.1f);
        
        // get speed depending if user is sprinting or not
        float speed = IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL) || IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT)
            ? Speed * 1.25f
            : Speed * 0.5f;
        speed /= Camera.zoom;
        speed = Math.Clamp(speed, 0, Speed * 2);
        
        // move camera
        if (IsKeyDown(KeyboardKey.KEY_W)) Move(0, -speed);
        if (IsKeyDown(KeyboardKey.KEY_S)) Move(0, speed);
        if (IsKeyDown(KeyboardKey.KEY_A)) Move(-speed, 0);
        if (IsKeyDown(KeyboardKey.KEY_D)) Move(speed, 0);
        
        // adapt camera center when windows is resized
        if (IsWindowResized())
        {
            Camera.offset = new Vector2(GetScreenWidth(), GetScreenHeight()) / 2;
        }
    }

    public void ZoomIn(float a)
    {
        _targetZoom += a * GetFrameTime();
    }

    public void ZoomOut(float a)
    {
        _targetZoom -= a * GetFrameTime();
        // clamp zoom, so camera wont get inverted
        if (_targetZoom < 0.01f) _targetZoom = 0.01f;
    }

    public void MoveTo(float x, float y)
    {
        _targetPosition = new Vector2(x, y);
    }

    public void Move(float x, float y)
    {
        _targetPosition += new Vector2(x, y) * Speed * GetFrameTime();
    }
}