namespace BuildingGame;

public static class Tiles
{
    // this wont be for long because i'll implement tile loading from atlas.json lmao
    private static readonly Dictionary<TileKind, Tile> _tiles = new Dictionary<TileKind, Tile>()
    {
        { TileKind.SmoothStone, new Tile(0, 0) },
        { TileKind.Planks, new Tile(1, 0) },
        { TileKind.Dirt, new Tile(2, 0) },
        { TileKind.GrassBlock, new Tile(3, 0) },
        { TileKind.Grass, new Tile(4, 0) },
        { TileKind.Sand, new Tile(5, 0) },
        { TileKind.Water, new Tile(6, 0) },
        { TileKind.Lava, new Tile(7, 0) },
        { TileKind.Obsidian, new Tile(8, 0) },
        { TileKind.Door, new Tile(9, 0) }, // atm it will be built from 1 tile
        { TileKind.Stone, new Tile(0, 1) },
        { TileKind.WhitePlate, new Tile(1, 1) },
        { TileKind.GreenPlate, new Tile(2, 1) },
        { TileKind.RedPlate, new Tile(3, 1) },
        { TileKind.BluePlate, new Tile(4, 1) },
        { TileKind.Chamomile, new Tile(5, 1) },
        { TileKind.RedTulip, new Tile(6, 1) },
        { TileKind.ChamomilePot, new Tile(7, 1) },
        { TileKind.RedTulipPot, new Tile(8, 1) },
        { TileKind.WoodenStairs, new Tile(0, 2) },
        { TileKind.WoodenSlab, new Tile(1, 2) },
        { TileKind.StoneStairs, new Tile(2, 2) },
        { TileKind.StoneSlab, new Tile(3, 2) },
        { TileKind.WoodenPole, new Tile(4, 2) },
        { TileKind.WoodenPoleHandle, new Tile(5, 2) },
        { TileKind.StonePole, new Tile(6, 2) },
        { TileKind.StonePoleHandle, new Tile(7, 2) },
        { TileKind.Log, new Tile(8, 2) },
        { TileKind.LogTop, new Tile(9, 2) },
        { TileKind.Leaves, new Tile(0, 3) },
        { TileKind.Glass, new Tile(1, 3) },
        { TileKind.WhiteWool, new Tile(2, 3) },
        { TileKind.RedWool, new Tile(3, 3) },
        { TileKind.GreenWool, new Tile(4, 3) },
        { TileKind.BlueWool, new Tile(5, 3) },
        { TileKind.YellowWool, new Tile(6, 3) },
        { TileKind.BlackWool, new Tile(7, 3) },
        { TileKind.DarkGrayWool, new Tile(8, 3) },
        { TileKind.GrayWool, new Tile(9, 3) },
        { TileKind.Sponge, new Tile(0, 4) },
        { TileKind.InfectionBlock, new Tile(1, 4) }
    };

    public static Tile GetTile(TileKind kind)
    {
        if (kind == TileKind.Air) throw new ArgumentException("TileKind could not be an Air", nameof(kind));
        return _tiles[kind];
    }
}