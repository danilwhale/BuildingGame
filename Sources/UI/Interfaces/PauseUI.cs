using System.Numerics;
using BuildingGame.Translation;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Screens;

namespace BuildingGame.UI.Interfaces;

public class PauseUI : UIInterface
{
    private Panel _background;
    private Button _resumeButton;
    private Button _menuButton;
    private Button _settingsButton;

    private SettingsUI _settingsUi = new SettingsUI();

    public PauseUI()
    {
        IgnorePause = true;
    }
    
    public override void Initialize()
    {
        var translation = TranslationContainer.Default;
        
        _background = new Panel(new ElementId("pauseScreen", "background"))
        {
            Brush = new GradientBrush(Color.BLANK, Color.BLACK),
            IgnorePause = true,
            ZIndex = 100
        };
        Elements.Add(_background);

        _resumeButton = new Button(new ElementId("pauseScreen", "resumeButton"))
        {
            Text = translation.GetTranslatedName("resume_button"),
            TextSize = 24,
            TextAlignment = Alignment.CenterLeft,
            IgnorePause = true,
            ZIndex = 1,
            Parent = _background
        };
        _resumeButton.OnClick += () =>
        {
            Visible = false;
            Program.Paused = false;
        };
        Elements.Add(_resumeButton);

        _settingsButton = new Button(new ElementId("pauseScreen", "settingsButton"))
        {
            Text = translation.GetTranslatedName("settings_button"),
            TextSize = 24,
            TextAlignment = Alignment.CenterLeft,
            IgnorePause = true,
            ZIndex = 1,
            Size = new Vector2(148, 24),
            Parent = _background
        };
        _settingsButton.OnClick += () =>
        {
            _settingsUi.Visible = !_settingsUi.Visible;
        };
        Elements.Add(_settingsButton);

        _menuButton = new Button(new ElementId("pauseScreen", "menuButton"))
        {
            Text = translation.GetTranslatedName("menu_button"),
            TextSize = 24,
            TextAlignment = Alignment.CenterLeft,
            IgnorePause = true,
            ZIndex = 1,
            Size = new Vector2(148, 24),
            Parent = _background
        };
        _menuButton.OnClick += () =>
        {
            Program.Paused = false;
            ScreenManager.Switch(new MenuScreen());
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
        _settingsButton.GlobalPosition = new Vector2(buttonsOriginX, _resumeButton.GlobalPosition.Y + _resumeButton.TextSize + 16);
        _menuButton.GlobalPosition = new Vector2(buttonsOriginX, _settingsButton.GlobalPosition.Y + _settingsButton.TextSize + 16);
    }

    public override void Resized()
    {
        base.Resized();
        
        Configure();
    }

    public override void Update()
    {
        base.Update();

        if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
        {
            Visible = !Visible;
            Program.Paused = !Program.Paused;
        }
    }
}