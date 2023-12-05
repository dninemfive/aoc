using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public class Problem5
{
    [SolutionToProblem(5)]
    public IEnumerable<object> Solve(int[] lines)
    {
        
        yield break;
    }    
    // possible paths:
    // seed -> soil -> fertilizer -> water -> light -> temperature -> humidity -> location
    // ...oh. that's the only one.
    public int LocationFor(int seed, Dictionary<string, XToYMap> mapMap)
    {
        (string type, int val) cur = ("seed", seed);
        while(curType != "location")
        {
            XToYMap map = mapMap[cur.type];
            cur = (map.To, map[cur.val]);
        }
    }
}
public class XToYMap(string title, IEnumerable<string> nonTitleLines)
{
    public string From => title.Split("-")[0];
    public string To => title.Split("-")[2];
    public string Name => $"{From}-to-{To} map";
    private readonly List<MapRange> _ranges = [ .. nonTitleLines.Select(x => new MapRange(x))
                                                                .OrderBy(x => x.Source.Start) ];
    public int this[string sourceType, string destType, int n]
    {
        get
        {
            if (sourceType != From || destType != To)
                throw new ArgumentException($"A {Name} can't convert from {sourceType} to {destType}!");
            foreach (MapRange range in _ranges)
            {
                int? result = range[n];
                if (result is not null)
                    return result.Value;
            }
            return n;
        }
    }
}
public readonly struct Range(int start, int length)
{
    public readonly int Start = start;
    public readonly int End = start + length;
    public bool Contains(int n) => n >= Start && n <= End;
}
public class MapRange
{
    public Range Source { get; private set; }
    public Range Destination { get; private set; }
    public int Diff => Source.Start - Destination.Start;
    public int Length { get; private set; }
    public MapRange(string line)
    {
        List<int> values = line.ToMany<int>().ToList();
        (int sourceStart, int destStart, int length) = (values[1], values[0], values[2]);
        Source = new(sourceStart, length);
        Destination = new(destStart, length);
    }
    public int? this[int n]
        => Source.Contains(n) ? n + Diff : null;
}