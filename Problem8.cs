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
        yield return NavigateBetween("AAA", "ZZZ", tape);
        yield return GhostNavigate(tape);
    }
    public static int NavigateBetween(string start, string end, string tape)
        => start.NavigateUntil(x => x == end, tape);
    public static int NavigateUntil(this string start, Func<string, bool> end, string input) {
        Tape tape = new(input);
        string cur = start;
        int ct = 0;
        while (!end(cur))
        {
            cur = tape.Step(cur);
            ct++;
        }
        return ct;
    }
    public static string Step(this Tape tape, string cur)
    {
        // Console.WriteLine($"{nameof(Step)}({cur})");
        (string left, string right) = _nodes[cur];
        char c = tape.Advance();
        return c == 'L' ? left : right;
    }
    public static int GhostNavigate(string tape)
    {
        IEnumerable<string> starts = _nodes.Keys.Where(x => x.EndsWith('A'));
        Console.WriteLine($"{nameof(GhostNavigate)}({starts.ListNotation()})");
        List<int> allZs = new();
        foreach(string start in starts)
        {
            IEnumerable<int> zPositions = tape.ZPositions(start).ToList();
            Console.WriteLine($"  {start}: {zPositions.ListNotation()}");
            allZs.AddRange(zPositions);
        }
        return allZs.LeastCommonMultiple();
    }
    public static bool DivisibleBy(this int a, int b)
    {
        float div = a / (float)b;
        return div == (int)div;
    }
    public static IEnumerable<int> ZPositions(this string input, string cur)
    {
        Console.WriteLine($"{nameof(ZPositions)}({cur})");
        Tape tape = new(input);
        HashSet<(string cur, int index)> visitedStates = new();
        int ct = 0;
        // continue until the sequence starts repeating and repeating and leaving you with the theory that they're trying to get inside
        while(!visitedStates.Contains((cur, tape.Index)))
        {
            if (cur.EndsWith('Z'))
            {
                Console.WriteLine($"\t{cur} {ct}");
                yield return ct;
            }
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