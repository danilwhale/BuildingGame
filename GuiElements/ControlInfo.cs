namespace BuildingGame;

public struct ControlInfo
{
    public Control Control { get; }
    public Screen Holder { get; }
    public bool MultiScreen { get; }

    public ControlInfo(Control control, Screen holder, bool multiscreen)
    {
        Control = control;
        Holder = holder;
        MultiScreen = multiscreen;
    }
    public ControlInfo((Control control, Screen holder, bool multiscreen) oldControlInfo)
        : this(oldControlInfo.control, oldControlInfo.holder, oldControlInfo.multiscreen) {}
}