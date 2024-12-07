using System.Globalization;
using System.Numerics;

namespace d9.aoc._24.day07; 
internal static class Extensions
{
    internal static IEnumerable<(IEnumerable<T> numbers, T expected)> Parse<T>(this string[] lines, IFormatProvider? provider = null)
        where T : INumber<T>, IParsable<T>
    {
        provider ??= CultureInfo.InvariantCulture;
        foreach (string line in lines)
        {
            string[] split = line.Split(": ");
            yield return (split[1].ToMany<T>(), T.Parse(split[0], provider));
        }
    }
    internal static IEnumerable<Expression<T>> PossibleExpressions<T>(this IEnumerable<T> operands, IEnumerable<Operator<T>> operatorOptions)
        where T : INumber<T>
    {
        foreach (IEnumerable<Operator<T>> combination in AllCombinationsOf(operatorOptions, operands.Count() - 1))
        {
            yield return new(operands, combination);
        }
    }
    internal static IEnumerable<IEnumerable<T>> AllCombinationsOf<T>(this IEnumerable<T> options, int count)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 1, nameof(count));
        if (count == 1)
        {
            foreach(T option in options)
            {
                yield return [option];
            }
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
    internal static IEnumerable<T> ValidSolutions<T>(this IEnumerable<(IEnumerable<T> numbers, T expected)> parsedLines,
                                                        params Operator<T>[] operators)
        where T : INumber<T>
        => parsedLines.Where(x => x.numbers.PossibleExpressions(operators)
                                           .Any(y => y == x.expected))
                      .Select(x => x.expected);
}
