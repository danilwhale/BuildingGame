using BuildingGame.GuiElements;

namespace BuildingGame;

public class CreateWorldScreen : Screen
{
    public int worldIndex;

    public override void Draw()
    {
        ClearBackground(new Color(20, 20, 20, 255));
        // throw new NotImplementedException();
    }

    public override void Initialize()
    {
        var worldNameBox = new InputBox("worldNameBox", 16, 24)
        {
            Area = new Rectangle(Program.WIDTH / 2 - 300 / 2, Program.HEIGHT / 3, 300, 24)
        };
        var worldNameBoxSubtitle = new TextBlock("worldNameBoxSubtitle", "(max. 16 characters)",
            new Vector2(worldNameBox.Area.x, worldNameBox.Area.y + 18 + 5), 18);
        worldNameBoxSubtitle.Color = Color.WHITE;

        var createWorldButton = new HoverButton("createWorldButton", "create",
            new Vector2(0, Program.HEIGHT / 3 + (24 + 5) * 2), 24);
        createWorldButton.CenterScreen();
        createWorldButton.Color = Color.WHITE;
        createWorldButton.Clicked += () =>
        {
            File.WriteAllText("saves/" + worldIndex + "/info.txt", worldNameBox.Text);
            Program.gameScreen.World.Load("saves/" + worldIndex + "/level.dat");
            Program.currentScreen = Program.gameScreen;
        };

        var backButton = new HoverButton("backButton", "back",
            new Vector2(0, Program.HEIGHT / 3 + (24 + 5) * 3), 24);
        backButton.CenterScreen();
        backButton.Color = Color.WHITE;
        backButton.Clicked += () =>
        {
            worldNameBox.Text = "";
            Program.currentScreen = Program.worldSelectScreen;
        };

        Gui.PutControl(worldNameBox, this);
        Gui.PutControl(worldNameBoxSubtitle, this);
        Gui.PutControl(createWorldButton, this);
        Gui.PutControl(backButton, this);
    }

    public override void Update()
    {
        // throw new NotImplementedException();
    }
}