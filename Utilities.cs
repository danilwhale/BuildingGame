namespace BuildingGame;

public static class Utilities
{
    public static void SafeForeach<TKey, TValue>(Dictionary<TKey, TValue> dict, Action<TKey, TValue> action) where TKey : notnull
    {
        int len = dict.Count;
        for (int i = 0; i < len; i++)
        {
            len = dict.Count;
            if (i >= len) break;

            var key = dict.Keys.ToArray()[i];
            var val = dict[key];

            action.Invoke(key, val);
        }
    }
}