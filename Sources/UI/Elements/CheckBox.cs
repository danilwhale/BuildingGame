using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class CheckBox : Element
{
    public float BoxScale = 1f;

    public IBrush? CheckBoxBrush = new OutlineBrush(Color.Gray, Color.LightGray);
    public bool Checked;

    public string Text = string.Empty;
    public Color TextColor = Color.White;
    public float TextSize = 12;

    public CheckBox(ElementId id) : base(id)
    {
    }

    public event Action<bool>? OnCheck;

    public override void Update()
    {
        if (IsClicked())
        {
            Checked = !Checked;
            OnCheck?.Invoke(Checked);
        }
    }

    protected override void Render()
    {
        var boxArea = new Rectangle(
            0,
            0 + Area.Height / 2 - 18 * BoxScale / 2,
            18 * BoxScale,
            18 * BoxScale
        );
        CheckBoxBrush?.FillArea(boxArea);
        if (Checked)
            DrawTexturePro(
                Resources.GetTexture("Checkmark.png"),
                new Rectangle(0, 0, 18, 18),
                boxArea,
                Vector2.Zero, 0, Color.White);
        
        DrawTextEx(GuiManager.Font, Text, new Vector2(8 + 18 * BoxScale, Area.Height / 2 - TextSize / 2f), TextSize, TextSize / GuiManager.FontSize, TextColor);
    }
}