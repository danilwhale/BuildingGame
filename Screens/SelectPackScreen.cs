using BuildingGame.GuiElements;
using BuildingGame.TilePacks;
using BuildingGame.Tiles;

namespace BuildingGame.Screens;

#nullable disable
public class SelectPackScreen : Screen
{
    private ListView _packListView;
    private HoverButton _backButton;
    private TextBlock _currentPackLabel;

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));
    }

    public override void Initialize()
    {
        _currentPackLabel = new TextBlock("currentPackLabel", "Current pack: default", Vector2.Zero, 24);
        _currentPackLabel.Color = Color.WHITE;
        _currentPackLabel.Adapt((windowSize) => 
            new Vector2(
                windowSize.X / 2 - MeasureTextEx(_currentPackLabel.Font, _currentPackLabel.Text, 24, 1).X / 2, 
                windowSize.Y - 32
            )
        );
        _currentPackLabel.ClientUpdate += () => _currentPackLabel.Text = "Current pack: " + Settings.CurrentPack;

        _packListView = new ListView("packListView");
        _packListView.PutItem("default");
        _packListView.ItemClicked += (item) =>
        {
            item = item.Replace(" (custom map)", null);
            if (item == "default")
            {
                TilePackManager.SetDefaultPack();
            }
            else
            {
                TilePackManager.ApplyPack(item);
            }

            Settings.CurrentPack = item;
        };

        foreach (var pack in TilePackManager.TilePacks)
        {
            _packListView.PutItem(pack.Name + (!pack.IsVanilla ? " (custom map)" : ""));
        }

        _backButton = new HoverButton("spsBackButton", "back", Vector2.Zero, 24);
        _backButton.Color = Color.WHITE;
        _backButton.CenterScreen();
        _backButton.Adapt(windowSize => new Vector2(_backButton.Area.x, windowSize.Y - 100));
        _backButton.Clicked += () =>
        {
            Program.currentScreen = Program.menuScreen;
        };

        Gui.PutControl(_packListView, this);
        Gui.PutControl(_backButton, this);
        Gui.PutControl(_currentPackLabel, this);
    }

    public override void Update()
    {
        
    }
}