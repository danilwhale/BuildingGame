using System.Numerics;
using BuildingGame.UI.Brushes;

namespace BuildingGame.UI.Elements;

public class CheckBox : Element
{
    public bool Checked;

    public IBrush? CheckBoxBrush = new OutlineBrush(Color.GRAY, Color.LIGHTGRAY);
    public float BoxScale = 1f;

    public string Text = string.Empty;
    public float TextSize = 12;
    public Color TextColor = Color.WHITE;


    public CheckBox(string name) : base(name)
    {
    }

    public override void Update()
    {
        if (IsClicked()) Checked = !Checked;
    }

    protected override void Render()
    {
        Rectangle boxArea = new Rectangle(
            0,
            0 + Area.Height / 2 - 18 * BoxScale / 2,
            18 * BoxScale,
            18 * BoxScale
        );
        CheckBoxBrush?.FillArea(boxArea);
        if (Checked)
        {
            DrawTexturePro(
                Resources.GetTexture("Checkmark.png"), 
                new Rectangle(0, 0, 18, 18), 
                boxArea, 
                Vector2.Zero, 0, Color.WHITE);
        }

        DrawText(Text, 8 + 18 * BoxScale, Area.Height / 2 - TextSize / 2f, TextSize, TextColor);
    }
}