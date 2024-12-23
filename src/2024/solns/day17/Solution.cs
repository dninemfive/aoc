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
        Program<Z> program = Program<Z>.FromLines(lines);
        ProgramState<Z> initialState = program.State;
        program.RunToCompletion();
        yield return new(program.OutputString);
        // binary search for a number where the program has the right number of outputs
        // once found, find the upper and lower bounds of that region and iterate between them
        Z avg(Z a, Z b) => (a + b) / 2;
        Z min = 0, max = Z.MaxValue, pivot = avg(min, max);
        Z minCt = 0, maxCt = Z.MaxValue;
        yield return new Program<Z>(initialState.CopyWith(a: min), program.Instructions).RunToCompletion().Count();
        yield return new Program<Z>(initialState.CopyWith(a: pivot), program.Instructions).RunToCompletion().Count();
        yield return new Program<Z>(initialState.CopyWith(a: max), program.Instructions).RunToCompletion().Count();
    }
}
