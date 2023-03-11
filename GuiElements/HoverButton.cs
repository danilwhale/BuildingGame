namespace BuildingGame.GuiElements;

public class HoverButton : TextBlock
{
    public HoverButton(string name, string text, Vector2 position, float size = 12)
        : base(name, text, position, size)
    {
        SetupButtonCollision();
    }

    public override void Update()
    {
        base.Update();

        if (_centeredScreen) CenterScreen();

        if (CheckCollisionPointRec(GetMousePosition(), Area))
        {
            if (Text != null && !Text.StartsWith("> "))
                Text = Text.Insert(0, "> ");
        }
        else
        {
            if (Text != null && Text.StartsWith("> "))
            {
                Text = Text.Remove(0, 2);
            }
        }
    }

    public void SetupButtonCollision()
    {
        SetupCollision("> " + Text);
    }
}