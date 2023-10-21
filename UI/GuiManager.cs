namespace BuildingGame.UI;

public static class GuiManager
{
    private static List<Element> _elements = new List<Element>();

    public static bool IsFocused = false;

    public static readonly Font Font = GetFontDefault();
    public static readonly int FontSize = 10;
    
    public static void Add(Element element)
    {
        _elements.Add(element);
    }

    public static void Remove(Element element)
    {
        _elements.Remove(element);
    }

    public static void Remove(string name)
    {
        int index = _elements.FindIndex(e => string.Equals(e.Name, name, StringComparison.CurrentCultureIgnoreCase));
        if (index < 0) return;
        _elements.RemoveAt(index);
    }

    public static Element? Get(string name)
    {
        int index = _elements.FindIndex(e => string.Equals(e.Name, name, StringComparison.CurrentCultureIgnoreCase));
        if (index < 0) return null;
        return _elements[index];
    }

    public static TElement? GetAs<TElement>(string name) where TElement : Element
    {
        int index = _elements.FindIndex(e =>
            e.GetType() == typeof(TElement) && string.Equals(e.Name, name, StringComparison.CurrentCultureIgnoreCase));
        if (index < 0) return null;
        return _elements[index] as TElement;
    }

    public static bool IsMouseOverElement()
    {
        var elements = _elements;
        foreach (var el in elements)
        {
            if (el.IsHovered()) return true;
        }

        return false;
    }

    public static Element? GetElementUnderMouse()
    {
        var elements = _elements;
        foreach (var el in elements)
        {
            if (el.IsHovered()) return el;
        }

        return null;
    }

    public static void Update()
    {
        var elements = _elements;

        foreach (var el in elements)
        {
            if (!el.Active) continue;
            el.Update();
        }
    }

    public static void Draw()
    {
        var elements = _elements;
        elements.Sort((e1, e2) => e1.ZIndex.CompareTo(e1.ZIndex));
        foreach (var el in elements)
        {
            if (!el.Active) continue;
            if (!el.Visible) continue;
            el.Draw();
        }
    }
}