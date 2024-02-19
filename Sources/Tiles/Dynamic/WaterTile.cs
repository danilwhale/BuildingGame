using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles.Dynamic;

public class WaterTile : LiquidTile
{
    private static readonly byte LavaId;
    private static readonly byte ObsidianId;

    static WaterTile()
    {
        LavaId = Tiles.GetId("lava");
        ObsidianId = Tiles.GetId("obsidian");
    }

    public WaterTile()
    {
        SpreadSpeed = 10;
    }

    protected override void OnTick(World world, TileInfo info, int x, int y)
    {
        base.OnTick(world, info, x, y);

        if (world.IsTileNear(x, y, LavaId)) world[x, y] = ObsidianId;
    }
}