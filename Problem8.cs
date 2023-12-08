using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem8
{
    private static readonly Dictionary<string, (string left, string right)> _nodes = new();
    [SolutionToProblem(8)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        Tape tape = new(lines.First());        
        foreach(string s in lines[2..])
        {
            List<string> vals = s.SplitAndTrim(" = (", ", ", ")");
            _nodes[vals.First()] = (vals[1], vals[2]);
        }
        yield return NavigateBetween("AAA", "ZZZ", tape);
    }
    public static int NavigateBetween(string start, string end, Tape tape)
    {
        string cur = start;
        tape.Index = -1;
        int ct = 0;
        while(cur != end)
        {
            (string left, string right) = _nodes[cur];
            cur = tape.Advance() == 'L' ? left : right;
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
}