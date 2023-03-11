using System.Diagnostics;
using System.Reflection;
using BuildingGame.GuiElements;
using BuildingGame.GuiElements.Brushes;

namespace BuildingGame;

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
            Settings.SkyColor = bgColorLine.ExportColor();
            Settings.EnablePhysics = physicsCheckBox.Checked;
        };

        bgColorTitle = new TextBlock(
            "bgColorTitle", "sky color (rgb): ",
            new Vector2(settingsPanel.Area.x + 8, settingsPanel.Area.y + 16),
            18
        );
        bgColorTitle.Color = Color.WHITE;
        settingsPanel.Children.Add(bgColorTitle);

        bgColorLine = new RgbBoxLine("bgColorLine",
            new Vector2(bgColorTitle.Area.x + bgColorTitle.Area.width + 8, bgColorTitle.Area.y),
            Color.SKYBLUE
        );
        bgColorLine.ImportColor(Settings.SkyColor);
        settingsPanel.Children.Add(bgColorLine);

        physicsCheckBox = new CheckBox("physicsCheckBox", "enable dynamic tiles (can cause fps drops)",
            new Vector2(settingsPanel.Area.x + 8, settingsPanel.Area.y + 16 + 8 + 18),
            18
        );
        physicsCheckBox.Checked = Settings.EnablePhysics;
        settingsPanel.Children.Add(physicsCheckBox);

        var title = new TextBlock("title", "building game", new Vector2(12, CalculateYForButton(0)), 36);
        title.Color = Color.WHITE;

        var playButton = new HoverButton("playButton", "play", 
            new Vector2(12, CalculateYForButton(1)), 24
        );
        playButton.Color = Color.WHITE;
        playButton.Clicked += () =>
        {
            Program.currentScreen = Program.worldSelectScreen;
            settingsPanel.Active = false;
        };

        var settingsButton = new HoverButton("settingsButton", "settings", 
            new Vector2(12, CalculateYForButton(2)), 24
        );
        settingsButton.Color = Color.WHITE;
        settingsButton.Clicked += () =>
        {
            if (!(settingsPanel.Active && CheckCollisionPointRec(GetMousePosition(), settingsPanel.Area)))
            {
                settingsPanel.Active = !settingsPanel.Active;
            }
        };

        var exitButton = new HoverButton("exitButton", "exit", 
            new Vector2(12, CalculateYForButton(3)), 24
        );
        exitButton.Color = Color.WHITE;
        exitButton.Clicked += () =>
        {
            Program.mustClose = true;
        };


        string dir = AppContext.BaseDirectory;
#if DEBUG
        dir = Path.Join(dir, "BuildingGame.dll");
#else
        dir = Path.Join(dir, "BuildingGame.exe");
#endif
        var ver = FileVersionInfo.GetVersionInfo(dir).FileVersion!;
        var versionBlock = new TextBlock("versionBlock", $"v{ver}",
            new Vector2(8, Program.HEIGHT - 8 - 18), 18
        );
        versionBlock.Color = Color.WHITE;

        Gui.PutControl(settingsPanel, this, true);
        Gui.PutControl(settingsButton, this);
        Gui.PutControl(playButton, this);
        Gui.PutControl(exitButton, this);
        Gui.PutControl(title, this);
        Gui.PutControl(versionBlock, this);

    }

    public override void Update()
    {

    }

    private float CalculateYForButton(int buttonNumber)
    {
        return Program.HEIGHT / 3 + 36 + (20 + 24 / 2) * buttonNumber;
    }
}