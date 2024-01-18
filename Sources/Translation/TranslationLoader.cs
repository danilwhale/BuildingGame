using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildingGame.Translation;

public static class TranslationLoader
{
    public const string DefaultTranslationPath = "Assets/Translation.yaml";
    
    private static readonly IDeserializer _yaml = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static bool TryLoadTranslation(string path, out TranslationContainer translation)
    {
        translation = new TranslationContainer();

        if (!File.Exists(path)) return false;

        var content = File.ReadAllText(path);
        var translationMap = _yaml.Deserialize<Dictionary<string, string>>(content);
        translation = new TranslationContainer(translationMap);
        
        return true;
    }
}