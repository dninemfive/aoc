using System.Numerics;

namespace d9.aoc._24.day13;
internal record Combo<T>(T Cheap, T Expensive, T Cost)
    where T : INumber<T>
{
    public static Combo<T> From(ClawMachine<T> parent, T c, T e)
        => new(c, e, parent.CheapButton.Cost * c + parent.ExpensiveButton.Cost * e);
    public override string ToString()
        => $"Combo {Cheap}/{Expensive} (${Cost})";
}
