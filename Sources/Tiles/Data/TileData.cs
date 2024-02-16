using BuildingGame.Tiles.Dynamic;

namespace BuildingGame.Tiles.Data;

public class TileData
{
    public int CurrentTick = 0;
    public float TickTimer = 1.0f / Tile.TickCount;
}