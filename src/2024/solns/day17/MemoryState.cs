using System.Numerics;

namespace d9.aoc._24.day17;
/// <summary>
/// Stores the state of the three registers used by a program.
/// </summary>
/// <typeparam name="T">The type of value in each register.</typeparam>
/// <param name="a"></param>
/// <param name="b"></param>
/// <param name="c"></param>
internal readonly struct MemoryState<T>(T a, T b, T c)
    where T : struct, INumber<T>
{
    /// <summary>The first register, <c>A</c>.</summary>
    public readonly T A = a;
    /// <summary>The second register, <c>B</c>.</summary>
    public readonly T B = b;
    /// <summary>The third register, <c>C</c>.</summary>
    public readonly T C = c;
    /// <summary>
    /// Copies the specified state with changes to any of the registers specified; if no value
    /// is provided for any given register, it retains its current value.
    /// </summary>
    /// <param name="a">The new value for the first register, <c>A</c>, if any.</param>
    /// <param name="b">The new value for the second register, <c>B</c>, if any.</param>
    /// <param name="c">The new value for the third register, <c>C</c>, if any.</param>
    /// <returns>A new state with the specified changes.</returns>
    public MemoryState<T> CopyWith(T? a = null, T? b = null, T? c = null)
        => (a ?? A,
            b ?? B,
            c ?? C);
    public static implicit operator MemoryState<T>((T a, T b, T c) tuple)
        => new(tuple.a, tuple.b, tuple.c);
}