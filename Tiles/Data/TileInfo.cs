namespace BuildingGame.Tiles.Data;

public struct TileInfo
{
    public byte Id;
    public TileFlags Flags;

    public TileInfo(byte id, TileFlags flags)
    {
        Id = id;
        Flags = flags;
    }
    
    public static implicit operator TileInfo(byte id) => new TileInfo(id, TileFlags.Default);
    public static implicit operator byte(TileInfo info) => info.Id;
}