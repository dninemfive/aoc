using System.Numerics;
using Z = long;

namespace d9.aoc._24.day17;
[SolutionToProblem(17)]
[SampleResults("4,6,3,5,6,3,5,2,1,0", 117440)]
[FinalResults("2,1,3,0,5,2,3,7,1")]
internal class Solution
    : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        (ProgramState<Z> initialState, IEnumerable<Z> instructions) = Parse<Z>(lines);
        yield return new(new Program<Z>(initialState, instructions).RunToCompletion().OutputString());
        // binary search for a number where the program has the right number of outputs
        // once found, find the upper and lower bounds of that region and iterate between them
        Z avg(Z a, Z b) => (a + b) / 2;
        Z min = 0, max = Z.MaxValue, pivot = avg(min, max);
        yield return new Program<Z>(initialState.CopyWith(a: min),   instructions).RunToCompletion().Count();
        yield return new Program<Z>(initialState.CopyWith(a: pivot), instructions).RunToCompletion().Count();
        yield return new Program<Z>(initialState.CopyWith(a: max),   instructions).RunToCompletion().Count();
    }
    public static (ProgramState<T> initialState, IEnumerable<T> instructions) Parse<T>(string[] lines)
        where T : struct, INumber<T>
    {
        T a = T.Parse(lines[0].Split(": ")[1], null);
        T b = T.Parse(lines[1].Split(": ")[1], null);
        T c = T.Parse(lines[2].Split(": ")[1], null);
        IEnumerable<T> instructions = lines[4].Split(": ")[1].Split(",").Select(x => T.Parse(x, null));
        return (new(new(a, b, c), 0), instructions);
    }
}
