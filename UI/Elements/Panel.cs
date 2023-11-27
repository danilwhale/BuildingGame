using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class Panel : Element
{
    public IBrush? Brush;
    public int Padding = 0;
    
    public Panel(string name) : base(name)
    {
    }

    protected override void Render()
    {
        Brush?.FillArea(new Rectangle(Padding, Padding, Area.width - Padding - 1, Area.height - Padding - 1));
    }
}