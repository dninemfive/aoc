using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem6
{
    [SolutionToProblem(6)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<(int time, int distance)> races = lines.First()
                                                           .ToMany<int>(skip: 1)
                                                           .Zip(lines[1].ToMany<int>(skip: 1));
        yield return races.Select(x => NumSolutions(x.time, x.distance)).Aggregate((x, y) => x * y);
    }
    public static int Distance(int buttonHeldTime, int totalTime)
        => buttonHeldTime * (totalTime - buttonHeldTime);
    public static (int left, int right) Intersections(int totalTime, int targetDistance)
    {
        void stepWhile(ref int result, int step, Func<int, bool> predicate)
        {
            while(predicate(result))
                result += step;
        }
        int left = 0, right = totalTime;
        stepWhile(ref left, 1, x => Distance(x, totalTime) <= targetDistance);
        stepWhile(ref right, -1, x => Distance(x, totalTime) <= targetDistance);
        return (left, right);
    }
    public static int NumSolutions(int totalTime, int targetDistance)
    {
        (int left, int right) = Intersections(totalTime, targetDistance);
        return right - left;
    }
}
