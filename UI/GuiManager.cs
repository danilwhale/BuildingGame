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
        
        int index = ScreenManager.CurrentScreen.Elements.FindIndex(e => e.Id == id);
        if (index < 0) return;
        ScreenManager.CurrentScreen.Elements.RemoveAt(index);
    }

    public static Element? Get(ElementId id)
    {
        if (ScreenManager.CurrentScreen == null) return null;

        int index = ScreenManager.CurrentScreen.Elements.FindIndex(e => e.Id == id);
        if (index < 0) return null;
        return ScreenManager.CurrentScreen.Elements[index];
    }

    public static TElement? GetAs<TElement>(ElementId id) where TElement : Element
    {
        if (ScreenManager.CurrentScreen == null) return default;
        
        int index = ScreenManager.CurrentScreen.Elements.FindIndex(e =>
            e.GetType() == typeof(TElement) && e.Id == id);
        if (index < 0) return default;
        return ScreenManager.CurrentScreen.Elements[index] as TElement;
    }

    public static bool IsMouseOverElement()
    {
        if (ScreenManager.CurrentScreen == null) return false;

        var elements = ScreenManager.CurrentScreen.ElementsSorted;
        return elements.Any(element => element.Visible && element.Active && element.IsUnderMouse());
    }

    public static Element? GetElementUnderMouse()
    {
        if (ScreenManager.CurrentScreen == null) return null;

        var elements = ScreenManager.CurrentScreen.ElementsSorted;
        return elements.FirstOrDefault(el => el.IsUnderMouse());
    }

    [Obsolete("Use ScreenManager.Draw instead")]
    public static void Draw() { }
    
    [Obsolete("Use ScreenManager.Update instead")]
    public static void Update() { }
}