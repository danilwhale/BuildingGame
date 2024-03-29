namespace BuildingGame.Tiles.Data;

public struct TileInfo
{
    public byte Id;
    public TileFlags Flags;
    public TileData Data = new();

    public TileInfo(byte id, TileFlags flags)
    {
        Id = id;
        Flags = flags;
    }

    public TileInfo Clone()
    {
        return new TileInfo(Id, Flags);
    }

    public static implicit operator TileInfo(byte id)
    {
        return new TileInfo(id, TileFlags.Default);
    }

    public static implicit operator byte(TileInfo info)
    {
        return info.Id;
    }
}