using System.Diagnostics;
using System.Reflection;


namespace BuildingGame.Screens;

public class MenuScreen : Screen
{
    private Dictionary<string, Control> _localControls = new Dictionary<string, Control>();
    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));
    }

    BackgroundBlock settingsPanel = null!;
    TextBlock bgColorTitle = null!;
    RgbBoxLine bgColorLine = null!;
    CheckBox physicsCheckBox = null!;
    CheckBox enableInfectionCheckBox = null!;

    public override void Initialize()
    {
        // create gui elements
        settingsPanel = new BackgroundBlock("settingsPanel", (ColorBrush)new Color(0, 0, 0, 100))
        {
            Area = new Rectangle(50, 50, Program.WIDTH - 50 * 2, Program.HEIGHT - 50 * 2),
            Active = false,
            ZIndex = 100
        };
        settingsPanel.ClientUpdate += () =>
        {
            settingsPanel.Area = new Rectangle(50, 50, Program.WIDTH - 50 * 2, Program.HEIGHT - 50 * 2);
            Settings.SkyColor = bgColorLine.ExportColor();
            Settings.EnablePhysics = physicsCheckBox.Checked;
            Settings.EnableInfectionBlock = enableInfectionCheckBox.Checked;
        };

        bgColorTitle = new TextBlock(
            "bgColorTitle", "sky color (rgb): ",
            new Vector2(settingsPanel.Area.x + 8, settingsPanel.Area.y + 16),
            18
        );
        bgColorTitle.ClientUpdate += () =>
        {
            var point = new Vector2(settingsPanel.Area.x + 8, settingsPanel.Area.y + 16);
            bgColorTitle.Area = new Rectangle(
                point.X,
                point.Y,
                bgColorTitle.Area.width,
                bgColorTitle.Area.height
            );
        };
        bgColorTitle.Color = Color.WHITE;
        settingsPanel.Children.Add(bgColorTitle);

        bgColorLine = new RgbBoxLine("bgColorLine",
            new Vector2(bgColorTitle.Area.x + bgColorTitle.Area.width + 8, bgColorTitle.Area.y),
            Color.SKYBLUE
        );
        bgColorLine.ImportColor(Settings.SkyColor);
        settingsPanel.Children.Add(bgColorLine);

        physicsCheckBox = new CheckBox("physicsCheckBox", "enable dynamic tiles (can cause fps drops)",
            Vector2.Zero,
            18
        );
        physicsCheckBox.Adapt(_ => new Vector2(settingsPanel.Area.x + 8, settingsPanel.Area.y + 16 + 8 + 18));
        physicsCheckBox.Checked = Settings.EnablePhysics;
        settingsPanel.Children.Add(physicsCheckBox);

        enableInfectionCheckBox = new CheckBox("enableInfectionCheckBox", "enable infection tiles",
            Vector2.Zero, 18);
        enableInfectionCheckBox.Adapt(_ => new Vector2(settingsPanel.Area.x + 8, settingsPanel.Area.y + (16 + 8 + 18) * 2));
        enableInfectionCheckBox.Checked = Settings.EnableInfectionBlock;
        settingsPanel.Children.Add(enableInfectionCheckBox);

        var title = new TextBlock("title", "building game", Vector2.Zero, 36);
        title.Adapt((windowSize) => new Vector2(12, CalculateYForButton(windowSize, 0)));
        title.Color = Color.WHITE;

        var playButton = new HoverButton("playButton", "play", 
            Vector2.Zero, 24);
        playButton.Adapt((windowSize) => new Vector2(12, CalculateYForButton(windowSize, 1)));
        playButton.Color = Color.WHITE;
        playButton.Clicked += () =>
        {
            Program.currentScreen = Program.worldSelectScreen;
            settingsPanel.Active = false;
        };

        var settingsButton = new HoverButton("settingsButton", "settings", 
            Vector2.Zero, 24);
        settingsButton.Adapt((windowSize) => new Vector2(12, CalculateYForButton(windowSize, 2)));
        settingsButton.Color = Color.WHITE;
        settingsButton.Clicked += () =>
        {
            if (!(settingsPanel.Active && CheckCollisionPointRec(GetMousePosition(), settingsPanel.Area)))
            {
                settingsPanel.Active = !settingsPanel.Active;
            }
        };

        var packsButton = new HoverButton("packsButton", "packs",
            Vector2.Zero, 24);
        packsButton.Adapt((windowSize) => new Vector2(12, CalculateYForButton(windowSize, 3)));
        packsButton.Color = Color.WHITE;
        packsButton.Clicked += () => Program.currentScreen = Program.selectPackScreen;

        var exitButton = new HoverButton("exitButton", "exit", 
            Vector2.Zero, 24
        );
        exitButton.Adapt((windowSize) => new Vector2(12, CalculateYForButton(windowSize, 4)));
        exitButton.Color = Color.WHITE;
        exitButton.Clicked += () =>
        {
            Program.mustClose = true;
        };

        var versionBlock = new TextBlock("versionBlock", $"v{Program.version}",
            Vector2.Zero, 18
        );
        versionBlock.Adapt((windowSize) => new Vector2(8, windowSize.Y - 8 - 18));
        versionBlock.Color = Color.WHITE;

        Gui.PutControl(settingsPanel, this, true);
        Gui.PutControl(settingsButton, this);
        Gui.PutControl(packsButton, this);
        Gui.PutControl(playButton, this);
        Gui.PutControl(exitButton, this);
        Gui.PutControl(title, this);
        Gui.PutControl(versionBlock, this);

    }

    public override void Update()
    {

    }

    private float CalculateYForButton(Vector2 windowSize, int buttonNumber)
    {
        return windowSize.Y / 3 + 36 + (20 + 24 / 2) * buttonNumber;
    }
}