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
    // => Deltas.IsMonotonic() && Deltas.Select(Math.Abs).Max() <= 3;
    {
        get
        {
            bool isMonotonic = Deltas.IsMonotonic();
            int max = Deltas.Select(Math.Abs).Max();
            Console.WriteLine($"{this}: {isMonotonic}, {max}");
            return isMonotonic && max <= 3;
        }
    }
}
