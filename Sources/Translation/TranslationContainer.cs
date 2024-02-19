namespace BuildingGame.Translation;

public class TranslationContainer
{
    private static TranslationContainer _default = new();
    private Dictionary<string, string> _translations;

    public TranslationContainer()
    {
        _translations = new Dictionary<string, string>();
    }

    public TranslationContainer(Dictionary<string, string> translations)
    {
        _translations = translations;
    }

    public static TranslationContainer Default
    {
        get
        {
            if (!_default.IsEmpty()) return _default;

            TranslationLoader.TryLoadTranslation(TranslationLoader.TranslationPath, out _default);
            return _default;
        }
    }

    public string GetTranslatedName(string name)
    {
        return _translations.GetValueOrDefault(name) ?? name;
    }

    public void Reload(string path)
    {
        TranslationLoader.TryLoadTranslation(path, out var translation);
        _translations = translation._translations;
    }

    private bool IsEmpty()
    {
        return _translations is { Count: > 0 };
    }
}