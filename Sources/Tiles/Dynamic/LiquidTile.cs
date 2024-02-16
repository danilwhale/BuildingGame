using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles.Dynamic;

public class LiquidTile : Tile
{
    public int SpreadSpeed = 1;
    
    protected override void OnTick(World world, TileInfo info, int x, int y)
    {
        if (!Settings.EnableDynamicTiles) return;
        
        if (info.Data.CurrentTick % (TickCount - SpreadSpeed) != 0) return;
        if (world[x, y + 1] == info.Id) return;

        if (CanExtendTo(world, info, x, y + 1))
        {
            world[x, y + 1] = info.Clone();
        }
        else if (CanExtendTo(world, info, x + 1, y))
        {
            world[x + 1, y] = info.Clone();
        }
        else if (CanExtendTo(world, info, x - 1, y))
        {
            world[x - 1, y] = info.Clone();
        }
    }

    private static bool CanExtendTo(World world, TileInfo info, int toX, int toY)
    {
        var toInfo = world[toX, toY];

        return toInfo.Id == 0;
    }
}