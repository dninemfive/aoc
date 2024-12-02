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
        => Items.ListNotation();
    public bool IsSafe
    // => Deltas.MinMax() is ( > 0, <= 3) or ( >= -3, < 0);
    {
        get
        {
            bool result = Deltas.MinMax() is ( > 0, <= 3) or ( >= -3, < 0);
            Console.WriteLine($"{this} {Deltas.MinMax()} {result}");
            return result;
        }
    }
}
