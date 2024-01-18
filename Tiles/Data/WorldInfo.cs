namespace BuildingGame.Tiles.Data;

public record struct WorldInfo(string Path, WorldInfo.InfoRecord Info)
{
    public record struct InfoRecord(string Name, TimeSpan PlayTime);
}