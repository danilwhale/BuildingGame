using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles.Dynamic;

public class InfectionTile : Tile
{
    protected override void OnTick(World world, TileInfo info, int x, int y)
    {
        if (!Settings.EnableInfectionTile) return;
        
        if (info.Data.CurrentTick % 3 != 0) return;

        if (world[x - 1, y] != 0) world[x - 1, y] = info.Clone();
        if (world[x + 1, y] != 0) world[x + 1, y] = info.Clone();
        if (world[x, y - 1] != 0) world[x, y - 1] = info.Clone();
        if (world[x, y + 1] != 0) world[x, y + 1] = info.Clone();
    }
}