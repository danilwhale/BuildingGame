using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildingGame.Tiles.Atlas;

public static class AtlasLoader
{
    public const string AtlasFile = "Assets/Atlas.yaml";

    private static readonly IDeserializer _yaml = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static Dictionary<string, AtlasTile> LoadTiles()
    {
        if (!File.Exists(AtlasFile))
            throw new InvalidOperationException("Can't load Atlas.json because it doesn't exists");

        var yamlText = File.ReadAllText(AtlasFile);

        return _yaml.Deserialize<Dictionary<string, AtlasTile>>(yamlText) ??
               throw new InvalidOperationException("Atlas.yaml is empty");
    }

    public static Dictionary<AtlasTileKey, Tile> ConvertTiles(Dictionary<string, AtlasTile> tiles)
    {
        byte i = 1;
        var flatTiles = new Dictionary<AtlasTileKey, Tile>();

        foreach (var kv in tiles)
        {
            flatTiles.Add(new AtlasTileKey(kv.Key, i), new Tile(kv.Value.Atlas, kv.Value.Size, kv.Key));
            i++;
        }

        return flatTiles;
    }
}