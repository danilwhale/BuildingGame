using System.Numerics;
using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles.IO;

public static class BGWorld21Format
{
    public const string Header = "BGWORLD21";

    public static void Register()
    {
        WorldIO.RegisterSerializer(new Serializer());
        WorldIO.RegisterDeserializer(new Deserializer());
    }

    private static void PushTile(BinaryWriter bw, TileInfo tile)
    {
        bw.Write(tile.Id);
        bw.Write((byte)tile.Flags.Rotation);
    }

    private static TileInfo PopTile(BinaryReader br)
    {
        return new TileInfo(br.ReadByte(), new TileFlags((TileRotation)br.ReadByte()));
    }

    public class Serializer : IWorldSerializer
    {
        public string Header => BGWorld21Format.Header;

        public bool TrySerialize(BinaryWriter writer, World world, out string? log)
        {
            writer.Write(world.PlayerPosition.X);
            writer.Write(world.PlayerPosition.Y);
            writer.Write(world.PlayerZoom);

            for (var x = 0; x < world.Width; x++)
            for (var y = 0; y < world.Height; y++)
                PushTile(writer, world[x, y]);

            log = null;
            return true;
        }
    }

    public class Deserializer : IWorldDeserializer
    {
        public string Header => BGWorld21Format.Header;

        public bool TryDeserialize(BinaryReader reader, out World world, out string? log)
        {
            world = new World();
            world.PlayerPosition = new Vector2(reader.ReadSingle(), reader.ReadSingle());
            world.PlayerZoom = reader.ReadSingle();

            for (var x = 0; x < world.Width; x++)
            for (var y = 0; y < world.Height; y++)
                world[x, y] = PopTile(reader);

            log = null;
            return true;
        }
    }
}