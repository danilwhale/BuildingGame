using BuildingGame.Tiles.Atlas;
using BuildingGame.Translation;

namespace BuildingGame.Tiles;

public static class Tiles
{
    private static Dictionary<AtlasTileKey, Tile> _Tiles = AtlasLoader.ConvertTiles(AtlasLoader.LoadTiles());

    public static void Reload()
    {
        _Tiles = AtlasLoader.ConvertTiles(AtlasLoader.LoadTiles());
    }
    
    public static Tile GetTile(byte id)
    {
        if (id < 1) throw new ArgumentException("Id mustn't be an air (0)", nameof(id));
        return _Tiles.First(kv => kv.Key.Id == id).Value;
    }

    public static Tile GetTile(string name)
    {
        return _Tiles.First(kv => string.Equals(kv.Key.Name, name, StringComparison.CurrentCultureIgnoreCase)).Value;
    }

    public static Tile[] GetTiles()
    {
        return _Tiles.Values.ToArray();
    }

    public static void RegisterCustomTile<T>(string name, T tile) where T : Tile
    {
        var key = _Tiles.Keys.First(k => string.Equals(k.Name, name, StringComparison.CurrentCultureIgnoreCase));
        _Tiles[key] = tile;
    }

    public static void RegisterCustomTile<T>(byte id, T tile) where T : Tile
    {
        var key = _Tiles.Keys.First(k => k.Id == id);
        _Tiles[key] = tile;
    }
}