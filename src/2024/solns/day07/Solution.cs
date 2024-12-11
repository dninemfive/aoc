using Operators = d9.aoc._24.day07.Operators<long>;
using ParsedLines = System.Collections.Generic.IEnumerable<(System.Collections.Generic.IEnumerable<long> numbers, long expected)>;

namespace d9.aoc._24.day07;
[SolutionToProblem(7, complete: true)]
[DisableTests]
[SampleResults(        3749L,
                      11387L)]
[FinalResults(2664460013123L,
            426214131924213L)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        ParsedLines parsedLines = lines.Parse<long>();
        yield return "preinit";
        yield return parsedLines.ValidSolutions(Operators.Add,
                                                Operators.Multiply).Sum();
        yield return parsedLines.ValidSolutions(Operators.Add,
                                                Operators.Multiply,
                                                Operators.Concatenate).Sum();

    }
}