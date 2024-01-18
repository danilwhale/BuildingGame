namespace BuildingGame.Tiles.Atlas;

public struct AtlasTileKey
{
    public string Name;
    public byte Id;

    public AtlasTileKey(string name, byte id)
    {
        Name = name;
        Id = id;
    }
}