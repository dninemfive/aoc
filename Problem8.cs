using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem8
{
    private static readonly Dictionary<string, (string left, string right)> _nodes = new();
    [SolutionToProblem(8)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        string tape = lines.First();
        foreach(string s in lines[2..])
        {
            List<string> vals = s.SplitAndTrim(" = (", ", ", ")");
            _nodes[vals.First()] = (vals[1], vals[2]);
        }
        yield return NavigateBetween("AAA", "ZZZ", tape).Count();
        yield return GhostPathLength(tape);
    }
    public static IEnumerable<(string position, long ct)> NavigateBetween(string start, string end, string tape)
        => start.NavigateUntil(x => x.position == end, tape);
    public static IEnumerable<(string position, long ct)> NavigateUntil(this string start, Func<(string position, long ct), bool> end, string input) {
        Tape tape = new(input);
        string cur = start;
        long ct = 0;
        while (!end((cur, ct)))
        {
            cur = tape.Step(cur);
            yield return (cur, ++ct);
        }
    }
    public static string Step(this Tape tape, string cur)
    {
        (string left, string right) = _nodes[cur];
        char c = tape.Advance();
        return c == 'L' ? left : right;
    }
    public static long GhostPathLength(string tape)
    {
        IEnumerable<string> starts = _nodes.Keys.Where(x => x.EndsWith('A'));
        List<long> allZs = new();
        foreach(string start in starts)
        {
            IEnumerable<long> zPositions = tape.ZPositions(start).ToList();
            allZs.AddRange(zPositions);
        }
        return allZs.LeastCommonMultiple();
    }
    public static IEnumerable<long> ZPositions(this string input, string cur)
    {
        Tape tape = new(input);
        HashSet<(string cur, long index)> visitedStates = new();
        long ct = 0;
        // once we see a (position, ct) pair twice, the sequence will repeat, so we don't need to continue
        // (it turns out for my input there's always exactly one point where the item ends with Z,
        //  but this code is more general and pretty cool i think)
        while (!visitedStates.Contains((cur, tape.Index)))
        {
            if (cur.EndsWith('Z'))
                yield return ct;
            visitedStates.Add((cur, tape.Index));
            cur = tape.Step(cur);
            ct++;
        }
    }
}
public class Tape(string s)
{
    public readonly string Items = s.All(x => x is 'L' or 'R') ? s 
        : throw new ArgumentException("String for a Tape must consist only of characters L and R!");
    private int _index = 0;
    public int Index
    {
        get => _index;
        set
        {
            _index = (value < 0, value >= Items.Length) switch
            {
                (true, _) => Items.Length - 1,
                (_, true) => 0,
                _ => value
            };
        }
    }
    public char Advance()
        => Items[Index++];
    public char Current
        => Items[Index];
    public void Reset() 
        => Index = 0;
}