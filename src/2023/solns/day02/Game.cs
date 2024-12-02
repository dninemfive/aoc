using d9.aoc.core;

namespace d9.aoc._23.day02;
public class Game(string line)
{
    public int Id = int.Parse(line.Split(": ")[0].Split(" ")[1]);
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
