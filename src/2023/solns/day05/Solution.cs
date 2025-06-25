using d9.aoc.core;
using d9.utl;

namespace d9.aoc._23.day05;
// disable warnings for unfinished parts lol
#pragma warning disable CS0162 // unreachable code
#pragma warning disable CS8321 // unused function
[SolutionToProblem(5)]
public class Solution(string[] lines)
    : AocSolution
{
    public IEnumerable<long> Seeds = lines.First().Split(": ")[1].ToMany<long>();
    public readonly Dictionary<string, XToYMap<long>> MapMap = ParseLines(lines[2..]);

    [ExpectedResults(84470622L)]
    public override AocPartResultValue? Part1()
        => Seeds.Select(LocationFor).Min();

    public override AocPartResultValue? Part2()
    {
        Range<long>[] pairs = new Range<long>[Seeds.Count() / 2];
        for (int i = 0; i < pairs.Length; i++)
            pairs[i] = new(Seeds.ElementAt(i * 2), Seeds.ElementAt(i * 2 + 1));
        Console.WriteLine($"Seeds:       {Seeds.Order().Select(x => $"{x,10}").ListNotation()}");
        return LowestLocationFor(pairs);
    }

    public static Dictionary<string, XToYMap<long>> ParseLines(string[] lines)
    {
        string title = "";
        List<string> nonTitleLines = new();
        Dictionary<string, XToYMap<long>> result = new();
        foreach (string line in lines.Append(""))
        {
            if (line.Contains("-to-"))
            {
                title = line;
            }
            else if (string.IsNullOrWhiteSpace(line))
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

    public long LocationFor(long seed)
    {
        // Console.Write($"LocationFor({seed})");
        (string type, long val) cur = ("seed", seed);
        while (cur.type != "location")
        {
            cur = MapMap[cur.type][cur];
            // Console.Write($" -> {cur.val,11}");
        }
        // Console.WriteLine();
        return cur.val;
    }

    public long LowestLocationFor(params Range<long>[] seedRanges)
    {
        // for each location range in order of smallest to largest,
        //      for each humidity range which maps to that location range,
        //          for each temperature range which maps to that humidity range,
        //              ...
        //                  for each seed range,
        //                      if the range maps to any of the soil ranges,
        //                          find the first seed which maps to that range
        // alternatively, make a sieve of seed ranges?
        // like we can eliminate any value which maps to a higher location than is encountered or whatever
        // in that case we'd iterate from largest to smallest tho
        // alternatively alternatively, we could make "traces" which track the unique paths through them
        // but i think this doesn't actually eliminate nearly as aggressively as necessary
        string[] keys = ["seed", "soil", "fertilizer", "water", "light", "temperature", "humidity"];
        IEnumerable<MapRange<long>> overlapping(string destKey, string sourceKey)
            => MapMap[sourceKey].Ranges.Where(x => MapMap[destKey].Ranges.Any(y => x.Destination.OverlapsWith(y.Source)));
        List<Tree<MapRange<long>>> trees = new();
        throw new NotImplementedException();
    }
}