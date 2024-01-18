using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class ListBox : Element
{
    public const float ScrollSpeed = 1000;

    public List<string> Items = new List<string>();
    public event Action<string>? OnItemSelect;

    public IBrush? BackgroundBrush = null;
    public IBrush? HighlightBrush = new SolidBrush(new Color(90, 95, 100, 255));
    public IBrush? SelectionBrush = new SolidBrush(new Color(60, 70, 80, 255));

    public float ItemTextSize = 12;
    public float ItemPadding = 4;
    public Color ItemColor = Color.WHITE;
    public int SelectedItem = -1;

    private float _scroll = 0;
    private int _highlightIndex = -1;

    public ListBox(ElementId id) : base(id) 
    {
    }

    public override void Update()
    {
        if (IsUnderMouse())
        {
            // finding index for highlight
            float localMouseY = GetMousePosition().Y - GlobalPosition.Y;
            int index = (int)((localMouseY - _scroll) / ItemTextSize);
            _highlightIndex = index;

            // list scrolling
            float boxHeight = Items.Count * ItemTextSize + Items.Count * ItemPadding;
            float wheel = GetMouseWheelMove();
            float wheelAxis = wheel * ScrollSpeed * GetFrameTime();

            if (boxHeight > Area.Height)
            {
                _scroll += wheelAxis;
                if (_scroll > 0) _scroll = 0;
                if (_scroll <= -boxHeight / 2) _scroll = -boxHeight / 2;
            }

            Console.WriteLine(_scroll);
        }

        if (IsClicked())
        {
            if (_highlightIndex >= 0)
            {
                SelectedItem = _highlightIndex;
                OnItemSelect?.Invoke(Items[SelectedItem]);
            }
        }
    }

    protected override void Render()
    {
        BackgroundBrush?.FillArea(Area);
        for (int i = 0; i < Items.Count; i++)
        {
            Rectangle area = new Rectangle(
                ItemPadding, 
                i * ItemTextSize + ItemPadding + _scroll,
                Area.Width, 
                ItemTextSize
                );
            if (i == SelectedItem)
            {
                SelectionBrush?.FillArea(area);
            }
            else if (i == _highlightIndex)
            {
                HighlightBrush?.FillArea(area);
            }

            DrawText(Items[i], area.X, area.Y, ItemTextSize, ItemColor);
        }
    }
}