using System.Numerics;

namespace d9.aoc._24.day13;
internal record ClawMachine<T>
    where T : INumber<T>
{
    public IReadOnlyList<Button<T>> Buttons;
    public Button<T> CheapButton => Buttons[0];
    public Button<T> ExpensiveButton => Buttons[1];
    public Point<T> Prize;
    public ClawMachine(IEnumerable<Button<T>> buttons, Point<T> prize)
    {
        Buttons = buttons.OrderBy(x => x.Cost).ToList();
        Prize = prize;
    }
    public static ClawMachine<T> FromLines(IEnumerable<string> lines)
    {
        return new(
            lines.Select(Button<T>.FromLine)
                 .Where(x => x is not null)!,
            lines.Select(Prize<T>.FromLine)
                 .Where(x => x is not null)
                 .First()!
                  );
    }
    public IEnumerable<Combo<T>> Combos()
    {
        T cheapMax = CheapButton.StepsToReachOrPass(Prize);
        for (T c = cheapMax; c > T.Zero; c--)
        {
            Point<T> start = c * CheapButton.Offset;
            if (ExpensiveButton.Offset.CanReach(start, Prize, out T e))
                yield return Combo<T>.From(this, c, e);
        }
    }
    public Combo<T>? CheapestCombo()
    {
        // the cheapest combo will use the cheapest button as many times as possible
        foreach (Combo<T> combo in Combos())
            return combo;
        return null;
    }
    public T CheapestComboCost()
    {
        if (CheapestCombo() is Combo<T> combo)
            return combo.Cost;
        return T.Zero;
    }
    public override string ToString()
        => $"Claw Machine {Buttons.ListNotation()} -> {Prize}";
}