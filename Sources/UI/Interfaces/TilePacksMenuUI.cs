using System.Numerics;
using BuildingGame.Tiles.Packs;
using BuildingGame.Translation;
using BuildingGame.UI.Brushes;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Screens;

namespace BuildingGame.UI.Interfaces;

public class TilePacksMenuUI : UIInterface
{
    private ListBox _tilePacksList;
    private Button _menuButton;

    private string[] _paths;
    
    public override void Initialize()
    {
        _paths = TilePackManager.TilePacks.Select(t => t.Path).ToArray();
        
        var translation = TranslationContainer.Default;
        
        _tilePacksList = new ListBox(new ElementId("tilePacksMenu", "tilePacksList"))
        {
            ItemTextSize = 24.0f,
            BackgroundBrush = null,
            ItemColor = Color.White,
            Items = TilePackManager.TilePacks.Select(t => t.Info.Name).ToList()
        };
        _tilePacksList.OnItemSelect += item =>
        {
            var index = _tilePacksList.SelectedItem;
            
            if (index < 0 || index >= _paths.Length) return;

            var oldTilePack = TilePackManager.Find(Settings.CurrentTilePack);
            SetTilePackActive(oldTilePack, false);
            
            var tilePack = TilePackManager.Find(_paths[index]);
            TilePackManager.Apply(tilePack);

            SetTilePackActive(tilePack, true);
        };
        Elements.Add(_tilePacksList);

        _menuButton = new Button(new ElementId("tilePacksMenu", "menuButton"))
        {
            Text = translation.GetTranslatedName("menu_button"),
            TextSize = 24.0f,
            Size = new Vector2(256.0f, 32.0f),
            TextAlignment = Alignment.Center
        };
        _menuButton.OnClick += () =>
        {
            ScreenManager.Switch(new MenuScreen());
        };
        Elements.Add(_menuButton);
        
        SetTilePackActive(TilePackManager.Find(Settings.CurrentTilePack), true);
        
        Configure();
    }

    private void SetTilePackActive(TilePack tilePack, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(tilePack.Path)) return;
        
        var index = Array.IndexOf(_paths, tilePack.Path);
        if (index < 0) return;
        
        _tilePacksList.Items[index] = (isActive ? "> " : string.Empty) + tilePack.Info.Name;
    }

    public override void Configure()
    {
        _tilePacksList.Size = new Vector2(GetScreenWidth() / 3.0f, GetScreenHeight() - _menuButton.Size.Y - 8.0f);
        _tilePacksList.GlobalPosition = new Vector2(GetScreenWidth() / 2.0f - _tilePacksList.Size.X / 2.0f, 0.0f);

        _menuButton.GlobalPosition = new Vector2(GetScreenWidth() / 2.0f - _menuButton.Size.X / 2.0f,
            GetScreenHeight() - _menuButton.Size.Y - 8.0f);
    }

    public override void Resized()
    {
        Configure();
    }
}