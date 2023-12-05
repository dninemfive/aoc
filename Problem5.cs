using System;
using System.Collections.Generic;
using System.Linq;
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
        Console.WriteLine(_mapMap.Select(x => $"{x.Key}: {x.Value}").Aggregate((x, y) => $"{x}\n{y}"));
        yield return seeds.Select(LocationFor).Min();
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
}
public class XToYMap(string title, IEnumerable<string> nonTitleLines)
{
    public string InputType => title.Split("-")[0];
    public string ResultType => title.SplitAndTrim("-", " ")[2];
    public string Name => $"{InputType}-to-{ResultType} map";
    private readonly List<MapRange> _ranges = [ .. nonTitleLines.Select(x => new MapRange(x))
                                                                .OrderBy(x => x.Source.Start) ];
    public (string type, long val) this[(string type, long val) input]
    {
        get
        {
            if (input.type != InputType)
                throw new ArgumentException($"A {Name} can't convert a value of type {input.type}!");
            foreach (MapRange range in _ranges)
            {
                long? result = range[input.val];
                if (result is not null)
                    return (ResultType, result.Value);
            }
            return (ResultType, input.val);
        }
    }
    public override string ToString() => Name;
}
public readonly struct Range(long start, long length)
{
    public readonly long Start = start;
    public readonly long End = start + length;
    public bool Contains(long l) => l >= Start && l <= End;
}
public class MapRange
{
    public Range Source { get; private set; }
    public Range Destination { get; private set; }
    public long Diff => Source.Start - Destination.Start;
    public MapRange(string line)
    {
        List<long> values = line.ToMany<long>().ToList();
        (long sourceStart, long destStart, long length) = (values[1], values[0], values[2]);
        Source = new(sourceStart, length);
        Destination = new(destStart, length);
    }
    public long? this[long l]
        => Source.Contains(l) ? l + Diff : null;
}