using BuildingGame.Tiles;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class GameHud : UIInterface
{
    private const float TileButtonOffset = Tile.RealTileSize / 2;
    private const float TileButtonSize = Tile.RealTileSize * 2;

    private BlockMenu _menu;
    
    private Button _tileMenuButton = null!;

    public GameHud(BlockMenu menu)
    {
        _menu = menu;
    }

    public override void Initialize()
    {
        base.Initialize();

        _tileMenuButton = new Button("tileMenuButton")
        {
            BackgroundBrush = new TextureBrush(Resources.GetTexture("Atlas.png"))
            {
                CropArea = new Rectangle(0, 0, Tile.TileSize, Tile.TileSize)
            },
            ShowHoverText = false
        };
        _tileMenuButton.OnClick += () =>
        {
            _menu.Visible = !_menu.Visible;
        };

        Elements.Add(_tileMenuButton);

        Configure();
    }

    public override void Update()
    {
        base.Update();

        var brush = (TextureBrush)_tileMenuButton.BackgroundBrush!;
        var tile = Tiles.Tiles.GetTile(Player.CurrentTile);
        (brush.CropArea.x, brush.CropArea.y) = (tile.TexCoord.X * Tile.TileSize, tile.TexCoord.Y * Tile.TileSize);
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
