using d9.aoc.core;
using d9.utl;
using System.Numerics;
using MathUtils = d9.aoc.core.MathUtils;

namespace d9.aoc._23.day06;
[SolutionToProblem(6)]
public class Solution(string[] lines) 
    : AocSolution
{
    public readonly IEnumerable<(int time, int distance)> Races = lines.First()
                                                                       .ToMany<int>(skip: 1)
                                                                       .Zip(lines.Second()
                                                                                 .ToMany<int>(skip: 1));

    [ExpectedResults(500346)]
    public override AocPartResultValue? Part1()
        => (int)Races.Select(x => NumSolutions<double>(x.time, x.distance))
                     .Aggregate((x, y) => x * y);

    [ExpectedResults(42515755L)]
    public override AocPartResultValue? Part2()
    {
        long correctTime = long.Parse(lines.First().SplitAndTrim(" ")[1..].Join());
        long correctDistance = long.Parse(lines.Second().SplitAndTrim(" ")[1..].Join());
        return (long)NumSolutions<double>(correctTime, correctDistance);
    }

    public static T NumSolutions<T>(T totalTime, T targetDistance)
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        (T lo, T hi) = MathUtils.QuadraticFormula(T.One, totalTime, targetDistance);
        return T.Floor(-lo) - T.Ceiling(-hi) + T.One;
    }
}
