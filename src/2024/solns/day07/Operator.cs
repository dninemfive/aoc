using System.Numerics;

namespace d9.aoc._24.day07;
public delegate T OperatorDelegate<T>(T l, T r)
    where T : INumber<T>;
internal class Operator<T>(string name, OperatorDelegate<T> func)
    where T : INumber<T>
{
    public override string ToString()
        => name;
    private readonly OperatorDelegate<T> _func = func;
    public static implicit operator OperatorDelegate<T>(Operator<T> obj)
        => obj._func;
}
internal static class Operators<T>
    where T : INumber<T>
{
    private static readonly Dictionary<string, Operator<T>> _byName = new()
    {
        { "+", Add! },
        { "*", Multiply! }
    };
    public static Operator<T> ByName(string name)
        => _byName[name];
    public static readonly Operator<T> Add         = new("+", (x, y) => x + y);
    public static readonly Operator<T> Multiply    = new("*", (x, y) => x * y);
}