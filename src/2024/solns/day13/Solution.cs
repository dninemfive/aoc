
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
        // find solutions to aA + bB = P
        IEnumerable<T> coefficients = [
            Prize.X / ButtonA.XOffset,
            Prize.X / ButtonB.XOffset,
            Prize.Y / ButtonA.YOffset,
            Prize.Y / ButtonB.YOffset
        ];
        // Console.WriteLine($"{coefficients.ListNotation()} {coefficients.Max().PrintNull()}");
        T min = T.Zero,
          aMax = coefficients.Take(2).Max()!,
          bMax = coefficients.Skip(2).Max()!;
        // b min = a max - b max ? 
        for(T a = min; a <= aMax; a++)
            for(T b = min; b <= bMax; b++)
            {
                //Console.WriteLine($"{a,3}\t{b,3}\t{ButtonA.Offset}\t{ButtonA.Offset * a}\t{ButtonB.Offset}\t{ButtonB.Offset * b}\t{ButtonA.Offset * a + ButtonB.Offset * b}\t{Prize}");
                if (ButtonA.Offset * a + ButtonB.Offset * b == Prize)
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