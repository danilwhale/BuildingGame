using System.Numerics;
using BuildingGame.Tiles;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class GameUI : UIInterface
{
    private const float TileButtonOffset = Tile.RealTileSize / 2;
    private const float TileButtonSize = Tile.RealTileSize * 2;

    private readonly BlockUI _blockUi;

    private Button _tileMenuButton = null!;
    private Tooltip _tooltip;

    public GameUI(BlockUI blockUi)
    {
        _blockUi = blockUi;
    }

    public override void Initialize()
    {
        base.Initialize();

        _tileMenuButton = new Button(new ElementId("gameHud", "tileMenuButton"))
        {
            BackgroundBrush = new TextureBrush(Resources.GetTexture("Atlas.png"))
            {
                CropArea = new Rectangle(0, 0, Tile.TileSize, Tile.TileSize)
            },
            ShowHoverText = false
        };
        _tileMenuButton.OnClick += () => { _blockUi.Visible = !_blockUi.Visible; };
        Elements.Add(_tileMenuButton);

        _tooltip = new Tooltip(new ElementId("gameHud", "tooltip"))
        {
            TextSize = 16.0f,
            Size = new Vector2(0.0f, 36.0f)
        };
        Elements.Add(_tooltip);

        Configure();
    }

    public override void Update()
    {
        base.Update();

        if (!_blockUi.Visible && IsKeyPressed(KeyboardKey.Escape)) _blockUi.Visible = false;

        if (!Tiles.Tiles.TryGetTile(Player.CurrentTile, out var tile)) return;

        var brush = (TextureBrush)_tileMenuButton.BackgroundBrush!;
        (brush.CropArea.X, brush.CropArea.Y) = (tile.TexCoord.X * Tile.TileSize, tile.TexCoord.Y * Tile.TileSize);
    }

    public override void Configure()
    {
        base.Configure();

        _tileMenuButton.Area = new Rectangle(
            GetScreenWidth() - TileButtonSize - TileButtonOffset, TileButtonOffset,
            TileButtonSize, TileButtonSize
        );
    }

    public override void Resized()
    {
        base.Resized();

        Configure();
    }
}