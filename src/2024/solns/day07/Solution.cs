using System.Numerics;
using Expression    = d9.aoc._24.day07.Expression<long>;
using Operator      = d9.aoc._24.day07.Operator<long>;
using Operators     = d9.aoc._24.day07.Operators<long>;

namespace d9.aoc._24.day07;
[SolutionToProblem(7)]
[SampleResults(3749L)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        List<Operator> operations = [Operators.Add, Operators.Multiply];
        List<(IEnumerable<Expression> expr, long expected)> equations
            = lines.Parse<long>()
                   .Select(x => (x.numbers.PossibleExpressions(operations), x.expected))
                   .ToList();
        foreach((IEnumerable<Expression> expressions, long expectedResult) in equations)
        {
            Console.WriteLine("=======");
            Console.WriteLine($"Expected result: {expectedResult}");
            foreach(Expression expression in expressions)
            {
                Console.WriteLine($"{expression} = {expression.Evaluate()}");
            }
        }
        yield return lines.Parse<long>()
                          .Where(x => x.numbers.PossibleExpressions(operations)
                                               .Any(y => y == x.expected))
                          .Select(x => x.expected)
                          .Sum();
    }
}