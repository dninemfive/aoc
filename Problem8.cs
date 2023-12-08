using System;
using System.Collections.Generic;
using System.Linq;
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
    {
        _tape.Reset();
        string cur = start;
        int ct = 0;
        while (cur != end)
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
        List<string> curs = _nodes.Keys.Where(x => x.EndsWith('A')).ToList();
        Console.WriteLine(curs.ListNotation());
        int ct = 0;
        while(curs.Any(x => !x.EndsWith('Z')))
        {
            for (int i = 0; i < curs.Count; i++)
                curs[i] = Step(curs[i]);
            _ = _tape.Advance();
            Console.WriteLine($"{ct,6} {curs.Select(x => x.EndsWith('Z') ? "Z" : " ").Merge()}");
            ct++;
        }
        return ct;
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