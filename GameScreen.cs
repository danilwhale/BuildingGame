using BuildingGame.GuiElements;
using BuildingGame.GuiElements.Brushes;

namespace BuildingGame;

public class GameScreen : Screen
{
    const float CAMERA_SPEED = 5;

    static byte _currentType = 1;

    Camera2D _camera;
    bool _cameraRunning;

    World _world = null!;
    public World World => _world;

    bool flipTile = false;
    float tileRot = 0;

    int mx = 0;
    int my = 0;

    public override void Draw()
    {
        BeginMode2D(_camera);
        ClearBackground(Settings.SkyColor);

        DrawRectangleLinesEx(
            new Rectangle(-26, -26, World.CHUNK_AREA * Chunk.SIZE * 48, World.CHUNK_AREA * Chunk.SIZE * 48),
            2f, Color.RED
        );
        _world.Draw();
        if (!Gui.IsMouseOverControl)
            Tile.DefaultTiles[_currentType - 1].Draw(mx, my, new TileFlags(tileRot, flipTile), new Color(255, 255, 255, 120));

        EndMode2D();
    }

    public override void Initialize()
    {
        _world = new World();

        _camera = new Camera2D
        {
            offset = new Vector2(Program.WIDTH / 2, Program.HEIGHT / 2),
            target = new Vector2(GetRandomValue(0, World.CHUNK_AREA * Chunk.SIZE * 48), GetRandomValue(0, World.CHUNK_AREA * Chunk.SIZE * 48)),
            zoom = 1,
            rotation = 0
        };

        CreateGui();
    }

    public override void Update()
    {
        #region Camera Movement
        _cameraRunning = IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT);

        float vx = IsKeyDown(KeyboardKey.KEY_D) - IsKeyDown(KeyboardKey.KEY_A);
        float vy = IsKeyDown(KeyboardKey.KEY_S) - IsKeyDown(KeyboardKey.KEY_W);

        _camera.target += new Vector2(vx, vy) * (_cameraRunning ? CAMERA_SPEED * 2 : CAMERA_SPEED);
        _world.SpawnPos = _camera.target;
        #endregion

        #region Tile Placement
        Vector2 wmPos = GetScreenToWorld2D(GetMousePosition(), _camera);

        mx = (int)(Math.Round((float)((wmPos.X) / 48)));
        my = (int)(Math.Round((float)((wmPos.Y) / 48)));

        if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && !Gui.IsMouseOverControl)
        {
            _world.PlaceTile(mx, my, new TileInfo(_currentType, new TileFlags(tileRot, flipTile)));
        }
        if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT) && !Gui.IsMouseOverControl)
        {
            _world.PlaceTile(mx, my, 0);
        }
        #endregion

        #region Tile Flags Manipulation
        if (IsKeyPressed(KeyboardKey.KEY_R))
        {
            if (tileRot >= 360) tileRot = 0;
            tileRot += 90;
        }
        if (IsKeyPressed(KeyboardKey.KEY_F))
        {
            flipTile = !flipTile;
        }
        #endregion

        #region Tile Menu shortcut
        if (IsKeyPressed(KeyboardKey.KEY_B))
        {
            var bgPanel = Gui.GetControl("bgPanel");
            bgPanel.Active = !bgPanel.Active;
        }
        #endregion

        #region Camera Zoom
        var mdelta = Vector2Clamp(GetMouseWheelMoveV(), -Vector2.One, Vector2.One);
        if (IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
        {
            _camera.zoom += ((float)GetMouseWheelMove() * 0.05f);

            if (_camera.zoom > 3.0f) _camera.zoom = 3.0f;
            else if (_camera.zoom < 0.1f) _camera.zoom = 0.1f;

        }
        #endregion

        _world.Update();
    }

    void CreateGui()
    {
        var bgPanel = new BackgroundBlock("bgPanel", 
            new GradientBrush(new Color(0, 0, 25, 100), new Color(0, 0, 0, 200))
        )
        {
            Area = new Rectangle(60, 90, Program.WIDTH - 60 * 2, Program.HEIGHT - 90 * 2),
            Active = false
        };
        bgPanel.ClientUpdate += () =>
        {
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE)) bgPanel.Active = false;
        };

        var pausePanel = new BackgroundBlock("pausePanel", bgPanel.Background)
        {
            Area = new Rectangle(0, 0, Program.WIDTH, Program.HEIGHT),
            Active = false
        };
        pausePanel.ClientUpdate += () =>
        {
            if (Gui.GetControl("settingsPanel").Active && IsKeyPressed(KeyboardKey.KEY_ESCAPE)) Gui.GetControl("settingsPanel").Active = false;
            else if (IsKeyPressed(KeyboardKey.KEY_ESCAPE) && !bgPanel.Active) pausePanel.Active = !pausePanel.Active;
            
        };

        var resumeGameButton = new HoverButton("resumeGameButton", "resume", Vector2.Zero, 24)
        {
            Area = new Rectangle(Program.WIDTH / 2, Program.HEIGHT / 3, 150, 24),
            Color = Color.WHITE
        };
        resumeGameButton.Clicked += () =>
        {
            pausePanel.Active = false;
            Gui.GetControl("settingsPanel").Active = false;
        };
        resumeGameButton.CenterScreen();
        pausePanel.Children.Add(resumeGameButton);

        var menuGameButton = new HoverButton("menuGameButton", "menu", Vector2.Zero, 24)
        {
            Area = new Rectangle(Program.WIDTH / 2, Program.HEIGHT / 3 + 8 + 24, 150, 24),
            Color = Color.WHITE
        };
        menuGameButton.Clicked += () =>
        {
            pausePanel.Active = false;
            Gui.GetControl("settingsPanel").Active = false;
            _world.Save();
            Program.currentScreen = Program.menuScreen;
        };
        menuGameButton.CenterScreen();
        pausePanel.Children.Add(menuGameButton);

        var settingsGameButton = new HoverButton("settingsGameButton", "settings", Vector2.Zero, 24)
        {
            Area = new Rectangle(Program.WIDTH / 2, Program.HEIGHT / 3 + 8 + 24 + 56, 150, 24),
            Color = Color.WHITE
        };
        settingsGameButton.Clicked += () =>
        {
            Gui.GetControl("settingsPanel").Active = !Gui.GetControl("settingsPanel").Active;
        };
        settingsGameButton.CenterScreen();
        pausePanel.Children.Add(settingsGameButton);

        var tooltip = new Tooltip("tileTooltip");
        tooltip.Background = new GradientBrush(new Color(0, 0, 0, 50), new Color(16, 0, 16, 127));

        #region Tile Preview (+ Tile Menu shortcut)
        var texImg = new ImageArea("texImg", Program.atlas, new Rectangle(0, 0, 16, 16), new Color(255, 255, 255, 200))
        {
            Area = new Rectangle(Program.WIDTH - 64 - 16, 16, 64, 64)
        };
        texImg.ClientUpdate += () =>
        {
            var tile = Tile.DefaultTiles[_currentType - 1];
            texImg.ImageSourceRect = new Rectangle(tile.AtlasOffset.X * 16, tile.AtlasOffset.Y * 16, 16, 16);;
        };
        texImg.Clicked += () =>
        {
            bgPanel.Active = !bgPanel.Active;
        };
        #endregion

        #region Tile Menu Generation
        float x = bgPanel.Area.x + 20;
        float y = bgPanel.Area.y + 60;

        var tileMenuTitle = new TextBlock("tileMenuTitle", "Select a tile", 
            new Vector2(bgPanel.Area.x, bgPanel.Area.y + 6), 32
        );
        tileMenuTitle.Active = true;
        tileMenuTitle.Area = new Rectangle(bgPanel.Area.x, bgPanel.Area.y + 6, bgPanel.Area.width, 32);
        tileMenuTitle.Color = Color.WHITE;
        // Console.WriteLine(tileMenuTitle.Text);
        tileMenuTitle.Center();
        bgPanel.Children.Add(tileMenuTitle);

        for (byte i = 0; i < Tile.DefaultTiles.Length; i++)
        {
            var tile = Tile.DefaultTiles[i];
            byte idx = (byte)(i + 1);
            var btn = new ImageArea("tile_" + i, Program.atlas, 
                new Rectangle(tile.AtlasOffset.X * 16, tile.AtlasOffset.Y * 16, 16, 16), Color.WHITE
            )
            {
                Area = new Rectangle(x, y, 48, 48),
            };
            btn.Clicked += () =>
            {
                _currentType = idx;
                bgPanel.Active = false;
            };

            bgPanel.Children.Add(btn);
            tooltip.Triggers.Add(btn, Tile.GetTile(idx).DisplayName);

            if (x >= bgPanel.Area.x + bgPanel.Area.width - 20 * 4)
            {
                x = bgPanel.Area.x + 20;
                y += 48 + 5;
                continue;
            }



            x += 48 + 5;
        }
        #endregion

        Gui.PutControl(texImg, this);
        Gui.PutControl(bgPanel, this);
        Gui.PutControl(tooltip, this);
        Gui.PutControl(pausePanel, this);
    }


}