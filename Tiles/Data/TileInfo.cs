namespace BuildingGame.Tiles.Data;

public record TileInfo(byte Id, TileFlags Flags)
{
    public static implicit operator TileInfo(byte id) => new TileInfo(id, TileFlags.Default);
    public static implicit operator byte(TileInfo info) => info.Id;
}