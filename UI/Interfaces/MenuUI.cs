using System.Numerics;
using System.Reflection;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Screens;

namespace BuildingGame.UI.Interfaces;

public class MenuUI : UIInterface
{
    private TextElement _title;
    private Button _playButton;
    private Button _settingsButton;
    private Button _packsButton;
    private Button _exitButton;
    private TextElement _versionText;
    private SettingsUI _settingsUi = new SettingsUI();

    public override void Initialize()
    {
        _title = new TextElement(new ElementId("menuUi", "title"))
        {
            TextSize = 36.0f,
            Text = "building game rewritten"
        };
        Elements.Add(_title);

        _playButton = new Button(new ElementId("menuUi", "playButton"))
        {
            Text = "play",
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _playButton.OnClick += () =>
        {
            ScreenManager.Switch(new GameScreen());
        };
        Elements.Add(_playButton);

        _settingsButton = new Button(new ElementId("menuUi", "settingsButton"))
        {
            Text = "settings",
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _settingsButton.OnClick += () =>
        {
            _settingsUi.Visible = !_settingsUi.Visible;
        };
        Elements.Add(_settingsButton);

        _packsButton = new Button(new ElementId("menuUi", "packsButton"))
        {
            Text = "packs",
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        Elements.Add(_packsButton);

        _exitButton = new Button(new ElementId("menuUi", "exitButton"))
        {
            Text = "exit",
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _exitButton.OnClick += () =>
        {
            Program.Running = false;
        };
        Elements.Add(_exitButton);

        var version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version();

        _versionText = new TextElement(new ElementId("menuUi", "versionText"))
        {
            Text = $"v{version.Major}.{version.Minor}.{version.Revision}",
            TextSize = 18.0f,
            Size = new Vector2(100.0f, 18.0f)
        };
        Elements.Add(_versionText);
        
        Configure();
    }
    
    public override void Configure()
    {
        _title.Position = new Vector2(12, GetYAt(0));
        _playButton.Position = new Vector2(12, GetYAt(1));
        _settingsButton.Position = new Vector2(12, GetYAt(2));
        _packsButton.Position = new Vector2(12, GetYAt(3));
        _exitButton.Position = new Vector2(12, GetYAt(4));
        _versionText.Position = new Vector2(8, GetScreenHeight() - 8 - _versionText.TextSize);
    }

    public override void Resized()
    {
        Configure();
    }
    
    private float GetYAt(int index)
    {
        var height = GetScreenHeight();

        var origin = height / 3.0f + 36.0f;
        var indexY = 32.0f * index;
        
        return origin + indexY;
    }
}