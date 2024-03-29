using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildingGame;

public static class Settings
{
    public const string SettingsFile = "Settings.yaml";

    private static readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .Build();

    private static readonly ISerializer _serializer = new SerializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .Build();

    public static Color SkyColor = Color.SkyBlue;
    public static bool EnableDynamicTiles = true;
    public static bool EnableInfectionTile = true;
    public static string CurrentTilePack = "Default";

    public static void Load()
    {
        try
        {
            var content = File.ReadAllText(SettingsFile);
            var record = _deserializer.Deserialize<Record>(content);

            SkyColor = record.SkyColor;
            EnableDynamicTiles = record.EnableDynamicTiles;
            EnableInfectionTile = record.EnableInfectionTile;
            CurrentTilePack = record.CurrentTilePack;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("no " + SettingsFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine("whoops: " + ex);
        }
    }

    public static void Save()
    {
        try
        {
            var record = new Record(SkyColor, EnableDynamicTiles, EnableInfectionTile, CurrentTilePack);

            var content = _serializer.Serialize(record);
            File.WriteAllText(SettingsFile, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine("whoops: " + ex);
        }
    }

    public record struct Record(
        Color SkyColor,
        bool EnableDynamicTiles,
        bool EnableInfectionTile,
        string CurrentTilePack);
}