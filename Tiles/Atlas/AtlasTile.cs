using Newtonsoft.Json;

namespace BuildingGame.Tiles.Atlas;

public struct AtlasTile
{
    public JsonVec2 AtlasOffset { get; set; }
    public JsonVec2 Size { get; set; }
    
    [JsonConstructor]
    AtlasTile(JsonVec2 AtlasOffset, JsonVec2? Size)
    {
        this.AtlasOffset = AtlasOffset;
        this.Size = Size ?? new JsonVec2() { X = 1, Y = 1 };
    }
}