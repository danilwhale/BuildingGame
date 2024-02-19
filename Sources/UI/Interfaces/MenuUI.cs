using System.Numerics;
using System.Reflection;
using BuildingGame.Translation;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Screens;

namespace BuildingGame.UI.Interfaces;

public class MenuUI : UIInterface
{
    private Button _exitButton;
    private Button _packsButton;
    private Button _playButton;
    private Button _settingsButton;
    private readonly SettingsUI _settingsUi = new();
    private TextElement _title;
    private TextElement _versionText;

    public override void Initialize()
    {
        var translation = TranslationContainer.Default;

        _title = new TextElement(new ElementId("menuUi", "title"))
        {
            TextSize = 36.0f,
            Text = translation.GetTranslatedName("title")
        };
        Elements.Add(_title);

        _playButton = new Button(new ElementId("menuUi", "playButton"))
        {
            Text = translation.GetTranslatedName("play_button"),
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _playButton.OnClick += () => { ScreenManager.Switch(new WorldSelectionScreen()); };
        Elements.Add(_playButton);

        _settingsButton = new Button(new ElementId("menuUi", "settingsButton"))
        {
            Text = translation.GetTranslatedName("settings_button"),
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _settingsButton.OnClick += () => { _settingsUi.Visible = !_settingsUi.Visible; };
        Elements.Add(_settingsButton);

        _packsButton = new Button(new ElementId("menuUi", "packsButton"))
        {
            Text = translation.GetTranslatedName("packs_button"),
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _packsButton.OnClick += () => { ScreenManager.Switch(new TilePacksScreen()); };
        Elements.Add(_packsButton);

        _exitButton = new Button(new ElementId("menuUi", "exitButton"))
        {
            Text = translation.GetTranslatedName("exit_button"),
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f)
        };
        _exitButton.OnClick += () => { Program.Running = false; };
        Elements.Add(_exitButton);

        var version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version();

        _versionText = new TextElement(new ElementId("menuUi", "versionText"))
        {
            Text = string.Format(translation.GetTranslatedName("version_format"), version.Major, version.Minor,
                version.Revision),
            TextSize = 18.0f,
            Size = new Vector2(100.0f, 20.0f)
        };
        Elements.Add(_versionText);

        Configure();
    }

    public override void Configure()
    {
        _title.GlobalPosition = new Vector2(12, GetYAt(0));
        _playButton.GlobalPosition = new Vector2(12, GetYAt(1));
        _settingsButton.GlobalPosition = new Vector2(12, GetYAt(2));
        _packsButton.GlobalPosition = new Vector2(12, GetYAt(3));
        _exitButton.GlobalPosition = new Vector2(12, GetYAt(4));
        _versionText.GlobalPosition = new Vector2(8, GetScreenHeight() - 8 - _versionText.TextSize);
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