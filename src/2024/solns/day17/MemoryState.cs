using System.Numerics;

namespace d9.aoc._24.day17;
internal readonly struct MemoryState<T>(T a, T b, T c)
    where T : struct, INumber<T>
{
    internal readonly T A = a, B = b, C = c;
    internal MemoryState<T> CopyWith(T? a = null, T? b = null, T? c = null)
        => (a ?? A,
            b ?? B,
            c ?? C);
    public static implicit operator MemoryState<T>((T a, T b, T c) tuple)
        => new(tuple.a, tuple.b, tuple.c);
    internal void Deconstruct(out T a, out T b, out T c)
    {
        a = A;
        b = B;
        c = C;
    }
}