using System.Numerics;

namespace d9.aoc._23.day5;
public readonly struct Range<T>(T start, T length)
    where T : INumber<T>
{
    public readonly T Start = start;
    public readonly T End = start + length;
    public readonly T Length = length;
    public bool Contains(T t) => t >= Start && t <= End;
    // https://stackoverflow.com/questions/3269434/whats-the-most-efficient-way-to-test-if-two-ranges-overlap
    public bool OverlapsWith(Range<T> other)
        => Start <= other.End && End >= other.Start;
    public IEnumerable<T> AllValues
    {
        get
        {
            for (T t = Start; t <= End; t++)
                yield return t;
        }
    }
    public override string ToString()
        => $"[{Start}, {End}]";
}