using System.Numerics;

namespace d9.aoc._24.day07;
[SolutionToProblem(7)]
[SampleResults(3749)]
internal class Solution : AocSolution
{
    public static T Add<T>(T l, T r)
        where T : INumber<T>
        => l + r;
    public static T Multiply<T>(T l, T r)
        where T : INumber<T>
        => l + r;
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        List<Operation<int>> operations = [Add, Multiply];
        yield return lines.Parse()
                          .Where(x => x.numbers.PossibleSolutions(operations)
                                               .Any(y => y == x.expected))
                          .Select(x => x.expected)
                          .Sum();
    }
}
public delegate T Operation<T>(T l, T r)
    where T : INumber<T>;
internal static class Extensions
{
    internal static IEnumerable<(IEnumerable<int> numbers, int expected)> Parse(this string[] lines)
    {
        foreach (string line in lines)
        {
            string[] split = line.Split(": ");
            yield return (split[1].ToMany<int>(), int.Parse(split[0]));
        }
    }
    internal static IEnumerable<T> PossibleSolutions<T>(this IEnumerable<T> operands, IEnumerable<Operation<T>> operations)
        where T : INumber<T>
    {
        foreach (IEnumerable<Operation<T>> combination in AllCombinationsOf(operations, operands.Count() - 1))
        {
            T result = operands.First();
            for (int i = 0; i < combination.Count(); i++)
            {
                result = combination.ElementAt(i)(result, operands.ElementAt(i + 1));
            }
            yield return result;
        }
    }
    internal static IEnumerable<IEnumerable<T>> AllCombinationsOf<T>(this IEnumerable<T> options, int count)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 1, nameof(count));
        if (count == 1)
        {
            yield return options;
            yield break;
        }
        foreach (T option in options)
        {
            foreach (IEnumerable<T> remainingOptions in AllCombinationsOf(options, count - 1))
            {
                yield return [option, .. remainingOptions];
            }
        }
    }
}