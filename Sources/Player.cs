using System.Numerics;
using BuildingGame.Tiles;
using BuildingGame.Tiles.Data;
using BuildingGame.UI;

namespace BuildingGame;

public class Player
{
    public static TileInfo CurrentTile = new(1, TileFlags.Default);

    private Vector2 _targetPosition;
    private float _targetZoom;
    private int _tileX;
    private int _tileY;

    public Camera2D Camera;
    public float LerpSpeed;
    public float Speed;
    public World World;

    public Player(World world, Vector2 position, float zoom, float speed, float lerpSpeed)
    {
        World = world;
        Camera = new Camera2D
        {
            Offset = new Vector2(GetScreenWidth(), GetScreenHeight()) / 2,
            Target = position,
            Zoom = zoom
        };
        _targetPosition = position;
        _targetZoom = zoom;
        Speed = speed;
        LerpSpeed = lerpSpeed;
    }

    public void Update()
    {
        Camera.Target =
            Vector2.Lerp(Camera.Target, _targetPosition, LerpSpeed); // lerp camera position to target position
        Camera.Zoom = Camera.Zoom * (1.0f - LerpSpeed) + _targetZoom * LerpSpeed; // lerp camera zoom to target zoom

        // zoom in/zoom out camera
        var scrollDelta = GetMouseWheelMove() * (GuiManager.IsMouseOverElement() ? 0 : 1);
        if (scrollDelta < 0) ZoomOut(Speed * 0.075f);
        if (scrollDelta > 0) ZoomIn(Speed * 0.075f);

        // get speed depending if user is sprinting or not
        var speed = IsKeyDown(KeyboardKey.LeftControl) || IsKeyDown(KeyboardKey.LeftShift)
            ? Speed * 1.25f
            : Speed * 0.5f;
        speed /= Camera.Zoom;
        speed = Math.Clamp(speed, 0, Speed * 2);

        // move camera
        if (IsKeyDown(KeyboardKey.W) && !GuiManager.IsFocused) Move(0, -speed);
        if (IsKeyDown(KeyboardKey.S) && !GuiManager.IsFocused) Move(0, speed);
        if (IsKeyDown(KeyboardKey.A) && !GuiManager.IsFocused) Move(-speed, 0);
        if (IsKeyDown(KeyboardKey.D) && !GuiManager.IsFocused) Move(speed, 0);

        // adapt camera center when windows is resized
        if (IsWindowResized()) Camera.Offset = new Vector2(GetScreenWidth(), GetScreenHeight()) / 2;

        UpdateTileControls();
    }

    public void Draw()
    {
        if (!GuiManager.IsMouseOverElement() && Tiles.Tiles.TryGetTile(CurrentTile.Id, out var tile))
            tile.DrawPreview(
                World, CurrentTile,
                _tileX, _tileY
            );
    }

    private void UpdateTileControls()
    {
        var worldMousePos = GetScreenToWorld2D(GetMousePosition(), Camera) + new Vector2(Tile.RealTileSize / 2);

        _tileX = (int)(worldMousePos.X / Tile.RealTileSize);
        _tileY = (int)(worldMousePos.Y / Tile.RealTileSize);

        if (IsMouseButtonDown(MouseButton.Left) && !GuiManager.IsMouseOverElement())
            World[_tileX, _tileY] = CurrentTile;

        if (IsMouseButtonDown(MouseButton.Right) && !GuiManager.IsMouseOverElement()) World[_tileX, _tileY] = 0;

        if (IsKeyReleased(KeyboardKey.R))
        {
            CurrentTile.Flags.Rotation = CurrentTile.Flags.Rotation + 1;
            if ((int)CurrentTile.Flags.Rotation > (int)TileRotation.Right)
                CurrentTile.Flags.Rotation = TileRotation.Up;
        }

        if (IsKeyReleased(KeyboardKey.T)) CurrentTile.Flags.FlipRotation();
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

    public void PushCameraInfo()
    {
        World.PlayerPosition = Camera.Target;
        World.PlayerZoom = Camera.Zoom;
    }

    public Rectangle GetViewRectangle()
    {
        var min = GetScreenToWorld2D(new Vector2(0, 0), Camera);
        var max = GetScreenToWorld2D(new Vector2(GetScreenWidth(), GetScreenHeight()), Camera);

        return new Rectangle(min.X, min.Y, max.X - min.X, max.Y - min.Y);
    }
}