using BuildingGame.Tiles;
using BuildingGame.Tiles.Data;
using BuildingGame.UI.Interfaces;

namespace BuildingGame.UI.Screens;

public class GameScreen : Screen
{
    private BlockUI _blockUi;
    private WorldInfo _info;
    private PauseUI _pause;

    private Player _player;
    private TimeSpan _playTime;
    private GameUI _ui;
    private World _world;

    public GameScreen(string worldPath)
    {
        _info = WorldManager.Find(worldPath);
        _playTime = _info.Info.PlayTime;
    }

    public override void Initialize()
    {
        base.Initialize();

        _world = new World();
        WorldManager.LoadWorld(ref _world, _info.Path);

        _player = new Player(_world, _world.PlayerPosition, _world.PlayerZoom, 50, 0.1f);

        _blockUi = new BlockUI();
        _ui = new GameUI(_blockUi);
        _pause = new PauseUI(_blockUi);
    }

    public override void Update()
    {
        base.Update();

        if (IsKeyPressed(KeyboardKey.R))
            Tiles.Tiles.Reload();

        if (!Program.Paused)
        {
            _playTime += TimeSpan.FromSeconds(GetFrameTime());

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

        _info.Info = new WorldInfo.InfoRecord(_info.Info.Name, _playTime);

        WorldManager.WriteWorld(_world, _info);
    }
}