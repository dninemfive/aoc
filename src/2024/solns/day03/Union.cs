namespace d9.aoc._24.day03;
internal readonly struct Union<T1, T2>
{
    private readonly T1? _t1 = default;
    private readonly T2? _t2 = default;
    public Union(T1 t1)
        => _t1 = t1;
    public Union(T2 t2)
        => _t2 = t2;
    public static implicit operator Union<T1, T2>(T1 t1) => new(t1);
    public static implicit operator Union<T1, T2>(T2 t2) => new(t2);
    public T? As<T>()
        where T : struct
        => _t1 is T t1 ? t1 : _t2 is T t2 ? t2 : null; // throw new Exception($"Cannot cast {GetType().Name} to {typeof(T).Name}!");
    public override string ToString()
        => $"{GetType().Name}: {_t1.PrintNull()}/{_t2.PrintNull()}";
}
