using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class Panel : Element
{
    public IBrush? Brush;
    public int Padding = 0;

    public Panel(ElementId id) : base(id)
    {
    }

    protected override void Render()
    {
        Brush?.FillArea(new Rectangle(Padding, Padding, Area.Width - Padding - 1, Area.Height - Padding - 1));
    }
}