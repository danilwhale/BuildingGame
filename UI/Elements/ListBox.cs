using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class ListBox : Element
{
    public const float ScrollSpeed = 1000;

    public List<string> Items = new List<string>();
    public event Action<string>? OnItemSelect;

    public IBrush? BackgroundBrush = new SolidBrush(WHITE);
    public IBrush? HighlightBrush = new SolidBrush(new Color(212, 245, 255, 255));
    public IBrush? SelectionBrush = new SolidBrush(new Color(122, 214, 255, 255));

    public float ItemTextSize = 12;
    public float ItemPadding = 4;
    public Color ItemColor = WHITE;

    private float _scroll = 0;
    private int _highlightIndex = -1;
    private int _selectedIndex = -1;

    public ListBox(string name) : base(name)
    {
    }

    public override void Update()
    {
        if (IsHovered())
        {
            // finding index for highlight
            float localMouseY = GetMousePosition().Y - Position.Y;
            int index = (int)((localMouseY - _scroll) / ItemTextSize);
            _highlightIndex = index;

            // list scrolling
            float boxHeight = Items.Count * ItemTextSize - Area.height + ItemPadding;
            float wheel = GetMouseWheelMove();
            float wheelAxis = wheel * ScrollSpeed * GetFrameTime();

            _scroll += wheelAxis;
            if (_scroll > 0) _scroll = 0;
            if (_scroll <= -boxHeight) _scroll = -boxHeight;
        }

        if (IsClicked())
        {
            if (_highlightIndex > 0)
            {
                _selectedIndex = _highlightIndex;
                OnItemSelect?.Invoke(Items[_selectedIndex]);
            }
        }
    }

    protected override void Render()
    {
        BackgroundBrush?.FillArea(Area);
        for (int i = 0; i < Items.Count; i++)
        {
            Rectangle area = new Rectangle(
                Area.x + ItemPadding, 
                Area.y + i * ItemTextSize + ItemPadding + _scroll,
                Area.width, 
                ItemTextSize
                );
            if (i == _selectedIndex)
            {
                SelectionBrush?.FillArea(area);
            }
            else if (i == _highlightIndex)
            {
                HighlightBrush?.FillArea(area);
            }

            DrawText(Items[i], area.x, area.y, ItemTextSize, ItemColor);
        }
    }
}