using System.ComponentModel;

namespace BuildingGame.Tiles.Atlas;

public struct AtlasTile
{
    public AtlasTile()
    {
        Atlas = default;
    }

    public AtlasVec2 Atlas { get; set; }
    public AtlasVec2 Size { get; set; } = new AtlasVec2(1, 1);
    
    
}