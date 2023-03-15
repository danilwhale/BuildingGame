using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace BuildingGame;

public class Settings
{
    public static Color SkyColor = Color.SKYBLUE;
    public static bool EnablePhysics = false;
    public static bool EnableInfectionBlock = false;
    public static string CurrentPack = "default";

    public static void Save()
    {
        try
        {
            ConfigParser.Write("settings.txt", new Dictionary<string, string>()
            {
                { "skyColor", ToStringColor(SkyColor) },
                { "enablePhysics", EnablePhysics.ToString() },
                { "enableInfectionBlock", EnableInfectionBlock.ToString() },
                { "currentTilePack", CurrentPack }
            });
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }
    public static void Load()
    {
        try
        {
            var data = ConfigParser.Parse("settings.txt");

            if (data.TryGetValue("skyColor", out var skyColorStr) &&
                TryParseColor(skyColorStr, out var skyColor))
            {
                SkyColor = skyColor;
            }

            if (data.TryGetValue("enablePhysics", out var enablePhysicsStr) &&
                bool.TryParse(enablePhysicsStr, out bool enablePhysics))
                EnablePhysics = enablePhysics;
            if (data.TryGetValue("enableInfectionBlock", out var enableInfectionBlockStr) &&
                bool.TryParse(enableInfectionBlockStr, out bool enableInfectionBlock))
                EnableInfectionBlock = enableInfectionBlock;
            if (data.TryGetValue("currentTilePack", out var currentPack))
                CurrentPack = currentPack;
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }

    private static bool TryParseColor(string str, out Color color)
    {
        var split = str.Split(',');
        color = Color.WHITE;

        if (split.Length < 3) return false;

        if (byte.TryParse(split[0].Trim(), out var r) &&
            byte.TryParse(split[1].Trim(), out var g) &&
            byte.TryParse(split[2].Trim(), out var b))
        {
            color = new Color(r, g, b, (byte)255);
            return true;
        }

        return false;
    }

    private static string ToStringColor(Color color)
    {
        return $"{color.r},{color.g},{color.b}";
    }
}