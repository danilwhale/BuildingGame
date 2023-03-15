namespace BuildingGame.Tiles;

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
    public static implicit operator string(TileInfo info)
    {
        if (info.Type > 0) return Tile.GetId(info.Type);
        return "";
    }
    public static implicit operator Tile(TileInfo info) => Tile.GetTile(info.Type);
    public static implicit operator TileInfo(byte type) => new TileInfo(type, TileFlags.Default);
    public static implicit operator TileInfo(string id) => new TileInfo(Tile.GetNId(id), TileFlags.Default);
}