using BuildingGame.Tiles.Data;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BuildingGame.Tiles;

public static class WorldManager
{
    public const string WorldsPath = "Worlds";
    public const string WorldInfoFile = "Info";
    public const string WorldLevelFile = "Level.dat";

    public static readonly List<WorldInfo> Worlds = new();

    private static readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    private static readonly ISerializer _serializer = new SerializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .Build();

    public static void ReadWorlds()
    {
        Worlds.Clear();

        foreach (var dir in Directory.EnumerateDirectories(WorldsPath))
        foreach (var file in Directory.EnumerateFiles(dir))
        {
            var fileName = Path.GetFileNameWithoutExtension(file).ToLower();
            if (fileName != WorldInfoFile.ToLower()) continue;

            var ext = Path.GetExtension(file).ToLower();

            var content = File.ReadAllText(file);

            switch (ext)
            {
                case ".txt":
                    Worlds.Add(new WorldInfo(dir, new WorldInfo.InfoRecord(content, TimeSpan.Zero)));
                    break;

                case ".yaml":
                    var deserialized = _deserializer.Deserialize<WorldInfo.InfoRecord>(content);
                    Worlds.Add(new WorldInfo(dir, deserialized));
                    break;

                default:
                    continue;
            }

            break;
        }
    }

    public static WorldInfo CreateInfo(string name)
    {
        var invalidChars = Path.GetInvalidPathChars();
        var pathChars = name.Select(c => Array.IndexOf(invalidChars, c) >= 0 ? ' ' : c);
        var path = Path.Join(WorldsPath, new string(pathChars.ToArray()));

        var info = new WorldInfo(path, new WorldInfo.InfoRecord(name, TimeSpan.Zero));

        return info;
    }

    public static WorldInfo Find(string path)
    {
        return Worlds.FirstOrDefault(i =>
            string.Equals(i.Path, path, StringComparison.CurrentCultureIgnoreCase));
    }

    public static void LoadWorld(ref World world, string name)
    {
        var info = Find(name);
        if (string.IsNullOrWhiteSpace(info.Path)) return;

        world.Load(Path.Join(info.Path, WorldLevelFile));
    }

    public static void WriteWorld(World world, WorldInfo info)
    {
        WriteInfo(info);
        world.Save(Path.Join(info.Path, WorldLevelFile));
    }

    public static void WriteInfo(WorldInfo info)
    {
        Directory.CreateDirectory(info.Path);

        var content = _serializer.Serialize(info.Info);
        File.WriteAllText(Path.Join(info.Path, WorldInfoFile + ".yaml"), content);
    }

    public static void DeleteWorld(WorldInfo info)
    {
        Directory.Delete(info.Path, true);
        Worlds.Remove(info);
    }
}