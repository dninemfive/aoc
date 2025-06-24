using Operators = d9.aoc._24.day07.Operators<long>;
using ParsedLines = System.Collections.Generic.IEnumerable<(System.Collections.Generic.IEnumerable<long> numbers, long expected)>;

namespace d9.aoc._24.day07;
[SolutionToProblem(7)]
internal class Solution(params string[] lines) : AocSolution
{
    public readonly ParsedLines ParsedLines = lines.Parse<long>();

    [ExpectedResults(sample: 3749L, final: 2664460013123L)]
    public override AocPartialResult? Part1()
        => ParsedLines.ValidSolutions(Operators.Add, Operators.Multiply).Sum();
    public override AocPartialResult Part2()
        => ParsedLines.ValidSolutions(Operators.Add, Operators.Multiply, Operators.Concatenate).Sum();
}