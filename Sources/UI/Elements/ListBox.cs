using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class ListBox : Element
{
    public const float ScrollSpeed = 1000;
    private int _highlightIndex = -1;

    private float _scroll;

    public IBrush? BackgroundBrush = null;
    public IBrush? HighlightBrush = new SolidBrush(new Color(90, 95, 100, 255));
    public Color ItemColor = Color.White;
    public float ItemPadding = 4;

    public List<string> Items = new();

    public float ItemTextSize = 12;
    public int SelectedItem = -1;
    public IBrush? SelectionBrush = new SolidBrush(new Color(60, 70, 80, 255));

    public ListBox(ElementId id) : base(id)
    {
    }

    public event Action<string>? OnItemSelect;

    public override void Update()
    {
        if (IsUnderMouse())
        {
            // finding index for highlight
            var localMouseY = GetMousePosition().Y - GlobalPosition.Y;
            var index = (int)((localMouseY - _scroll) / ItemTextSize);
            _highlightIndex = index;

            // list scrolling
            var boxHeight = Items.Count * ItemTextSize + Items.Count * ItemPadding;
            var wheel = GetMouseWheelMove();
            var wheelAxis = wheel * ScrollSpeed * GetFrameTime();

            if (boxHeight > Area.Height)
            {
                _scroll += wheelAxis;
                if (_scroll > 0) _scroll = 0;
                if (_scroll <= -boxHeight / 2) _scroll = -boxHeight / 2;
            }
        }

        if (IsClicked())
            if (_highlightIndex >= 0)
            {
                SelectedItem = _highlightIndex;

                if (SelectedItem >= 0 && SelectedItem < Items.Count)
                    OnItemSelect?.Invoke(Items[SelectedItem]);
                else
                    OnItemSelect?.Invoke(string.Empty);
            }
    }

    protected override void Render()
    {
        BackgroundBrush?.FillArea(Area);
        for (var i = 0; i < Items.Count; i++)
        {
            var area = new Rectangle(
                ItemPadding,
                i * ItemTextSize + ItemPadding + _scroll,
                Area.Width,
                ItemTextSize
            );
            if (i == SelectedItem)
                SelectionBrush?.FillArea(area);
            else if (i == _highlightIndex) HighlightBrush?.FillArea(area);

            DrawText(Items[i], area.X, area.Y, ItemTextSize, ItemColor);
        }
    }
}