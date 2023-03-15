using BuildingGame.Screens;

namespace BuildingGame.GuiElements;

public class ListView : Control
{
    private List<string> _strItems = new List<string>();
    public event Action<string>? ItemClicked;

    public ListView(string name)
        : base(name)
    {
    }

    public override void Update()
    {
        base.Update();

        var children = Children;
        var first = children.First();
        var last = children.Last();

        if (GetMouseWheelMoveV().Y < 0)
        {   
            foreach (var child in children) child.Area = new Rectangle(child.Area.x, child.Area.y - child.Area.height, child.Area.width, child.Area.height);
        }
        
        if (GetMouseWheelMoveV().Y > 0)
        {
            foreach (var child in children) child.Area = new Rectangle(child.Area.x, child.Area.y + child.Area.height, child.Area.width, child.Area.height);
        }
    }

    public void RecreateItems()
    {
        foreach (var control in Children) Gui.RemoveControl(control.Name);
        Children.Clear();

        for (int i = 0; i < _strItems.Count; i++)
        {
            var text = _strItems[i];
            var block = new TextBlock(Name + "_item_" + i, text,
                                      new Vector2(0, 10 + (24 + 6) * (i + 1)), 24);
            block.CenterScreen();
            block.Color = Color.WHITE;
            block.Clicked += () => ItemClicked?.Invoke(text);

            Children.Add(block);
        }
    }

    public void PutItem(string item)
    {
        _strItems.Add(item);
        RecreateItems();
    }

    public void RemoveItem(string item)
    {
        _strItems.Remove(item);
        RecreateItems();
    }

    public void ReplaceItem(string orig, string @new)
    {
        _strItems[_strItems.IndexOf(orig)] = @new;
        RecreateItems();
    }

    public void ClearItems()
    {
        _strItems.Clear();
        RecreateItems();
    }
}