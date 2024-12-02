namespace d9.aoc._24.day02;
public class Report(string line)
{
    public readonly List<int> Items = line.ToMany<int>().ToList();
    private IEnumerable<int>? _deltas = null;
    public IEnumerable<int> Deltas
    {
        get
        {
            if (_deltas is null)
                _deltas = Items.Deltas();
            return _deltas;
        }
    }
    public override string ToString()
        => Items.Select(x => $"{x,2}").ListNotation();
    public bool IsStrictlySafe
        => Deltas.IsSafe();
    public bool IsLooselySafe
    {
        get
        {
            IEnumerable<int> orderedDeltas = Deltas.Order();
            return orderedDeltas.Skip(1).IsSafe() || orderedDeltas.SkipLast(1).IsSafe();
        }
    }
}
