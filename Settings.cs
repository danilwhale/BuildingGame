using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace BuildingGame;

public class Settings
{
    public static Color SkyColor = Color.SKYBLUE;
    public static bool EnablePhysics = false;

    public byte skyColorR { get; set; }
    public byte skyColorG { get; set; }
    public byte skyColorB { get; set; }
    public bool enablePhysics { get; set; }

    public static void Save()
    {
        try
        {
            ConfigParser.Write("settings.txt", new Dictionary<string, string>()
            {
                { "skyColorR", SkyColor.r.ToString() },
                { "skyColorG", SkyColor.g.ToString() },
                { "skyColorB", SkyColor.b.ToString() },
                { "enablePhysics", EnablePhysics.ToString() }
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
        }
        catch (Exception ex) { Console.WriteLine(ex); }
    }
}