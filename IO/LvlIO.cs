namespace BuildingGame.IO;

/// <summary>
/// WARNING: There's no implemented conversion for LVL format
/// </summary>
public static class LvlIO
{
    public const string Header = "LVL";
    
    public class Serializer : IWorldSerializer
    {
        public string Header => LvlIO.Header;
        
        public bool TrySerialize(BinaryWriter writer, World world, out string? log)
        {
            log = "PRE-ALPHA 0.1.0 LEVEL FORMAT IS CURRENTLY NOT SUPPORTED";
            return false;
        }
    }

    public class Deserializer : IWorldDeserializer
    {
        public string Header => LvlIO.Header;
        
        public bool TryDeserialize(BinaryReader reader, out World world, out string? log)
        {
            world = new World();
            log = "PRE-ALPHA 0.1.0 LEVEL FORMAT IS CURRENTLY NOT SUPPORTED";
            return false;
        }
    }

    public static void Register()
    {
        WorldIO.RegisterSerializer(new Serializer());
        WorldIO.RegisterDeserializer(new Deserializer());
    }
}