namespace BuildingGame;

public static class ConfigParser
{
    public static Dictionary<string, string> Parse(string fileName)
    {
        var dict = new Dictionary<string, string>();
        if (!File.Exists(fileName)) return dict;

        foreach (var line in File.ReadAllLines(fileName))
        {
            string[] kv = line.Split(':');
            if (kv.Length > 2 || kv.Length < 2) continue;
            dict.Add(kv[0].Trim(), kv[1].Trim());
        }

        return dict;
    }

    public static Dictionary<string, (string left, string right)> ParseTriple(string fileName)
    {
        var dict = new Dictionary<string, (string, string)>();
        if (!File.Exists(fileName)) return dict;

        foreach (var line in File.ReadAllLines(fileName))
        {
            string[] kv = line.Split(':');
            if (kv.Length != 3) continue;
            dict.Add(kv[0].Trim(), (kv[1].Trim(), kv[2].Trim()));
        }

        return dict;
    }

    public static Dictionary<string, (string left, string middle, string right)> ParseFourth(string fileName)
    {
        var dict = new Dictionary<string, (string, string, string)>();
        if (!File.Exists(fileName)) return dict;

        foreach (var line in File.ReadAllLines(fileName))
        {
            string[] kv = line.Split(':');
            if (kv.Length != 4) continue;
            dict.Add(kv[0].Trim(), (kv[1].Trim(), kv[2].Trim(), kv[3].Trim()));
        }

        return dict;
    }

    public static void Write(string fileName, Dictionary<string, string> data)
    {
        List<string> strList = new List<string>();

        foreach (var key in data.Keys)
        {
            strList.Add($"{key}:{data[key]}");
        }

        File.WriteAllLines(fileName, strList.ToArray());
    }

    public static Vector2 ToVector2i(string str)
    {
        string[] xy = str.Split(',');
        if (xy.Length < 2) return new Vector2(0, 0);
        if (int.TryParse(xy[0], out int x) && int.TryParse(xy[1], out int y))
            return new(x, y);
        return new Vector2(0, 0);
    }
}