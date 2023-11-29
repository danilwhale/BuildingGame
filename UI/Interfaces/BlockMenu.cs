using System.Numerics;
using BuildingGame.Tiles;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class BlockMenu : UIInterface
{
    private Panel _background = null!;
    private TextElement _menuTitle = null!;
    private Button[] _tileButtons = null!;

    public override void Initialize()
    {
        base.Initialize();
        
        _background = new Panel("blockMenu::background")
        {
            Brush = new GradientBrush(new Color(0, 0, 25, 100), new Color(0, 0, 0, 200)),
            ZIndex = 10
        };
        Elements.Add(_background);

        _menuTitle = new TextElement("blockMenu::title")
        {
            Text = "Select a tile",
            TextColor = WHITE,
            TextSize = 32,
            TextAlignment = Alignment.Center
        };
        Elements.Add(_menuTitle);

        // new TextBox("tv3ewds")
        // {
        //     Position = new Vector2(86, 86),
        //     TextColor = BLACK,
        //     MaxCharacters = 128
        // };

        Tile[] tiles = Tiles.Tiles.GetTiles();
        _tileButtons = new Button[tiles.Length];

        for (int i = 0; i < tiles.Length; i++)
        {
            float aspectX = Tile.TileSize / (Tile.TileSize * tiles[i].Size.X);
            float aspectY = Tile.TileSize / (Tile.TileSize * tiles[i].Size.Y);

            float ratio = aspectX < aspectY ? aspectX : aspectY;
            
            _tileButtons[i] = new Button("blockMenu::tile_" + i)
            {
                Size = new Vector2(1) * Tile.RealTileSize,
                BackgroundBrush = new TextureBrush(Resources.GetTexture("Atlas.png"))
                {
                    CropArea = new Rectangle(
                        tiles[i].TexCoord.X * Tile.TileSize, 
                        tiles[i].TexCoord.Y * Tile.TileSize, 
                        Tile.TileSize,
                        Tile.TileSize
                        )
                },
                ShowHoverText = false
            };
            
            Elements.Add(_tileButtons[i]);
        }

        Visible = false;
        Configure();
    }

    public override void Configure()
    {
        base.Configure();

        Vector2 screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());

        _background.Size = new Vector2(680, 420);
        _background.Position = screenSize / 2 - _background.Size / 2;

        _menuTitle.Position = _background.Position;
        _menuTitle.Size = _background.Size with { Y = 38 };

        float x = 20;
        float y = 40;

        for (int i = 0; i < _tileButtons.Length; i++)
        {
            Button tileButton = _tileButtons[i];
            Tile tile = Tiles.Tiles.GetTile((byte)(i + 1));

            tileButton.Position = _background.Position + new Vector2(x, y);

            int ii = i;
            tileButton.OnClick += () =>
            {
                Player.CurrentTile = (byte)(ii + 1);
                Visible = false;
            };
            
            x += Tile.RealTileSize + 8;

            if (x > _background.Size.X - Tile.RealTileSize)
            {
                x = 20;
                y += Tile.RealTileSize + 8;
            }
        }
    }

    public override void Resized()
    {
        base.Resized();

        Configure();
    }

    public override void Update()
    {
        base.Update();

        if (IsKeyReleased(KeyboardKey.KEY_B)) Visible = !Visible;
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && !_background.IsHovered())
            Visible = false;
    }
}