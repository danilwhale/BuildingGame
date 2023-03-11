using BuildingGame.GuiElements;

namespace BuildingGame;

public class WorldSelectScreen : Screen
{
    List<HoverButton> _worldButtons = new List<HoverButton>();

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));
    }

    public override void Initialize()
    {
        int y = 80;
        for (int i = 0; i < 10; i++)
        {
            var idx = i;
            var worldButton = new HoverButton("world_" + i,
                $"world {i + 1}{(!IsExistsWorld(i) ? " (new)" : "")}", new Vector2(0, y), 32
            );
            worldButton.Clicked += () => LoadWorld(idx);
            worldButton.Color = Color.WHITE;
            worldButton.CenterScreen();

            Gui.PutControl(worldButton, this);
            y += 37;
        }

        var menuButton = new HoverButton("menuSelectButton", "menu", new Vector2(0, y + 60), 32);
        menuButton.Clicked += () =>
        {
            Program.currentScreen = Program.menuScreen;
        };
        menuButton.Color = Color.WHITE;
        menuButton.CenterScreen();

        Gui.PutControl(menuButton, this);
    }

    private void LoadWorld(int i)
    {
        Program.gameScreen.World.Load("saves/" + i + "/level.dat");
        Program.currentScreen = Program.gameScreen;
    }

    private bool IsExistsWorld(int i)
    {
        if (!Directory.Exists("saves/" + i))
        {
            Directory.CreateDirectory("saves/" + i);
        }

        return File.Exists("saves/" + i + "/level.dat");
    }

    public override void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            int idx = i;
            ((HoverButton)Gui.GetControl("world_" + i)).Text = $"world {idx + 1}{(!IsExistsWorld(idx) ? " (new)" : "")}";
        }
    }
}