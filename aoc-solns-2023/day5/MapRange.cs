using System.Numerics;
namespace d9.aoc._23.day5;
public class MapRange<T>
    where T : struct, INumber<T>
{
    public Range<T> Source { get; private set; }
    public Range<T> Destination { get; private set; }
    public T Diff => Destination.Start - Source.Start;
    public MapRange(string line)
    {
        List<T> values = line.ToMany<T>().ToList();
        (T sourceStart, T destStart, T length) = (values[1], values[0], values[2]);
        Source = new(sourceStart, length);
        Destination = new(destStart, length);
    }
    public T? this[T t]
        => Source.Contains(t) ? t + Diff : null;
    public override string ToString() => $"{{{Source} -> {Destination} ({Diff})}}";
}