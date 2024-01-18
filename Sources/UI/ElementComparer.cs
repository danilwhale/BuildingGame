namespace BuildingGame.UI;

public class ElementComparer : IComparer<Element>
{
    public int Compare(Element? x, Element? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.ZIndex.CompareTo(y.ZIndex);
    }
}