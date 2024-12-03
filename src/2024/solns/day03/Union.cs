using System.Diagnostics.CodeAnalysis;

namespace d9.aoc._24.day03;
internal readonly struct Union<T1, T2>
{
    public readonly bool IsType1;
    private readonly T1 _t1;
    private readonly T2 _t2;
    public Union(T1 t1)
    {
        _t1 = t1;
        _t2 = default!;
        IsType1 = true;
    }
    public Union(T2 t2)
    {
        _t1 = default!;
        _t2 = t2;
        IsType1 = false;
    }
    public static implicit operator Union<T1, T2>(T1 t1) => new(t1);
    public static implicit operator Union<T1, T2>(T2 t2) => new(t2);
    public static string TypeName
        => $"({typeof(T1).Name}|{typeof(T2).Name})";
    public override string ToString()
        => $"{TypeName}: {(IsT1(out T1? t1) ? t1 : IsT2(out T2? t2) ? t2 : "(no value)")}";
    public T As<T>()
        => _t1 is T t1 ? t1 : _t2 is T t2 ? t2 : throw new Exception($"Cannot cast {TypeName} to {typeof(T).Name}!");
    public bool IsT1([MaybeNullWhen(false)]out T1? value)
    {
        value = IsType1 ? _t1 : default;
        return IsType1;
    }
    public bool IsT2([MaybeNullWhen(false)]out T2? value)
    {
        value = !IsType1 ? _t2 : default;
        return !IsType1;
    }
}
