namespace BuildingGame;

public static class Resources
{
    public const string FallbackResourcesPath = "Assets/";
    public static string ResourcesPath = FallbackResourcesPath;

    private static readonly Dictionary<string, Texture2D> _textures = new();
    private static readonly Dictionary<string, Image> _images = new();
    private static readonly Dictionary<string, Font> _fonts = new();

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
    
    public static Font GetFont(string key)
    {
        key = key.ToLower();
        if (_fonts.TryGetValue(key, out var font)) return font;

        font = LoadFont(GetPath(key));
        _fonts[key] = font;
        return font;
    }

    public static Font GetFont(string key, int fontSize, int[] codepoints)
    {
        key = key.ToLower();
        if (_fonts.TryGetValue(key, out var font)) return font;

        font = LoadFontEx(GetPath(key), fontSize, codepoints, codepoints.Length);
        _fonts[key] = font;
        return font;
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

    public static unsafe void Reload(string newResourcesPath)
    {
        ResourcesPath = newResourcesPath;

        foreach (var texture in _textures)
        {
            UnloadTexture(texture.Value);
            _textures[texture.Key] = LoadTexture(GetPath(texture.Key));
        }

        foreach (var image in _images)
        {
            UnloadImage(image.Value);
            _images[image.Key] = LoadImage(GetPath(image.Key));
        }

        foreach (var font in _fonts)
        {
            var codepoints = new int[font.Value.GlyphCount];
            for (var i = 0; i < font.Value.GlyphCount; i++)
            {
                codepoints[i] = font.Value.Glyphs[i].Value;
            }

            var fontSize = font.Value.BaseSize;
            
            UnloadFont(font.Value);
            _fonts[font.Key] = LoadFontEx(GetPath(font.Key), fontSize, codepoints, codepoints.Length);
        }
    }

    public static void Free()
    {
        foreach (var texture in _textures) UnloadTexture(texture.Value);
        _textures.Clear();

        foreach (var image in _images) UnloadImage(image.Value);
        _images.Clear();
        
        foreach (var font in _fonts) UnloadFont(font.Value);
        _fonts.Clear();
    }
}