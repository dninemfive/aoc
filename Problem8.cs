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
        yield return GhostNavigate();
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
        (string left, string right) = _nodes[cur];
        char c = tape.Advance();
        return c == 'L' ? left : right;
    }
    public static int GhostNavigate()
    {
        // todo: find where the sequence repeats (i.e. encounters a (node, direction, index) threeple twice)
        // but recently the sequence has repeated and repeated and it leaves me with the theory that they're trying to get inside
        _tape.Reset();
        List<string> starts = _nodes.Keys.Where(x => x.EndsWith('A')).ToList();
        return starts.Select(x => x.NavigateUntil(x => x.EndsWith('Z'))).LeastCommonFactor();
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
    public static int LeastCommonFactor(this IEnumerable<int> ints)
    {
        // todo: we can eliminate low factors once any item doesn't include them,
        // so add an argument "min" to factors
        IEnumerable<IEnumerable<int>> factorLists = ints.Select(x => x.Factors());
        foreach (int factor in factorLists.SelectMany(x => x).Order())
            if (factorLists.All(x => x.Contains(factor)))
                return factor;
        return ints.Distinct().Aggregate((x, y) => x * y);
    }
    public static IEnumerable<int> ZIndexes(string cur, string input)
    {
        Tape tape = new(input);
        HashSet<(string cur, int index)> visitedStates = new();
        while(!visitedStates.Contains((cur, tape.Index)))
        {
            if (cur.EndsWith('Z'))
                yield return tape.Index;
            cur = tape.Step(cur);
        }
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