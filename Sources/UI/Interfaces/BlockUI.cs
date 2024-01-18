using System.Numerics;
using BuildingGame.Tiles;
using BuildingGame.Translation;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class BlockUI : UIInterface
{
    private Panel _background = null!;
    private TextElement _menuTitle = null!;
    private Button[] _tileButtons = null!;

    public override void Initialize()
    {
        base.Initialize();

        var translation = TranslationContainer.Default;
        
        _background = new Panel(new ElementId("blockMenu", "background"))
        {
            Brush = new GradientBrush(new Color(0, 0, 25, 100), new Color(0, 0, 0, 200)),
            Size = new Vector2(680, 420),
            ZIndex = -1
        };
        Elements.Add(_background);

        _menuTitle = new TextElement(new ElementId("blockMenu", "title"))
        {
            Text = translation.GetTranslatedName("block_ui_title"),
            TextColor = Color.WHITE,
            TextSize = 32,
            TextAlignment = Alignment.Center,
            Parent = _background,
            ZIndex = 1
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
        
        float x = 20;
        float y = 40;

        for (int i = 0; i < tiles.Length; i++)
        {
            float aspectX = Tile.TileSize / (Tile.TileSize * tiles[i].Size.X);
            float aspectY = Tile.TileSize / (Tile.TileSize * tiles[i].Size.Y);

            float ratio = aspectX < aspectY ? aspectX : aspectY;
            
            var tileButton = _tileButtons[i] = new Button(new ElementId("blockMenu", "tile_" + i))
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
                ShowHoverText = false,
                TooltipText = translation.GetTranslatedName(tiles[i].TranslationKey),
                Parent = _background,
                ZIndex = 1,
                LocalPosition = new Vector2(x, y)
            };
            
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
            
            Elements.Add(tileButton);
        }
        
        Configure();
        
        Visible = false;
    }

    public override void Configure()
    {
        base.Configure();

        Vector2 screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
        
        _background.GlobalPosition = screenSize / 2 - _background.Size / 2;
        
        _menuTitle.Size = _background.Size with { Y = 38 };
    }

    public override void Resized()
    {
        base.Resized();

        Configure();
    }

    public override void Update()
    {
        base.Update();

        if (IsKeyPressed(KeyboardKey.KEY_B)) Visible = !Visible;
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && !_background.IsUnderMouse() &&
            GuiManager.GetElementUnderMouse()?.Id != "gameHud::tileMenuButton") // 🤟😏
            Visible = false;
    }
}