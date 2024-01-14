using System.Numerics;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class PauseUI : UIInterface
{
    private Panel _background;
    private Button _resumeButton;
    private Button _menuButton;
    private Button _settingsButton;

    public PauseUI()
    {
        IgnorePause = true;
    }
    
    public override void Initialize()
    {
        base.Initialize();

        _background = new Panel("pauseScreen::background")
        {
            Brush = new GradientBrush(Color.BLANK, Color.BLACK),
            IgnorePause = true,
            ZIndex = 100
        };
        Elements.Add(_background);

        _resumeButton = new Button("pauseScreen::resumeButton")
        {
            Text = "resume",
            TextSize = 24,
            TextAlignment = Alignment.CenterLeft,
            IgnorePause = true,
            ZIndex = 101
        };
        _resumeButton.OnClick += () =>
        {
            Visible = false;
            Program.Paused = false;
        };
        Elements.Add(_resumeButton);

        _settingsButton = new Button("pauseScreen::settingsButton")
        {
            Text = "settings",
            TextSize = 24,
            TextAlignment = Alignment.CenterLeft,
            IgnorePause = true,
            ZIndex = 101
        };
        Elements.Add(_settingsButton);

        _menuButton = new Button("pauseScreen::menuButton")
        {
            Text = "menu",
            TextSize = 24,
            TextAlignment = Alignment.CenterLeft,
            IgnorePause = true,
            ZIndex = 101
        };
        Elements.Add(_menuButton);
        
        Configure();
        Visible = false;
    }

    public override void Configure()
    {
        base.Configure();

        _background.Area = new Rectangle(-1, -1, GetScreenWidth() + 2, GetScreenHeight() + 2);

        float buttonsOriginX = 16;
        float buttonsOriginY = GetScreenHeight() / 2 - _resumeButton.TextSize;
        
        _resumeButton.Area = new Rectangle(buttonsOriginX, buttonsOriginY, 148, 24);
        _settingsButton.Area = new Rectangle(
            buttonsOriginX, _resumeButton.Position.Y + _resumeButton.TextSize + 16, 
            148, 24
            );
        _menuButton.Area = new Rectangle(
            buttonsOriginX, _settingsButton.Position.Y + _settingsButton.TextSize + 16,
            148, 24);
    }

    public override void Resized()
    {
        base.Resized();
        
        Configure();
    }

    public override void Update()
    {
        base.Update();

        if (IsKeyReleased(KeyboardKey.KEY_ESCAPE))
        {
            Visible = !Visible;
            Program.Paused = !Program.Paused;
        }
    }
}