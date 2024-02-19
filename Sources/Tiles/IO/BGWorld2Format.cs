using System.Numerics;
using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles.IO;

public static class BGWorld2Format
{
    public const string Header = "BGWORLD2";

    public static void Register()
    {
        WorldIO.RegisterSerializer(new Serializer());
        WorldIO.RegisterDeserializer(new Deserializer());
    }

    private static void PushTile(BinaryWriter bw, TileInfo tile)
    {
        bw.Write(tile.Id);
        bw.Write(tile.Flags.RotationAsFloat());
        bw.Write(false); // we dont know if tile is flipped
    }

    private static TileInfo PopTile(BinaryReader br)
    {
        var flags = new TileFlags(br.ReadSingle());
        if (br.ReadBoolean()) flags.FlipRotation();

        return new TileInfo(br.ReadByte(), flags);
    }

    public class Serializer : IWorldSerializer
    {
        public string Header => BGWorld2Format.Header;

        public bool TrySerialize(BinaryWriter writer, World world, out string? log)
        {
            log = "Serialization as BGWORLD2 is not supported anymore";
            return false;
        }
    }

    public class Deserializer : IWorldDeserializer
    {
        public string Header => BGWorld2Format.Header;

        public bool TryDeserialize(BinaryReader reader, out World world, out string? log)
        {
            world = new World();
            world!.PlayerPosition = new Vector2(reader.ReadSingle(), reader.ReadSingle());

            for (var x = 0; x < world.Width; x++)
            for (var y = 0; y < world.Height; y++)
                world[x, y] = PopTile(reader);

            log = null;
            return true;
        }
    }
}