using BuildingGame.UI.Screens;

namespace BuildingGame.UI;

public static class GuiManager
{
    public static bool IsFocused = false;

    public static readonly Font Font = GetFontDefault();
    public static readonly int FontSize = 10;

    public static void Add(Element element)
    {
        ScreenManager.CurrentScreen?.Elements.Add(element);
    }

    public static void Remove(Element element)
    {
        ScreenManager.CurrentScreen?.Elements.Remove(element);
    }

    public static void Remove(ElementId id)
    {
        if (ScreenManager.CurrentScreen == null) return;

        var index = ScreenManager.CurrentScreen.Elements.FindIndex(e => e.Id == id);
        if (index < 0) return;
        ScreenManager.CurrentScreen.Elements.RemoveAt(index);
    }

    public static Element? Get(ElementId id)
    {
        if (ScreenManager.CurrentScreen == null) return null;

        var index = ScreenManager.CurrentScreen.Elements.FindIndex(e => e.Id == id);
        if (index < 0) return null;
        return ScreenManager.CurrentScreen.Elements[index];
    }

    public static TElement? GetAs<TElement>(ElementId id) where TElement : Element
    {
        if (ScreenManager.CurrentScreen == null) return default;

        var index = ScreenManager.CurrentScreen.Elements.FindIndex(e =>
            e.GetType() == typeof(TElement) && e.Id == id);
        if (index < 0) return default;
        return ScreenManager.CurrentScreen.Elements[index] as TElement;
    }

    public static bool IsMouseOverElement()
    {
        return IsMouseOverElement(el => el.IsUnderMouse());
    }

    public static bool IsMouseOverElement(Func<Element, bool> predicate)
    {
        if (ScreenManager.CurrentScreen == null) return false;

        var elements = ScreenManager.CurrentScreen.ElementsSorted;
        return elements.Any(predicate);
    }

    public static Element? GetElementUnderMouse()
    {
        return GetElementUnderMouse(el => el.IsUnderMouse());
    }

    public static Element? GetElementUnderMouse(Func<Element, bool> predicate)
    {
        if (ScreenManager.CurrentScreen == null) return null;

        var elements = ScreenManager.CurrentScreen.ElementsSorted;
        return elements.FirstOrDefault(predicate);
    }

    [Obsolete("Use ScreenManager.Draw instead")]
    public static void Draw()
    {
    }

    [Obsolete("Use ScreenManager.Update instead")]
    public static void Update()
    {
    }
}