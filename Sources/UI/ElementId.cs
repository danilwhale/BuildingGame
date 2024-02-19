using System.Text;

namespace BuildingGame.UI;

public struct ElementId
{
    public readonly string? Root;
    public string Name;

    public ElementId(ElementId root, string name)
    {
        Root = root.ToString();
        Name = name;
    }

    public ElementId(string root, string name)
    {
        Root = root;
        Name = name;
    }

    public ElementId(string name)
    {
        var split = name.Split("::");

        switch (split.Length)
        {
            case >= 2:
                Root = split[0];
                Name = split[1];
                break;
            case >= 1:
                Name = split[0];
                break;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is string str) return Equals(str);
        if (obj is ElementId id) return Equals(id);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Root, Name);
    }

    private bool Equals(ElementId id)
    {
        return string.Equals(Root, id.Root, StringComparison.CurrentCultureIgnoreCase) &&
               string.Equals(Name, id.Name, StringComparison.CurrentCultureIgnoreCase);
    }

    private bool Equals(string id)
    {
        return string.Equals(id, ToString(), StringComparison.CurrentCultureIgnoreCase);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        if (Root != null)
        {
            builder.Append(Root);
            builder.Append("::");
        }

        builder.Append(Name);
        return builder.ToString();
    }

    public static implicit operator string(ElementId id)
    {
        return id.ToString();
    }

    public static bool operator ==(ElementId a, ElementId b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ElementId a, ElementId b)
    {
        return !(a == b);
    }

    public static bool operator ==(ElementId a, string b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ElementId a, string b)
    {
        return !(a == b);
    }
}