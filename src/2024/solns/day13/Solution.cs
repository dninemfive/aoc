
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.RegularExpressions;

namespace d9.aoc._24.day13;
[SolutionToProblem(13)]
[SampleResults(480L)]
[FinalResults(40069L)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<ClawMachine<long>> clawMachines1 = lines.Chunk(4)
                                                   //.Select(x => x.Where(y => !y.IsNullOrEmpty()))
                                                     .Select(ClawMachine<long>.FromLines);
        yield return clawMachines1.Select(x => x.MinComboCost()).Sum();
        Point<long> offset = (10000000000000L, 10000000000000L);
        IEnumerable<ClawMachine<long>> clawMachines2 = clawMachines1.Select(x => new ClawMachine<long>(x.ButtonA, x.ButtonB, x.Prize + offset));
        yield return clawMachines2.Select(x => x.MinComboCost()).Sum();
    }
}
internal partial record Button<T>(T XOffset, T YOffset, string Name)
    where T : INumber<T>
{
    public static readonly Regex ButtonRegex = GenerateButtonRegex();
    public T Value => Name switch
    {
        "A" => T.CreateChecked(3),
        "B" => T.One,
        _ => throw new Exception($"{Name} is not a valid button name!")
    };
    public Point<T> Offset => (XOffset, YOffset);
    public static implicit operator Button<T>((T a, T b, string c) tuple)
        => new(tuple.a, tuple.b, tuple.c);
    public static Button<T>? FromLine(string line)
    {
        MatchCollection matches = ButtonRegex.Matches(line);
        if (!matches.Any())
            return null;
        List<string> groups = matches.First()
                                     .Groups
                                     .Values
                                     .Skip(1)
                                     .Select(x => x.Value)
                                     .ToList();
        return new(T.Parse(groups[1], null), T.Parse(groups[2], null), groups[0]);
    }
    public static implicit operator Point<T>(Button<T> button)
        => button.Offset;
    [GeneratedRegex(@"Button (.): X\+(\d+), Y\+(\d+)")]
    private static partial Regex GenerateButtonRegex();
}
internal partial record Prize<T>(T X, T Y)
    where T : INumber<T>
{
    public static readonly Regex PrizeRegex = GeneratePrizeRegex();
    public static Prize<T>? FromLine(string line)
    {
        MatchCollection matches = PrizeRegex.Matches(line);
        if (!matches.Any())
            return null;
        List<string> groups = matches.First()
                                     .Groups
                                     .Values
                                     .Skip(1)
                                     .Select(x => x.Value)
                                     .ToList();
        return new(T.Parse(groups.First(), null), T.Parse(groups.Second(), null));
    }
    public static implicit operator Point<T>(Prize<T> prize)
        => (prize.X, prize.Y);
    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex GeneratePrizeRegex();
}
internal partial record ClawMachine<T>(Button<T> ButtonA, Button<T> ButtonB, Point<T> Prize)
    where T : INumber<T>
{
    public static ClawMachine<T> FromLines(IEnumerable<string> lines)
    {
        Dictionary<string, Button<T>?> buttons = lines.Select(Button<T>.FromLine)
                                                   .Where(x => x is not null)
                                                   .ToDictionary(x => x!.Name);
        Prize<T> prize = lines.Select(Prize<T>.FromLine)
                                              .Where(x => x is not null)
                                              .First()!;
        return new(buttons["A"]!, buttons["B"]!, prize);
    }
    public IEnumerable<(T a, T b)> PotentialButtonPressCombos()
    {
        T aMax = ButtonA.Offset.StepsToReachOrPass(Prize);
        for(T a = T.Zero; a <= aMax; a++)
        {
            Point<T> start = a * ButtonA.Offset;
            if (ButtonB.Offset.CanReach(start, Prize, out T b))
                yield return (a, b);
        }
    }
    public T ComboCost((T a, T b) combo)
        => ButtonA.Value * combo.a + ButtonB.Value * combo.b;
    public (T a, T b)? MinCostCombo()
    {
        IEnumerable<(T a, T b)> potentialCombos = PotentialButtonPressCombos();
        // Console.WriteLine(potentialCombos.ListNotation());
        return potentialCombos.Any() ? potentialCombos.MinBy(ComboCost) : null;
    }
    public T MinComboCost()
        => MinCostCombo() is (T, T) t ? ComboCost(t) : T.Zero;
}
internal static class Extensions
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
        bool reached = step.X.CanReach(distance.X, out T xSteps)
                     & step.Y.CanReach(distance.Y, out T ySteps); // & instead of && to make sure both are calculated
        result = T.Max(xSteps, ySteps);
        return reached;
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
        return result;
    }
    public static T StepsToReachOrPass<T>(this Button<T> button, Point<T> distance)
        where T : INumber<T>
        => button.Offset.StepsToReachOrPass(distance);
}