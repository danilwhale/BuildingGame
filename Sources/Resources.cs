namespace BuildingGame;

public static class Resources
{
    private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

    public static Texture2D GetTexture(string key)
    {
        key = key.ToLower();
        if (_textures.TryGetValue(key, out var texture)) return texture;

        texture = LoadTexture("Assets/" + key);
        _textures[key] = texture;
        return texture;
    }

    public static void Free()
    {
        foreach (var texture in _textures)
        {
            UnloadTexture(texture.Value);
        }
        _textures.Clear();
    }
}