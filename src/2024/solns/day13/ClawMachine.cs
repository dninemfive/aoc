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
    public (T c, T e)? CheapestCombo()
    {
        // the cheapest combo will use the cheapest button as many times as possible
        T cheapMax = CheapButton.StepsToReachOrPass(Prize);
        for (T c = cheapMax; c > T.Zero; c--)
        {
            Point<T> start = c * CheapButton.Offset;
            if (ExpensiveButton.Offset.CanReach(start, Prize, out T e))
                return (c, e);
        }
        return null;
    }
    public T ComboCost()
    //    => Combo() is (T a, T b) ? a * ButtonA.cost + b * ButtonB.cost 
    //                               : T.Zero;
    {
        if (CheapestCombo() is (T c, T e))
        {
            T cost = c * CheapButton.Cost + e * ExpensiveButton.Cost;
            // Console.WriteLine($"Combo for {this}: {(c, e)} ({cost})");
            return cost;
        }
        // Console.WriteLine($"No combo for {this}.");
        return T.Zero;
    }
    public override string ToString()
        => $"Claw Machine {Buttons.ListNotation()} -> {Prize}";
}