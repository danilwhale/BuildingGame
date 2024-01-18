using System.Numerics;
using BuildingGame.Tiles;
using BuildingGame.Translation;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Screens;

namespace BuildingGame.UI.Interfaces;

public class WorldMenuUI : UIInterface
{
    private ListBox _worldList;
    private Panel _sidePanel;
    private Panel _bottomPanel;
    private TextElement _selectedWorldTitle;
    private TextElement _selectedWorldPlayTime;
    private Button _playWorldButton;
    private Button _menuButton;
    private Button _createWorldButton;
    private Button _deleteWorldButton;
    
    private string[] _paths;
    
    public override void Initialize()
    {
        var translation = TranslationContainer.Default;

        _paths = WorldManager.Worlds.Select(w => w.Path).ToArray();

        _worldList = new ListBox(new ElementId("worldScreen", "list"))
        {
            ItemTextSize = 20.0f,
            GlobalPosition = new Vector2(0.0f, 0.0f),
            BackgroundBrush = null,
            ItemColor = Color.WHITE,
            Items = WorldManager.Worlds.Select(w => $"{w.Info.Name} ['{Path.GetFileName(w.Path)}']").ToList()
        };
        _worldList.OnItemSelect += (item) =>
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                _selectedWorldTitle.Text = translation.GetTranslatedName("default_world_title");

                _selectedWorldPlayTime.Visible = false;
                _playWorldButton.Visible = false;
                _deleteWorldButton.Visible = false;

                return;
            }

            _selectedWorldTitle.Text = item;

            _selectedWorldPlayTime.Visible = true;
            _playWorldButton.Visible = true;
            _deleteWorldButton.Visible = true;

            var world = WorldManager.Find(_paths[_worldList.SelectedItem]);
            _selectedWorldPlayTime.Text = string.Format(translation.GetTranslatedName("play_time_format"), world.Info.PlayTime);
        };
        Elements.Add(_worldList);

        _sidePanel = new Panel(new ElementId("worldScreen", "sidePanel"));
        Elements.Add(_sidePanel);

        _bottomPanel = new Panel(new ElementId("worldScreen", "bottomPanel"));
        Elements.Add(_bottomPanel);

        _selectedWorldTitle = new TextElement(new ElementId(_sidePanel.Id, "title"))
        {
            Text = translation.GetTranslatedName("default_world_title"),
            TextSize = 28.0f,
            TextAlignment = Alignment.Center,
            Parent = _sidePanel
        };

        _selectedWorldPlayTime = new TextElement(new ElementId(_sidePanel.Id, "playTime"))
        {
            Text = string.Format(translation.GetTranslatedName("play_time_format"), TimeSpan.Zero),
            TextSize = 20.0f,
            TextAlignment = Alignment.TopCenter,
            Parent = _sidePanel,
            LocalPosition = new Vector2(0.0f, 40.0f),
            Visible = false
        };

        _playWorldButton = new Button(new ElementId(_sidePanel.Id, "playButton"))
        {
            Text = translation.GetTranslatedName("play_button"),
            TextSize = 32.0f,
            TextAlignment = Alignment.Center,
            Parent = _sidePanel,
            LocalPosition = new Vector2(0.0f, 100.0f),
            Visible = false
        };
        _playWorldButton.OnClick += () =>
        {
            ScreenManager.Switch(new GameScreen(_paths[_worldList.SelectedItem]));
        };

        _menuButton = new Button(new ElementId(_bottomPanel.Id, "menuButton"))
        {
            Text = translation.GetTranslatedName("menu_button"),
            TextSize = 24.0f,
            Size = new Vector2(64.0f, 28.0f),
            TextAlignment = Alignment.Center,
            Parent = _bottomPanel
        };
        _menuButton.OnClick += () =>
        {
            ScreenManager.Switch(new MenuScreen());
        };

        _createWorldButton = new Button(new ElementId(_bottomPanel.Id, "createWorldButton"))
        {
            Text = translation.GetTranslatedName("create_world_button"),
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f),
            TextAlignment = Alignment.Center,
            Parent = _bottomPanel
        };
        _createWorldButton.OnClick += () =>
        {
            ScreenManager.Switch(new CreateWorldScreen());
        };

        _deleteWorldButton = new Button(new ElementId(_bottomPanel.Id, "deleteWorldButton"))
        {
            Text = translation.GetTranslatedName("delete_world_button"),
            TextSize = 24.0f,
            Size = new Vector2(100.0f, 28.0f),
            TextAlignment = Alignment.Center,
            Visible = false,
            Parent = _bottomPanel,
        };
        _deleteWorldButton.OnClick += () =>
        {
            var world = WorldManager.Find(_paths[_worldList.SelectedItem]);
            WorldManager.DeleteWorld(world);
            _worldList.Items.RemoveAt(_worldList.SelectedItem);
            _deleteWorldButton.Visible = false;
        };

        Configure();
    }

    public override void Configure()
    {
        _worldList.Size = new Vector2(GetScreenWidth() / 2.0f, GetScreenHeight() - 64.0f);

        _bottomPanel.Size = new Vector2(GetScreenWidth(), 64.0f);
        _bottomPanel.GlobalPosition = new Vector2(0.0f, GetScreenHeight() - 64.0f);

        _sidePanel.Size = (new Vector2(GetScreenWidth(), GetScreenHeight()) - _worldList.Size) with { Y = GetScreenHeight() - _bottomPanel.Size.Y };
        _sidePanel.GlobalPosition = _worldList.Size with { Y = 0 };

        _selectedWorldTitle.Size = new Vector2(_sidePanel.Size.X, 40.0f);
        _selectedWorldPlayTime.Size = new Vector2(_sidePanel.Size.X, 24.0f);
        _playWorldButton.Size = new Vector2(_sidePanel.Size.X, 36.0f);

        _menuButton.LocalPosition = new Vector2(_bottomPanel.Size.X - _menuButton.Size.X - 32.0f, _bottomPanel.Size.Y / 2 - _menuButton.Size.Y / 2);

        _createWorldButton.LocalPosition = new Vector2(8.0f, _bottomPanel.Size.Y / 2 - _createWorldButton.Size.Y / 2);
        _deleteWorldButton.LocalPosition = _createWorldButton.LocalPosition + _createWorldButton.Size with { Y = 0 } + new Vector2(16.0f, 0.0f);
    }

    public override void Resized()
    {
        Configure();
    }
}