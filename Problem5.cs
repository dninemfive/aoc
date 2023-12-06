using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem5
{
    private static Dictionary<string, XToYMap<long>> _mapMap = new();
    [SolutionToProblem(5)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<long> seeds = lines.First().Split(": ")[1].ToMany<long>();
        _mapMap = ParseLines(lines[2..]);
        Console.WriteLine(seeds.ListNotation());
        foreach(XToYMap<long> map in _mapMap.Values)
        {
            Console.WriteLine(map.FullString);
        }
        yield return seeds.Select(LocationFor).Min();
        Range<long>[] pairs = new Range<long>[seeds.Count() / 2];     
        for(int i = 0; i < pairs.Length; i++)
            pairs[i] = new(seeds.ElementAt(i * 2), seeds.ElementAt(i * 2 + 1));
        yield return LowestLocationFor(pairs);
    }   
    public static Dictionary<string, XToYMap<long>> ParseLines(string[] lines)
    {
        string title = "";
        List<string> nonTitleLines = new();
        Dictionary<string, XToYMap<long>> result = new();
        foreach(string line in lines.Append(""))
        {
            if (line.Contains("-to-"))
            {
                title = line;
            }
            else if(string.IsNullOrWhiteSpace(line))
            {
                XToYMap<long> map = new(title, nonTitleLines);
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
    public static long LocationFor(long seed)
    {
        Console.WriteLine($"LocationFor({seed})");
        (string type, long val) cur = ("seed", seed);
        while(cur.type != "location")
        {
            cur = _mapMap[cur.type][cur];
            Console.WriteLine($"\t{cur.type,-8} {cur.val}");
        }
        return cur.val;
    }
    public static long LowestLocationFor(params Range<long>[] seedRanges)
    {
        IEnumerable<long> allCalculations = seedRanges.SelectMany(_mapMap["seed"].BreakPointsFor).Distinct().Order();
        Console.WriteLine($"Breakpoints: {allCalculations.ListNotation()}");
        long totalCalls = allCalculations.Count();
        long itemsPerPeriod = Math.Max(totalCalls / 100, 1);
        // Console.WriteLine($"LowestLocationFor([{totalCalls} items]), one period per {itemsPerPeriod} items:");
        // Console.WriteLine($"{".".Repeated(totalCalls / itemsPerPeriod)}");
        //long ct = 0;
        long result = long.MaxValue;
        static long min(long a, long b) => a < b ? a : b;
        foreach(long l in allCalculations)
        {
            result = min(result, LocationFor(l));
            //if (++ct % itemsPerPeriod == 0)
                //Console.Write($".");
        }
        //Console.WriteLine("\n");
        return result;
    }

}
public class XToYMap<T>(string title, IEnumerable<string> nonTitleLines)
    where T : struct, INumber<T>
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
                    return (ResultType, result.Value);
            }
            return (ResultType, input.val);
        }
    }
    public IEnumerable<T> BreakPointsFor(Range<T> range)
    {
        IEnumerable<Range<T>> matchingRanges = _ranges.Select(x => x.Source).Where(range.OverlapsWith);
        foreach (T t in matchingRanges.SelectMany(x => new List<T>() { x.Start, x.End }).Order())
        {
            if (range.Contains(t))
                yield return t;
        }
    }
    public override string ToString() => Name;
    public string FullString
        => $"{Name} {{\n\t{_ranges.Select(x => $"{x}").Aggregate((x, y) => $"{x}\n\t{y}")}\n}}";
}
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