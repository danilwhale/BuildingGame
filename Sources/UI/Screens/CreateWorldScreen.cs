using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;
public class CreateWorldScreen : Screen
{
    private CreateWorldUI _ui;

    public override void Initialize()
    {
        base.Initialize();

        _ui = new CreateWorldUI();
    }

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));

        base.Draw();
    }
}
