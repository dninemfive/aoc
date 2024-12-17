
using System.Text.RegularExpressions;

namespace d9.aoc._24.day13;
[SolutionToProblem(13)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        yield return 0;
        Button.FromLine("Button A: X+94, Y+34");
    }
}
internal partial record Button(int XOffset, int YOffset, string Name)
{
    public static readonly Regex ButtonRegex = GenerateButtonRegex();
    public int Value => Name switch
    {
        "A" => 3,
        "B" => 1,
        _ => throw new Exception($"{Name} is not a valid button name!")
    };
    public static implicit operator Button((int a, int b, string c) tuple)
        => new(tuple.a, tuple.b, tuple.c);
    public static Button? FromLine(string line)
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
        return new(int.Parse(groups[1]), int.Parse(groups[2]), groups[0]);
    }
    [GeneratedRegex(@"Button (.): X\+(\d+), Y\+(\d+)")]
    private static partial Regex GenerateButtonRegex();
}
internal partial record Prize(int X, int Y)
{
    public static readonly Regex PrizeRegex = GeneratePrizeRegex();
    public static Prize? FromLine(string line)
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
        return new(int.Parse(groups.First()), int.Parse(groups.Second()));
    }
    public static implicit operator Point<int>(Prize prize)
        => (prize.X, prize.Y);
    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex GeneratePrizeRegex();
}
internal partial record ClawMachine(Button ButtonA, Button ButtonB, Point<int> Prize)
{
    public static ClawMachine FromLines(IEnumerable<string> lines)
    {
        Dictionary<string, Button?> buttons = lines.Select(Button.FromLine)
                                                   .Where(x => x is not null)
                                                   .ToDictionary(x => x!.Name);
        Prize prize = lines.Select(day13.Prize.FromLine)
                                              .Where(x => x is not null)
                                              .First()!;
        return new(buttons["A"]!, buttons["B"]!, prize);
    }
    public IEnumerable<(int a, int b)> PotentialButtonPressCombos()
    {
        // find solutions to aA + bB = P
        throw new NotImplementedException();
    }
    public int ComboCost((int a, int b) combo)
        => ButtonA.Value * combo.a + ButtonB.Value * combo.b;
    public (int a, int b) MinCostCombo()
        => PotentialButtonPressCombos().MinBy(ComboCost);
    public int MinComboCost
        => ComboCost(MinCostCombo());
}