using System.Numerics;

namespace d9.aoc._24.day13; internal static class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="step">The length of a step.</param>
    /// <param name="distance">How far you're trying to travel.</param>
    /// <param name="result">The number of steps required to reach <em>or travel further than</em> the specified <paramref name="distance"/>.</param>
    /// <returns><see langword="true"/> if you can reach exactly <paramref name="distance"/> units, or <see langword="false"/> otherwise.</returns>
    public static bool CanReach<T>(this T step, T distance, out T result)
        where T : INumber<T>
    {
        result = distance / step;
        bool reached = result * step == distance;
        if (!reached)
            result += T.One;
        return reached;
    }
    public static bool CanReach<T>(this T step, T start, T end, out T result)
        where T : INumber<T>
        => step.CanReach(T.Abs(end - start), out result);
    public static bool CanReach<T>(this Point<T> step, Point<T> distance, out T result)
        where T : INumber<T>
    {
        bool reachedX = step.X.CanReach(distance.X, out T xSteps),
             reachedY = step.Y.CanReach(distance.Y, out T ySteps);
        result = T.Min(xSteps, ySteps);
        return reachedX && reachedY && xSteps == ySteps;
    }
    public static bool CanReach<T>(this Point<T> step, Point<T> start, Point<T> end, out T result)
        where T : INumber<T>
        => step.CanReach((end - start).Abs, out result);
    public static bool CanReach<T>(this Button<T> button, Point<T> start, Point<T> end, out T result)
        where T : INumber<T>
        => button.Offset.CanReach(start, end, out result);
    public static T StepsToReachOrPass<T>(this Point<T> step, Point<T> distance)
        where T : INumber<T>
    {
        _ = step.CanReach(distance, out T result);
        Console.WriteLine($"{step,32} {result,16} {distance - (step * result),32}");
        return result;
    }
    public static T StepsToReachOrPass<T>(this Button<T> button, Point<T> distance)
        where T : INumber<T>
        => button.Offset.StepsToReachOrPass(distance);
}