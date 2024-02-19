using System.Numerics;
using BuildingGame.Tiles;
using BuildingGame.UI.Elements;
using BuildingGame.UI.Screens;

namespace BuildingGame.UI.Interfaces;

public class CreateWorldUI : UIInterface
{
    private Button _backButton;
    private Button _createButton;
    private TextElement _maxCharacterText;
    private TextBox _worldNameTextBox;

    public override void Initialize()
    {
        base.Initialize();

        _worldNameTextBox = new TextBox(new ElementId("createWorldScreen", "worldNameTextBox"))
        {
            TextSize = 28.0f,
            Size = new Vector2(512.0f, 32.0f)
        };
        Elements.Add(_worldNameTextBox);

        _maxCharacterText = new TextElement(new ElementId("createWorldScreen", "maxCharacterText"))
        {
            Text = "(max. 16 characters)",
            TextSize = 20.0f,
            Size = _worldNameTextBox.Size with { Y = 24.0f },
            TextAlignment = Alignment.CenterLeft
        };
        Elements.Add(_maxCharacterText);

        _createButton = new Button(new ElementId("createWorldScreen", "createButton"))
        {
            Text = "create",
            TextSize = 24.0f,
            Size = _worldNameTextBox.Size with { Y = 28.0f },
            TextAlignment = Alignment.Center
        };
        _createButton.OnClick += () =>
        {
            if (string.IsNullOrWhiteSpace(_worldNameTextBox.Text)) return;
            if (WorldManager.Find(Path.Join(WorldManager.WorldsPath, _worldNameTextBox.Text)).Path != null) return;

            var info = WorldManager.CreateInfo(_worldNameTextBox.Text);
            WorldManager.WriteWorld(new World(), info);
            ScreenManager.Switch(new WorldSelectionScreen());
        };
        Elements.Add(_createButton);

        _backButton = new Button(new ElementId("createWorldScreen", "backButton"))
        {
            Text = "back",
            TextSize = 24.0f,
            Size = _worldNameTextBox.Size with { Y = 28.0f },
            TextAlignment = Alignment.Center
        };
        _backButton.OnClick += () => { ScreenManager.Switch(new WorldSelectionScreen()); };
        Elements.Add(_backButton);

        Configure();
    }

    public override void Configure()
    {
        _worldNameTextBox.GlobalPosition = new Vector2(GetScreenWidth() / 2 - _worldNameTextBox.Size.X / 2,
            GetScreenHeight() / 2 - 64.0f);
        _maxCharacterText.GlobalPosition =
            _worldNameTextBox.GlobalPosition + new Vector2(0.0f, _worldNameTextBox.Size.Y);
        _createButton.GlobalPosition = _maxCharacterText.GlobalPosition + new Vector2(0.0f, 40.0f);
        _backButton.GlobalPosition = _createButton.GlobalPosition + new Vector2(0.0f, _createButton.Size.Y);
    }

    public override void Resized()
    {
        Configure();
    }
}