using BuildingGame.GuiElements;

namespace BuildingGame;

public static class Gui
{
    public static Font GuiFont => _font;
    public static bool ProcessGui { get; set; } = true;
    private static Font _font;

    private static List<(Control control, Screen holder, bool multiscreen)> _controls = 
        new List<(Control control, Screen holder, bool multiscreen)>();

    public static void SetGuiFont(Font font)
    {
        _font = font;
    }
    public static void SetGuiFont(string fileName)
    {
        _font = LoadFont(fileName);
    }
    public static void UnloadGuiFont() => UnloadFont(_font);

    public static bool IsMouseOverControl 
    {
        get
        {
            var curControls = _controls;
            foreach (var control in curControls)
            {
                if (control.control.Active && control.control.IsMouseHovered() && (control.holder == Program.currentScreen || control.multiscreen))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static void PutControl(Control control, Screen holder, bool multiscreen = false)
    {
        if (!_controls.Contains((control, holder, multiscreen)))
            _controls.Add((control, holder, multiscreen));

        foreach (var child in control.Children)
        {
            PutControl(child, holder, multiscreen);
        }
    } 
    public static Control PopControl(string id)
    {
        var control = _controls.Find(m => m.control.Name == id);
        _controls.Remove(control);
        return control.control;
    }
    public static Control GetControl(string id) => _controls.Find(m => m.control.Name == id).control;

    public static void Update()
    {
        if (!ProcessGui) return;
        
        var curControls = _controls;
        foreach (var control in curControls)
        {
            if (control.holder == Program.currentScreen || control.multiscreen)
                control.control.Update();
        }
    }

    public static void Draw()
    {
        if (!ProcessGui) return;

        var curControls = _controls;
        curControls.Sort((x, y) => x.control.ZIndex.CompareTo(y.control.ZIndex));
        foreach (var control in curControls)
        {
            if (control.control.Active && (control.holder == Program.currentScreen || control.multiscreen))
                control.control.Draw();
        } 
    }
}