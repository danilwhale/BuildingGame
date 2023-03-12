using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace BuildingGame;

public class Settings
{
    public static Color SkyColor = Color.SKYBLUE;
    public static bool EnablePhysics = false;
    public static bool EnableInfectionBlock = false;

    public static void Save()
    {
        try
        {
            ConfigParser.Write("settings.txt", new Dictionary<string, string>()
            {
                { "skyColorR", SkyColor.r.ToString() },
                { "skyColorG", SkyColor.g.ToString() },
                { "skyColorB", SkyColor.b.ToString() },
                { "enablePhysics", EnablePhysics.ToString() },
                { "enableInfectionBlock", EnableInfectionBlock.ToString() }
            });
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }
    public static void Load()
    {
        try
        {
            var data = ConfigParser.Parse("settings.txt");

            if (byte.TryParse(data["skyColorR"], out byte skyColorR) &&
                byte.TryParse(data["skyColorG"], out byte skyColorG) &&
                byte.TryParse(data["skyColorB"], out byte skyColorB))
            {
                SkyColor = new Color(skyColorR, skyColorG, skyColorB, (byte)255);
            }

            if (bool.TryParse(data["enablePhysics"], out bool enablePhysics))
                EnablePhysics = enablePhysics;
            if (bool.TryParse(data["enableInfectionBlock"], out bool enableInfectionBlock))
                EnableInfectionBlock = enableInfectionBlock;
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }
}