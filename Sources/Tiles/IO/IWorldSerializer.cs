namespace BuildingGame.Tiles.IO;

public interface IWorldSerializer
{
    string Header { get; }

    bool TrySerialize(BinaryWriter writer, World world, out string? log);
}