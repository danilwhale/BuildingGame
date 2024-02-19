using BuildingGame.Tiles.Packs;
using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class TilePacksScreen : Screen
{
    private TilePacksMenuUI _tilePacksMenu;

    public override void Initialize()
    {
        base.Initialize();

        TilePackManager.Load();
        _tilePacksMenu = new TilePacksMenuUI();
    }

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));

        base.Draw();
    }
}