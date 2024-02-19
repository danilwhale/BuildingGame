using BuildingGame.Tiles.Data;

namespace BuildingGame.Tiles.Dynamic;

public class SandTile : Tile
{
    private static readonly byte WaterId;
    private static readonly byte LavaId;
    private static readonly byte GlassId;

    static SandTile()
    {
        WaterId = Tiles.GetId("water");
        LavaId = Tiles.GetId("lava");
        GlassId = Tiles.GetId("glass");
    }

    protected override void OnTick(World world, TileInfo info, int x, int y)
    {
        if (!Settings.EnableDynamicTiles) return;
        
        if (world.IsTileNear(x, y, LavaId))
        {
            world[x, y] = GlassId;
            return;
        }
        
        var tileBelow = world[x, y + 1];
        
        if (y >= world.Height - 1) return;
        if (tileBelow == info.Id || (tileBelow != 0 && tileBelow != WaterId)) return;

        world[x, y] = 0;
        world[x, y + 1] = info.Clone();
    }
}