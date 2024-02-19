namespace BuildingGame.Tiles;

public partial class World
{
    public bool IsTileNear(int x, int y, byte nearTileId)
    {
        return this[x - 1, y] == nearTileId ||
               this[x + 1, y] == nearTileId ||
               this[x, y - 1] == nearTileId ||
               this[x, y + 1] == nearTileId;
    }
}