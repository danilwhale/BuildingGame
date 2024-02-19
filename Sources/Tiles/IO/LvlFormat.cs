namespace BuildingGame.Tiles.IO;

public static class LvlFormat
{
    public const string Header = "LVL";

    public static void Register()
    {
        WorldIO.RegisterSerializer(new Serializer());
        WorldIO.RegisterDeserializer(new Deserializer());
    }

    public class Serializer : IWorldSerializer
    {
        public string Header => LvlFormat.Header;

        public bool TrySerialize(BinaryWriter writer, World world, out string? log)
        {
            log = "Serialization as LVL is not supported anymore";
            return false;
        }
    }

    public class Deserializer : IWorldDeserializer
    {
        public string Header => LvlFormat.Header;

        public bool TryDeserialize(BinaryReader reader, out World world, out string? log)
        {
            world = new World();

            for (var x = 0; x < world.Width; x++)
            for (var y = 0; y < world.Height; y++)
                world[x, y] = reader.ReadByte();

            log = null;
            return true;
        }
    }
}