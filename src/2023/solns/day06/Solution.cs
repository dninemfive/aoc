using d9.aoc.core;
using d9.utl;
using System.Numerics;
using MathUtils = d9.aoc.core.MathUtils;

namespace d9.aoc._23.day6;
[SolutionToProblem(6)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(string[] lines)
    {
        IEnumerable<(int time, int distance)> races = lines.First()
                                                           .ToMany<int>(skip: 1)
                                                           .Zip(lines.Second()
                                                                     .ToMany<int>(skip: 1));
        yield return (int)races.Select(x => NumSolutions<double>(x.time, x.distance)).Aggregate((x, y) => x * y);
        long correctTime = long.Parse(lines.First()
                                           .SplitAndTrim(" ")[1..]
                                           .Join());
        long correctDistance = long.Parse(lines.Second()
                                               .SplitAndTrim(" ")[1..]
                                               .Join());
        yield return (long)NumSolutions<double>(correctTime, correctDistance);
    }
    public static T NumSolutions<T>(T totalTime, T targetDistance)
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        (T lo, T hi) = MathUtils.QuadraticFormula(T.One, totalTime, targetDistance);
        return T.Floor(-lo) - T.Ceiling(-hi) + T.One;
    }
}
