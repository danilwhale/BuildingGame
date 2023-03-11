using BuildingGame.GuiElements.Brushes;

namespace BuildingGame.GuiElements;

public class BackgroundBlock : Control
{
    public IBrush? Background { get; set; }

    public BackgroundBlock(string name, IBrush? background = null)
        : base(name)
    {
        Background = background;
    }

    public override void Draw()
    {
        if (Background != null)
            Background.FillArea(Area);
    }
}