using System.Numerics;
using BuildingGame.Translation;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class SettingsUI : UIInterface
{
    private Panel _background;
    private TextElement _skyColorLineText;
    private ColorLine _skyColorLine;
    private CheckBox _enableDynamicTilesBox;
    private CheckBox _enableInfectionTileBox;
    
    public override void Initialize()
    {
        var translation = TranslationContainer.Default;
        
        _background = new Panel(new ElementId("settings", "background"))
        {
            Brush = new SolidBrush(new Color(0, 0, 0, 100)),
            ZIndex = 200
        };
        Elements.Add(_background);

        _skyColorLineText = new TextElement(new ElementId("settings", "skyColorLineText"))
        {
            Text = translation.GetTranslatedName("sky_color_line"),
            Size = new Vector2(128.0f, 20.0f),
            TextSize = 16.0f,
            ZIndex = 1,
            Parent = _background
        };
        Elements.Add(_skyColorLineText);

        _skyColorLine = new ColorLine(new ElementId("settings", "skyColorLine"))
        {
            Color = Settings.SkyColor,
            DefaultColor = Color.SkyBlue,
            Parent = _skyColorLineText,
            LocalPosition = new Vector2(_skyColorLineText.Size.X + 16.0f, 2.0f)
        };
        _skyColorLine.OnColorUpdate += color =>
        {
            Settings.SkyColor = color;
        };
        Elements.Add(_skyColorLine);

        _enableDynamicTilesBox = new CheckBox(new ElementId("settings", "enableDynamicTilesBox"))
        {
            Text = translation.GetTranslatedName("enable_dynamic_tiles"),
            Size = new Vector2(512.0f, 20.0f),
            TextSize = 16.0f,
            ZIndex = 1,
            Checked = Settings.EnableDynamicTiles,
            Parent = _background
        };
        _enableDynamicTilesBox.OnCheck += @checked =>
        {
            Settings.EnableDynamicTiles = @checked;
        };
        Elements.Add(_enableDynamicTilesBox);

        _enableInfectionTileBox = new CheckBox(new ElementId("settings", "enableInfectionTileBox"))
        {
            Text = translation.GetTranslatedName("enable_infection_tile"),
            Size = new Vector2(512.0f, 20.0f),
            TextSize = 16.0f,
            ZIndex = 1,
            Checked = Settings.EnableInfectionTile,
            Parent = _background
        };
        _enableInfectionTileBox.OnCheck += @checked =>
        {
            Settings.EnableInfectionTile = @checked;
        };
        Elements.Add(_enableInfectionTileBox);
        
        Configure();
        Visible = false;
    }

    public override void Resized()
    {
        Configure();
    }

    public override void Configure()
    {
        _background.Area = new Rectangle(50.0f, 50.0f, GetScreenWidth() - 100.0f, GetScreenHeight() - 100.0f);
        _skyColorLineText.GlobalPosition = _background.GlobalPosition + new Vector2(8.0f, 16.0f);
        _enableDynamicTilesBox.GlobalPosition = _skyColorLineText.GlobalPosition + new Vector2(0.0f, _skyColorLineText.Size.Y + 8.0f);
        _enableInfectionTileBox.GlobalPosition = _enableDynamicTilesBox.GlobalPosition +
                                                 new Vector2(0.0f, _enableInfectionTileBox.Size.Y + 8.0f);
    }
}