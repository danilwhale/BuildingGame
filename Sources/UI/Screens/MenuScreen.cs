using BuildingGame.UI.Elements;
using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class MenuScreen : Screen
{
    private MenuUI _ui;
    
    public override void Initialize()
    {
        base.Initialize();

        _ui = new MenuUI();
    }

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));
        
        base.Draw();
    }
}