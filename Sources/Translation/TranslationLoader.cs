using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildingGame.Translation;

public static class TranslationLoader
{
    public static readonly string TranslationPath = "Translation.yaml";

    private static readonly IDeserializer _yaml = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static bool TryLoadTranslation(string path, out TranslationContainer translation)
    {
        translation = new TranslationContainer();

        if (!File.Exists(Resources.GetPath(path))) return false;

        var content = Resources.GetText(path);
        var translationMap = _yaml.Deserialize<Dictionary<string, string>>(content);
        translation = new TranslationContainer(translationMap);

        return true;
    }
}