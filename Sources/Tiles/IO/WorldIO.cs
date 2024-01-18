using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace BuildingGame.Tiles.IO;

public static class WorldIO
{
    private static List<IWorldDeserializer> _deserializers = new List<IWorldDeserializer>();
    private static List<IWorldSerializer> _serializers = new List<IWorldSerializer>();

    private static List<IWorldDeserializer> _backupDeserializers = new List<IWorldDeserializer>();

    public static void RegisterDeserializer<T>(T deserializer) where T : IWorldDeserializer
    {
        if (_deserializers.Find(m => m.GetType() == typeof(T)) != null)
            throw new InvalidOperationException($"Deserializer of type {typeof(T)} is already registered");
        _deserializers.Add(deserializer);
    }

    public static void RegisterSerializer<T>(T serializer) where T : IWorldSerializer
    {
        if (_serializers.Find(m => m.GetType() == typeof(T)) != null)
            throw new InvalidOperationException($"Serializer of type {typeof(T)} is already registered");
        _serializers.Add(serializer);
    }

    public static bool TryDeserializeWorld(string path, [NotNullWhen(true)] out World? world)
    {
        world = null;
        if (!File.Exists(path)) return false;

        try
        {
            using var fileStream = File.OpenRead(path);
            using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
            using var reader = new BinaryReader(gzipStream);

            string header = reader.ReadString();
            IWorldDeserializer deserializer =
                _deserializers.Find(d => string.Equals(d.Header, header, StringComparison.CurrentCultureIgnoreCase))
                ?? throw new IOException("Unknown or corrupted world header: " + header);

            if (_backupDeserializers.FindIndex(d => d.GetType() == deserializer.GetType()) >= 0)
            {
                File.Copy(path, path + ".old");
            }
            
            if (deserializer.TryDeserialize(reader, out var outWorld, out var log))
            {
                world = outWorld;
                return true;
            }
            
            // TODO: print log from deserializer
            return false;
        }
        catch (Exception ex)
        {
            // TODO: implement exception logging
            return false;
        }
    }

    public static bool TrySerializeWorld<TSerializer>(string path, World world) where TSerializer : IWorldSerializer
    {
        try
        {
            using var fileStream = File.OpenWrite(path);
            using var gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
            using var writer = new BinaryWriter(gzipStream);
            
            IWorldSerializer serializer =
                _serializers.Find(d => d.GetType() == typeof(TSerializer))
                ?? throw new IOException("Unable to find serializer of type " + typeof(TSerializer));
            
            writer.Write(serializer.Header);

            if (serializer.TrySerialize(writer, world, out var log))
            {
                return true;
            }
            
            // TODO: print log from serializer
            return false;
        }
        catch (Exception ex)
        {
            // TODO: implement exception logging
            return false;
        }
    }

    public static void AddDeserializerAsBackupable<TDeserializer>(TDeserializer deserializer)
        where TDeserializer : IWorldDeserializer
    {
        _backupDeserializers.Add(deserializer);
    }
}