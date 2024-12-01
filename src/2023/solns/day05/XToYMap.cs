using d9.aoc.core;
using System.Numerics;

namespace d9.aoc._23.day5;
public class XToYMap<T>(string title, IEnumerable<string> nonTitleLines)
    where T : struct, INumber<T>
{
    public string InputType => title.Split("-")[0];
    public string ResultType => title.SplitAndTrim("-", " ")[2];
    public string Name => $"{InputType}-to-{ResultType} map";
    private readonly List<MapRange<T>> _ranges = [.. nonTitleLines.Select(x => new MapRange<T>(x))
                                                                .OrderBy(x => x.Source.Start)];
    public IEnumerable<MapRange<T>> Ranges => _ranges;
    public (string type, T val) this[(string type, T val) input]
    {
        get
        {
            if (input.type != InputType)
                throw new ArgumentException($"A {Name} can't convert a value of type {input.type}!");
            foreach (MapRange<T> range in _ranges)
            {
                T? result = range[input.val];
                if (result is not null)
                    return (ResultType, result.Value);
            }
            return (ResultType, input.val);
        }
    }
    public IEnumerable<T> BreakPointsFor(Range<T> range)
    {
        IEnumerable<Range<T>> matchingRanges = _ranges.Select(x => x.Source).Where(range.OverlapsWith);
        yield return range.Start;
        yield return range.End;
        foreach (Range<T> range2 in matchingRanges)
        {
            foreach (T t in new List<T>() { range2.Start - T.One, range2.Start, range2.End, range2.End + T.One })
                if (range.Contains(t))
                    yield return t;
        }
    }
    public override string ToString() => Name;
    public string FullString
        => $"{Name} {{\n\t{_ranges.Select(x => $"{x}").Aggregate((x, y) => $"{x}\n\t{y}")}\n}}";
}