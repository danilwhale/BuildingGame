using System.Numerics;

namespace BuildingGame.UI;

public static class Text
{
    public static readonly int[] CyrillicCodepoints = Enumerable.Range('\u0400', '\u04FF').ToArray();
    public static readonly int[] LatinCodepoints = Enumerable.Range('\u0000', '\u007F').ToArray();

    public static Font Font;

    public static void LoadFont(string path, int fontSize)
    {
        var codepoints = new int[LatinCodepoints.Length + CyrillicCodepoints.Length];
        Buffer.BlockCopy(LatinCodepoints, 0, codepoints, 0, LatinCodepoints.Length * sizeof(int));
        Buffer.BlockCopy(CyrillicCodepoints, 0, codepoints, LatinCodepoints.Length * sizeof(int),
            CyrillicCodepoints.Length * sizeof(int));

        Font = Resources.GetFont(path, fontSize, codepoints);
    }

    public static void Draw(string text, Vector2 position, float textSize, Color tint)
    {
        DrawTextEx(Font, text, position, textSize, GetSpacing(textSize), tint);
    }

    public static float GetSpacing(float textSize)
    {
        return Font.BaseSize / textSize;
    }
}