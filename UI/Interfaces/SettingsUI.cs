using System.Numerics;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;

namespace BuildingGame.UI.Interfaces;

public class SettingsUI : UIInterface
{
    private Panel _background;
    private TextElement _skyColorLineText;
    private ColorLine _skyColorLine;
    
    public override void Initialize()
    {
        _background = new Panel(new ElementId("settings", "background"))
        {
            Brush = new SolidBrush(new Color(0, 0, 0, 100)),
            ZIndex = 200
        };
        Elements.Add(_background);

        _skyColorLineText = new TextElement(new ElementId("settings", "skyColorLineText"))
        {
            Text = "sky color (RGB): ",
            Size = new Vector2(128.0f, 20.0f),
            TextSize = 16.0f,
            ZIndex = 201
        };
        Elements.Add(_skyColorLineText);

        _skyColorLine = new ColorLine(new ElementId("settings", "skyColorLine"))
        {
            ZIndex = 201
        };
        _skyColorLine.Color = Settings.SkyColor;
        _skyColorLine.OnColorUpdate += color =>
        {
            Settings.SkyColor = color;
        };
        Elements.Add(_skyColorLine);
        
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
        _skyColorLineText.Position = _background.Position + new Vector2(8.0f, 16.0f);
        _skyColorLine.Position = _skyColorLineText.Position + _skyColorLineText.Size with { Y = 0 } + new Vector2(16.0f, 2.0f);
    }
}