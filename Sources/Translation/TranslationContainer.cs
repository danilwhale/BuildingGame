namespace BuildingGame.Translation;

public readonly struct TranslationContainer
{
    private static TranslationContainer _default;

    public static TranslationContainer Default
    {
        get
        {
            if (!_default.IsEmpty()) return _default;

            TranslationLoader.TryLoadTranslation(TranslationLoader.DefaultTranslationPath, out _default);
            return _default;
        }
    }
    private readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

    public TranslationContainer(Dictionary<string, string> translations)
    {
        _translations = translations;
    }

    public string GetTranslatedName(string name)
    {
        return _translations.GetValueOrDefault(name) ?? name;
    }

    public bool IsEmpty()
    {
        return _translations == null || _translations.Count < 1;
    }
}