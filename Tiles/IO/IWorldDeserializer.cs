namespace BuildingGame.Tiles.IO;

public interface IWorldDeserializer
{
    string Header { get; }
    
    bool TryDeserialize(BinaryReader reader, out World world, out string? log);
}