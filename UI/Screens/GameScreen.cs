using BuildingGame.Tiles;
using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class GameScreen : Screen
{
    private World _world;
    private Player _player;
    private BlockUI _blockUi;
    private GameUI _ui;
    private PauseUI _pause;

    public override void Initialize()
    {
        base.Initialize();
        
        _world = new World();
        _world.Load();

        _player = new Player(_world, _world.PlayerPosition, _world.PlayerZoom, 50, 0.1f);

        _blockUi = new BlockUI();
        _ui = new GameUI(_blockUi);
        _pause = new PauseUI();
    }

    public override void Update()
    {
        base.Update();
        
        if (IsKeyPressed(KeyboardKey.KEY_R))
            Tiles.Tiles.Reload();
        
        if (!Program.Paused)
        {
            _player.Update();
            _world.Update();
        }
    }

    public override void Draw()
    {
        ClearBackground(Settings.SkyColor);

        BeginMode2D(_player.Camera);
        {
            _world.Draw(_player);
            _player.Draw();
        }
        EndMode2D();
        
        base.Draw();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (!disposing) return;
        
        _player.PushCameraInfo();
        _world.Save();
    }
}