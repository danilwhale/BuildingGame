namespace BuildingGame;

public static class ConfigParser
{
    public static Dictionary<string, string> Parse(string fileName)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        foreach (var line in File.ReadAllLines(fileName))
        {
            string[] kv = line.Split(':');
            if (kv.Length > 2 || kv.Length < 2) continue;
            dict.Add(kv[0].Trim(), kv[1].Trim());
        }

        return dict;
    }

    public static Dictionary<string, (string m, string r)> ParseTriple(string fileName)
    {
        Dictionary<string, (string, string)> dict = new Dictionary<string, (string, string)>();

        foreach (var line in File.ReadAllLines(fileName))
        {
            string[] kv = line.Split(':');
            if (kv.Length > 3 || kv.Length < 3) continue;
            dict.Add(kv[0].Trim(), (kv[1].Trim(), kv[2].Trim()));
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