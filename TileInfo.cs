namespace BuildingGame;

public class TileInfo
{
    public byte Type { get; }
    public TileFlags Flags { get; }

    public TileInfo(byte type, TileFlags flags)
    {
        Type = type;
        Flags = flags;
    }

    public static implicit operator byte(TileInfo info) => info.Type;
    public static implicit operator TileInfo(byte type) => new TileInfo(type, TileFlags.Default);
}