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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value... i know it will have a non-null value when needed
    private static Tape _tape;
#pragma warning restore CS8618
    [SolutionToProblem(8)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        _tape = new(lines.First());
        foreach(string s in lines[2..])
        {
            List<string> vals = s.SplitAndTrim(" = (", ", ", ")");
            _nodes[vals.First()] = (vals[1], vals[2]);
        }
        yield return NavigateBetween("AAA", "ZZZ");
        yield return GhostNavigate();
    }
    public static int NavigateBetween(string start, string end)
        => start.NavigateUntil(x => x == end);
    public static int NavigateUntil(this string start, Func<string, bool> end) {
        _tape.Reset();
        string cur = start;
        int ct = 0;
        while (!end(cur))
        {
            cur = Step(cur, advance: true);
            ct++;
        }
        return ct;
    }
    public static string Step(string cur, bool advance = false)
    {
        (string left, string right) = _nodes[cur];
        char c = advance ? _tape.Advance() : _tape.Current;
        return c == 'L' ? left : right;
    }
    public static int GhostNavigate()
    {
        _tape.Reset();
        List<string> starts = _nodes.Keys.Where(x => x.EndsWith('A')).ToList();
        int ct = 0;
        while(starts.Any(x => !x.EndsWith('Z')))
        {
            for (int i = 0; i < starts.Count; i++)
                starts[i] = Step(starts[i]);
            _ = _tape.Advance();
            Console.WriteLine($"{ct,6} {starts.Select(x => x.EndsWith('Z') ? "Z" : " ").Merge()}");
            ct++;
        }
        return ct;
    }
    public static IEnumerable<int> Factors(this int n)
    {
        // making sure to perform each calculation once in a thickheaded way to improve performance
        int halfN = n / 2;
        for(float f = 0; f < halfN; f++)
        {
            float factor = n / f;
            int intFactor = (int)factor;
            if (factor == intFactor)
                yield return intFactor;
        }
    }
    public static int LeastCommonFactor(params int[] ints)
    {
        // todo: we can eliminate low factors once any item doesn't include them,
        // so add an argument "min" to factors
        IEnumerable<IEnumerable<int>> factorLists = ints.Select(x => x.Factors());
        foreach (int factor in factorLists.SelectMany(x => x).Order())
            if (factorLists.All(x => x.Contains(factor)))
                return factor;
        return ints.Distinct().Aggregate((x, y) => x * y);
    }
}
public class Tape(string s)
{
    public readonly string Items = s;
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