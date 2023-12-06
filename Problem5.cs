using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem5
{
    private static Dictionary<string, XToYMap> _mapMap = new();
    [SolutionToProblem(5)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<long> seeds = lines.First().Split(": ")[1].ToMany<long>();
        _mapMap = ParseLines(lines[2..]);
        yield return seeds.Select(LocationFor).Min();
        (long, long)[] pairs = new (long, long)[seeds.Count() / 2];     
        for(int i = 0; i < pairs.Length; i++)
            pairs[i] = (seeds.ElementAt(i * 2), seeds.ElementAt(i * 2 + 1));
        yield return LowestLocationFor(pairs);
    }   
    public static Dictionary<string, XToYMap> ParseLines(string[] lines)
    {
        string title = "";
        List<string> nonTitleLines = new();
        Dictionary<string, XToYMap> result = new();
        foreach(string line in lines.Append(""))
        {
            if (line.Contains("-to-"))
            {
                title = line;
            }
            else if(string.IsNullOrWhiteSpace(line))
            {
                XToYMap map = new(title, nonTitleLines);
                result[map.InputType] = map;
                nonTitleLines.Clear();
            }
            else
            {
                nonTitleLines.Add(line);
            }
        }
        return result;
    }
    // possible paths:
    // seed -> soil -> fertilizer -> water -> light -> temperature -> humidity -> location
    // ...oh. that's the only one.
    public static long LocationFor(long seed)
    {
        (string type, long val) cur = ("seed", seed);
        while(cur.type != "location")
            cur = _mapMap[cur.type][cur];
        return cur.val;
    }
    public static long LowestLocationFor(params (long start, long length)[] seedRanges)
    {
        long itemsPerPeriod = (long)1e7;
        Console.WriteLine($"LowestLocationFor()");
        Console.WriteLine($"{".".Repeated(seedRanges.Select(x => x.length).Sum() / itemsPerPeriod)}");
        long ct = 0;
        long result = long.MaxValue;
        static long min(long a, long b) => a < b ? a : b;
        foreach((long start, long length) in seedRanges)
        {
            for(long l = 0; l < length; l++)
            {
                result = min(result, LocationFor(start + l));
                if (++ct % itemsPerPeriod == 0)
                    Console.WriteLine($".");
            }
        }
        Console.WriteLine("\n");
        return result;
    }

}
public class XToYMap<T>(string title, IEnumerable<string> nonTitleLines)
    where T : INumber<T>
{
    public string InputType => title.Split("-")[0];
    public string ResultType => title.SplitAndTrim("-", " ")[2];
    public string Name => $"{InputType}-to-{ResultType} map";
    private readonly List<MapRange<T>> _ranges = [ .. nonTitleLines.Select(x => new MapRange<T>(x))
                                                                .OrderBy(x => x.Source.Start) ];
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
                    return (ResultType, result);
            }
            return (ResultType, input.val);
        }
    }
    public IEnumerable<T> BreakPointsFor(Range<T> range)
    {
        T cur = range.Start;
        
    }
    public override string ToString() => Name;
}
public readonly struct Range<T>(T start, T length)
    where T : INumber<T>
{
    public readonly T Start = start;
    public readonly T End = start + length;
    public bool Contains(T t) => t >= Start && t <= End;
}
public class MapRange<T>
    where T : INumber<T>
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
        => Source.Contains(t) ? t + Diff : default;
}