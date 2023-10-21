using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class Panel : Element
{
    public IBrush? Brush;
    
    public Panel(string name) : base(name)
    {
    }

    protected override void Render()
    {
        Brush?.FillArea(Area);
    }
}