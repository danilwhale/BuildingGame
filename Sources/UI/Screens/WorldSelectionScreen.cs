using BuildingGame.Tiles;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class WorldSelectionScreen : Screen
{
    private WorldMenuUI _ui;

    public override void Initialize()
    {
        base.Initialize();

        WorldManager.ReadWorlds();
        _ui = new WorldMenuUI();
    }

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));
        
        base.Draw();
    }
}