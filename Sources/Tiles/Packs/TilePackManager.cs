using BuildingGame.Translation;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildingGame.Tiles.Packs;

public static class TilePackManager
{
    private const string TilePacksPath = "TilePacks";
    private const string PackFileName = "pack";

    public static List<TilePack> TilePacks = new();

    private static readonly IDeserializer _yaml = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static void Load()
    {
        TilePacks.Clear();
        
        if (!Directory.Exists(TilePacksPath))
        {
            Directory.CreateDirectory(TilePacksPath);
            return;
        }

        foreach (var dir in Directory.EnumerateDirectories(TilePacksPath))
        foreach (var file in Directory.EnumerateFiles(dir))
        {
            if (Path.GetExtension(file) is not (".yaml" or ".yml") ||
                Path.GetFileNameWithoutExtension(file).ToLower() != PackFileName) continue;

            try
            {
                var tilePack = _yaml.Deserialize<TilePackInfo>(File.ReadAllText(file));
                TilePacks.Add(new TilePack(dir, tilePack));
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public static void Apply(TilePack pack)
    {
        Resources.Reload(Path.Join(pack.Path, pack.Info.AssetsPath));
        TranslationContainer.Default.Reload(TranslationLoader.TranslationPath);
        
        Settings.CurrentTilePack = pack.Path.Split('/', '\\').Last();
        
        Program.LoadWindowIcon();
    }
    
    public static TilePack Find(string path)
    {
        return TilePacks.FirstOrDefault(i =>
        {
            var packDirectoryName = i.Path.Split('/', '\\').Last();
            var pathDirectoryName = path.Split('/', '\\').Last();

            return string.Equals(packDirectoryName, pathDirectoryName, StringComparison.CurrentCultureIgnoreCase);
        });
    }
}