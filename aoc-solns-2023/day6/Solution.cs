using System.Numerics;
using d9.aoc._23.shared;
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
        yield return races.Select(x => NumSolutions(x.time, x.distance)).Aggregate((x, y) => x * y);
        long correctTime = long.Parse(lines.First()
                                           .SplitAndTrim(" ")[1..]
                                           .Merge());
        long correctDistance = long.Parse(lines.Second()
                                               .SplitAndTrim(" ")[1..]
                                               .Merge());
        yield return NumSolutions(correctTime, correctDistance);
    }
    public static T Distance<T>(T buttonHeldTime, T totalTime)
        where T : INumber<T>
        => buttonHeldTime * (totalTime - buttonHeldTime);
    public static (T left, T right) Intersections<T>(T totalTime, T targetDistance)
        where T : INumber<T>
    {
        static void stepWhile(ref T result, bool subtract, Func<T, bool> predicate)
        {
            while (predicate(result))
                result = subtract ? result - T.One : result + T.One;
        }
        T left = T.Zero, right = totalTime;
        stepWhile(ref left, subtract: false, x => Distance(x, totalTime) <= targetDistance);
        stepWhile(ref right, subtract: true, x => Distance(x, totalTime) <= targetDistance);
        return (left, right);
    }
    public static T NumSolutions<T>(T totalTime, T targetDistance)
        where T : INumber<T>
    {
        (T left, T right) = Intersections(totalTime, targetDistance);
        return right - left + T.One;
    }
}
