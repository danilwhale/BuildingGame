using System.Numerics;

namespace BuildingGame;

public static class Raylib
{
    // https://github.com/raysan5/raylib/blob/d2b1256e5c3567484486ad70cc2bb69495abfbf4/src/rtext.c#L1108
    public static void DrawText(string text, float posX, float posY, float fontSize, Color color)
    {
        if (GetFontDefault().Texture.Id == 0) return;

        var pos = new Vector2(posX, posY);
        var spacing = GetSpacing(fontSize);

        DrawTextEx(GetFontDefault(), text, pos, fontSize, spacing, color);
    }

    public static float GetSpacing(float fontSize)
    {
        var defaultFontSize = 10.0f;
        if (fontSize < defaultFontSize) fontSize = defaultFontSize;
        return fontSize / defaultFontSize;
    }
}