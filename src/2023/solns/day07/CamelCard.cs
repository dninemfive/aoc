namespace d9.aoc._23.day07;
public readonly struct CamelCard(char c, bool jokerMode = false) : IEquatable<CamelCard>, IComparable<CamelCard>
{
    private readonly char _c = c;
    public readonly bool JokerMode = jokerMode;
    public int Value => _c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => JokerMode ? 1 : 11,
        'T' => 10,
        >= '0' and <= '9' => _c - '0',
        _ => throw new ArgumentOutOfRangeException(nameof(_c))
    };
    #region miscellaneous operators
    public static implicit operator char(CamelCard cc) => cc._c;
    public override bool Equals(object? obj)
        => obj is CamelCard card && Equals(card);
    public bool Equals(CamelCard other)
        => this == other;
    public override int GetHashCode()
        => HashCode.Combine(_c, JokerMode);
    public int CompareTo(CamelCard other)
        => Value.CompareTo(other.Value);
    public static bool operator ==(CamelCard a, CamelCard b)
        => a.JokerMode == b.JokerMode && a.Value == b.Value;
    public static bool operator !=(CamelCard a, CamelCard b)
        => !(a == b);
    #endregion miscellaneous operators
}