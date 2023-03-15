namespace BuildingGame;

public static class Gui
{
    public static Font GuiFont => _font;
    public static bool ProcessGui { get; set; } = true;
    private static Font _font;

    private static List<ControlInfo> _controls = 
        new List<ControlInfo>();

    public static void SetGuiFont(Font font)
    {
        _font = font;
    }
    public static void SetGuiFont(string fileName)
    {
        _font = LoadFont(fileName);
    }
    public static void UnloadGuiFont() => UnloadFont(_font);

    public static ControlInfo[] GetControls()
    {
        return _controls.ToArray();
    }

    public static bool IsMouseOverControl 
    {
        get
        {
            foreach (var control in GetControls())
            {
                if (control.Control.Active && control.Control.IsMouseHovered() && (control.Holder == Program.currentScreen || control.MultiScreen))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static void PutControl(ControlInfo info)
    {
        if (!_controls.Contains(info))
            _controls.Add(info);

        foreach (var child in info.Control.Children)
        {
            PutControl(new ControlInfo(child, info.Holder, info.MultiScreen));
        }
    }

    public static void PutControl(Control control, Screen holder, bool multiscreen = false)
    {
        PutControl(new ControlInfo(control, holder, multiscreen));
    } 
    public static Control PopControl(string id)
    {
        var control = _controls.Find(m => m.Control.Name == id);
        _controls.Remove(control);
        return control.Control;
    }
    public static void RemoveControl(string id)
    {
        var control = _controls.Find(m => m.Control.Name == id);
        _controls.Remove(control);
    }
    public static Control GetControl(string id) => _controls.Find(m => m.Control.Name == id).Control;

    public static void Update()
    {
        if (!ProcessGui) return;

        var curControls = GetControls();
        foreach (var control in curControls)
        {
            if (control.Holder == Program.currentScreen || control.MultiScreen)
            {
                control.Control.Update();
                foreach (var child in control.Control.Children)
                {
                    var info = new ControlInfo(child, control.Holder, control.MultiScreen);
                    if (!_controls.Contains(info))
                        _controls.Add(info);
                }
            }
                
        }
    }

    public static void Draw()
    {
        if (!ProcessGui) return;

        var curControls = GetControls();

        curControls.ToList().Sort((x, y) => x.Control.ZIndex.CompareTo(y.Control.ZIndex));
        foreach (var control in curControls)
        {
            if (control.Control.Active && (control.Holder == Program.currentScreen || control.MultiScreen))
                control.Control.Draw();
        } 
    }
}