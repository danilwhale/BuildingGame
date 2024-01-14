using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BuildingGame;


/// <summary>
/// contains some extra methods to simplify transition from raylib-csharp-vinculum
/// </summary> 
public static partial class Raylib
{
    [LibraryImport(NativeLibName, EntryPoint = "IsKeyPressedRepeat")]
    public static partial CBool IsKeyPressedRepeat(KeyboardKey key);
    
    // https://github.com/raysan5/raylib/blob/d2b1256e5c3567484486ad70cc2bb69495abfbf4/src/rtext.c#L1108
    public static void DrawText(string text, float posX, float posY, float fontSize, Color color)
    {
        if (GetFontDefault().Texture.Id == 0) return;

        var pos = new Vector2(posX, posY);

        var defaultFontSize = 10.0f;
        if (fontSize < defaultFontSize) fontSize = defaultFontSize;
        var spacing = fontSize / defaultFontSize;
        
        DrawTextEx(GetFontDefault(), text, pos, fontSize, spacing, color);
    }
}