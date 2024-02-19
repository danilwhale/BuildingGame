namespace BuildingGame;

public static class Resources
{
    public static string ResourcesPath = FallbackResourcesPath;
    public const string FallbackResourcesPath = "Assets/";
    
    private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
    private static Dictionary<string, Image> _images = new Dictionary<string, Image>();

    public static Texture2D GetTexture(string key)
    {
        key = key.ToLower();
        if (_textures.TryGetValue(key, out var texture)) return texture;

        texture = LoadTexture(GetPath(key));
        _textures[key] = texture;
        return texture;
    }

    public static Image GetImage(string key)
    {
        key = key.ToLower();
        if (_images.TryGetValue(key, out var image)) return image;

        image = LoadImage(GetPath(key));
        _images[key] = image;
        return image;
    }

    public static string GetText(string key)
    {
        return File.ReadAllText(GetPath(key));
    }

    public static string GetPath(string key)
    {
        var root = !File.Exists(Path.Join(ResourcesPath, key)) ? FallbackResourcesPath : ResourcesPath;
        return Path.Join(root, key);
    }

    public static void Reload(string newResourcesPath)
    {
        ResourcesPath = newResourcesPath;

        foreach (var texture in _textures)
        {
            _textures[texture.Key] = LoadTexture(GetPath(texture.Key));
        }
        
        foreach (var image in _images)
        {
            _images[image.Key] = LoadImage(GetPath(image.Key));
        }
    }

    public static void Free()
    {
        foreach (var texture in _textures)
        {
            UnloadTexture(texture.Value);
        }
        _textures.Clear();

        foreach (var image in _images)
        {
            UnloadImage(image.Value);
        }
        _images.Clear();
    }
}