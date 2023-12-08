namespace d9.aoc._23;
public static class Problem2
{
    [SolutionToProblem(2)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<Game> games = lines.Select(x => new Game(x));
        yield return games.Where(x => x.PossibleWith(("red", 12), ("green", 13), ("blue", 14)))
                          .Sum(x => x.Id);
        yield return games.Select(x => x.MinimumRequiredColors)
                          .Sum(x => x.Power());
    }
    public class Game(string line)
    {
        public int Id = int.Parse(line.Split(": ")[0].Split(" ") [1]);
        public List<Handful> Handfuls = line.Split(": ")[1].Split("; ")
                                            .Select(x => new Handful(x))
                                            .ToList();
        public class Handful(string desc)
        {
            public Dictionary<string, int> Colors = desc.Split(", ")
                                                         .Select(x => x.Split(" "))
                                                         .ToDict(keys: x => x[1], values: x => int.Parse(x[0]));
            public int this[string key] => Colors.TryGetValue(key, out int result) ? result : 0;
        }
        public bool PossibleWith(params (string color, int quantity)[] colors)
            => !Handfuls.Any(handful => colors.Any(tuple => handful[tuple.color] > tuple.quantity));
        public IEnumerable<string> UniqueColors => Handfuls.SelectMany(x => x.Colors.Keys)
                                                           .Distinct();
        public int MinimumRequired(string color)
            => Handfuls.Select(x => x[color]).Max();
        public Dictionary<string, int> MinimumRequiredColors
            => UniqueColors.ToDictWithValue(MinimumRequired);
    }
    public static int Power(this Dictionary<string, int> cubeSet)
        => cubeSet.Values.Aggregate((x, y) => x * y);
}