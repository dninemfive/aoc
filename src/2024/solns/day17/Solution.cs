
namespace d9.aoc._24.day17;
[SolutionToProblem(17)]
[SampleResults("4,6,3,5,6,3,5,2,1,0", 117440)]
[FinalResults("2,1,3,0,5,2,3,7,1")]
internal class Solution
    : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Program<int> program = Program<int>.FromLines(lines);
        ProgramState<int> initialState = program.State;
        program.RunToCompletion();
        yield return new(program.OutputString);
    }
}
