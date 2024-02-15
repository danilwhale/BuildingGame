using System.Diagnostics.CodeAnalysis;
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
    
    [Obsolete("use TryGetTile instead pls ty", true)]
    public static Tile GetTile(byte id)
    {
        throw new Exception("use TryGetTile instead pls ty");
    }
    
    [Obsolete("use TryGetTile instead pls ty", true)]
    public static Tile GetTile(string name)
    {
        throw new Exception("use TryGetTile instead pls ty");
    }

    public static bool TryGetTile(byte id, [NotNullWhen(true)] out Tile? tile)
    {
        tile = _Tiles.FirstOrDefault(kv => kv.Key.Id == id).Value;
        return tile != null;
    }

    public static bool TryGetTile(string name, [NotNullWhen(true)] out Tile? tile)
    {
        tile = _Tiles.FirstOrDefault(kv => string.Equals(kv.Key.Name, name, StringComparison.CurrentCultureIgnoreCase)).Value;
        return tile != null;
    }

    public static byte GetId(Tile tile)
    {
        var key = _Tiles.FirstOrDefault(kv => kv.Value == tile).Key;
        return key.Id;
    }

    public static Tile[] GetTiles()
    {
        return _Tiles.Values.ToArray();
    }

    public static void Update()
    {
        foreach (var kv in _Tiles)
        {
            kv.Value.StaticUpdate();
        }
    }

    public static void RegisterCustomTile<T>(string name, T tile) where T : Tile
    {
        var key = _Tiles.Keys.First(k => string.Equals(k.Name, name, StringComparison.CurrentCultureIgnoreCase));
        var value = _Tiles[key];

        tile.Size = value.Size;
        tile.TexCoord = value.TexCoord;
        tile.TranslationKey = value.TranslationKey;

        _Tiles[key] = tile;
    }

    public static void RegisterCustomTile<T>(byte id, T tile) where T : Tile
    {
        var key = _Tiles.Keys.First(k => k.Id == id);
        var value = _Tiles[key];

        tile.Size = value.Size;
        tile.TexCoord = value.TexCoord;
        tile.TranslationKey = value.TranslationKey;

        _Tiles[key] = tile;
    }
}