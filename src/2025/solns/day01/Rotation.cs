using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace d9.aoc._25.day01;
internal record Rotation<T>(Direction Direction, T Distance)
    where T : INumber<T>
{
    internal static readonly T MAX = T.CreateChecked(100);

    public static implicit operator Rotation<T>(string s)
        => new(s[0].ToDirection(), T.Parse(s.AsSpan()[1..], CultureInfo.InvariantCulture));
    public static implicit operator Rotation<T>(T t)
        => new(T.IsNegative(t) ? Direction.Left : Direction.Right, T.Abs(t));
    public static implicit operator T(Rotation<T> rotation)
        => rotation.Direction.Sign<T>() * rotation.Distance;
    public static Rotation<T> operator +(T initial, Rotation<T> rotation)
    {
        return (initial + (T)rotation) % MAX;
    }
}