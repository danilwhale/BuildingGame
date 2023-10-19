using Newtonsoft.Json;

namespace BuildingGame.Atlas;

public static class AtlasLoader
{
    public const string AtlasFile = "Assets/Atlas.json";

    public static Dictionary<string, AtlasTile> LoadTiles()
    {
        if (!File.Exists(AtlasFile))
            throw new InvalidOperationException("Can't load Atlas.json because it doesn't exists");

        string json = File.ReadAllText(AtlasFile);

        return JsonConvert.DeserializeObject<Dictionary<string, AtlasTile>>(json) ??
               throw new InvalidOperationException("Atlas.json is empty");
    }

    public static Dictionary<AtlasTileKey, Tile> ConvertTiles(Dictionary<string, AtlasTile> tiles)
    {
        byte i = 1;
        Dictionary<AtlasTileKey, Tile> flatTiles = new Dictionary<AtlasTileKey, Tile>();

        foreach (var kv in tiles)
        {
            flatTiles.Add(new AtlasTileKey(kv.Key, i), new Tile(kv.Value.AtlasOffset, kv.Value.Size));
            i++;
        }

        return flatTiles;
    }
}