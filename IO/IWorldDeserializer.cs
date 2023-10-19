using System.Diagnostics.CodeAnalysis;

namespace BuildingGame.IO;

public interface IWorldDeserializer
{
    string Header { get; }
    
    bool TryDeserialize(BinaryReader reader, out World world, out string? log);
}