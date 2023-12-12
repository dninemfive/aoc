using System.Numerics;
namespace d9.aoc._23.day6;
public static class Solution
{
    [SolutionToProblem(6)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<(int time, int distance)> races = lines.First()
                                                           .ToMany<int>(skip: 1)
                                                           .Zip(lines.Second()
                                                                     .ToMany<int>(skip: 1));
        yield return (int)races.Select(x => NumSolutions<double>(x.time, x.distance)).Aggregate((x, y) => x * y);
        long correctTime = long.Parse(lines.First()
                                           .SplitAndTrim(" ")[1..]
                                           .Merge());
        long correctDistance = long.Parse(lines.Second()
                                               .SplitAndTrim(" ")[1..]
                                               .Merge());
        yield return (long)NumSolutions<double>(correctTime, correctDistance);
    }
    public static T NumSolutions<T>(T totalTime, T targetDistance)
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        (T lo, T hi) = Utils.QuadraticFormula(T.One, totalTime, targetDistance);
        return T.Floor(-lo) - T.Ceiling(-hi) + T.One;
    }
}
