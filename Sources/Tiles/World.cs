using System.Numerics;
using BuildingGame.Tiles.Data;
using BuildingGame.Tiles.IO;

namespace BuildingGame.Tiles;

public partial class World
{
    public const int DefaultSize = 256;
    public readonly int ChunkHeight;
    public readonly int ChunkWidth;
    public readonly int Height;

    public readonly int Width;

    private readonly Chunk[][] _chunks;

    public Vector2 PlayerPosition;
    public float PlayerZoom = 1.0f;

    public World()
        : this(DefaultSize, DefaultSize)
    {
    }

    public World(int width, int height)
    {
        Width = width;
        Height = height;
        ChunkWidth = width / Chunk.Size;
        ChunkHeight = height / Chunk.Size;

        _chunks = new Chunk[ChunkWidth][];
        for (var x = 0; x < ChunkWidth; x++)
        {
            _chunks[x] = new Chunk[ChunkHeight];
            for (var y = 0; y < ChunkHeight; y++) _chunks[x][y] = new Chunk(this, x, y);
        }
    }

    public TileInfo this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return 0;

            var pos = WorldToChunk(x, y);
            pos.WorldToTile(x, y, out var tx, out var ty);

            return _chunks[pos.X][pos.Y][tx, ty];
        }
        set
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            var pos = WorldToChunk(x, y);
            pos.WorldToTile(x, y, out var tx, out var ty);

            _chunks[pos.X][pos.Y][tx, ty] = value;
        }
    }

    public void Update()
    {
        for (var x = ChunkWidth - 1; x >= 0; x--)
        for (var y = ChunkHeight - 1; y >= 0; y--)
            _chunks[x][y].Update();
    }

    public void Draw(Player player)
    {
        var view = player.GetViewRectangle();

        for (var x = 0; x < ChunkWidth; x++)
        for (var y = 0; y < ChunkHeight; y++)
            if (CheckCollisionRecs(
                    new Rectangle(x * Chunk.ViewSize, y * Chunk.ViewSize, Chunk.ViewSize, Chunk.ViewSize), view))
                _chunks[x][y].Draw();
    }

    public ChunkPosition WorldToChunk(int x, int y)
    {
        var cx = x / Chunk.Size;
        var cy = y / Chunk.Size;

        return new ChunkPosition(cx, cy);
    }

    public void Load(string path)
    {
        if (!WorldIO.TryDeserializeWorld(path, out var world)) return;
        if (Width != world.Width || Height != world.Height) return;

        PlayerPosition = world.PlayerPosition;
        PlayerZoom = world.PlayerZoom;

        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
            this[x, y] = world[x, y];
    }

    public void Save(string path)
    {
        WorldIO.TrySerializeWorld<BGWorld21Format.Serializer>(path, this);
    }
}