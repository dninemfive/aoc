using d9.aoc.core;

namespace d9.aoc._23.day08;
[SolutionToProblem(8)]
public class Solution(string[] lines) : AocSolution
{
    public readonly string Tape = lines.First();
    public readonly IReadOnlyDictionary<string, (string left, string right)> Nodes 
        = lines.Skip(2)
               .Select(x => x.SplitAndTrim(" = (", ", ", ")"))
               .ToDict(keys: x => x[0], values: x => (x[1], x[2]));

    [ExpectedResults(12083)]
    public override AocPartResultValue? Part1()
        => NavigateBetween("AAA", "ZZZ", Tape).Count();

    [ExpectedResults(13385272668829)]
    public override AocPartResultValue? Part2()
        => GhostPathLength(Tape);

    public IEnumerable<(string position, long ct)> NavigateBetween(string start, string end, string tape)
        => NavigateUntil(start, x => x.position == end, tape);

    public IEnumerable<(string position, long ct)> NavigateUntil(string start, Func<(string position, long ct), bool> end, string input)
    {
        Tape tape = new(input);
        string cur = start;
        long ct = 0;
        while (!end((cur, ct)))
            yield return (cur = Step(tape, cur), ++ct);
    }

    public string Step(Tape tape, string cur)
    {
        (string left, string right) = Nodes[cur];
        char c = tape.Advance();
        return c == 'L' ? left : right;
    }

    public long GhostPathLength(string tape)
        => Nodes.Keys.Where(x => x.EndsWith('A'))
                     .SelectMany(x => ZPositions(tape, x))
                     .LeastCommonMultiple();

    public IEnumerable<long> ZPositions(string input, string cur)
    {
        Tape tape = new(input);
        HashSet<(string cur, long index)> visitedStates = new();
        long ct = 0;
        // once we see a (position, ct) pair twice, the sequence will repeat, so we don't need to continue
        // (it turns out for my input there's always exactly one NumberPair<int> where the item ends with Z,
        //  but this code is more general and pretty cool i think)
        while (!visitedStates.Contains((cur, tape.Index)))
        {
            if (cur.EndsWith('Z'))
                yield return ct;
            visitedStates.Add((cur, tape.Index));
            cur = Step(tape, cur);
            ct++;
        }
    }
}