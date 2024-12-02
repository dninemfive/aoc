using System.Numerics;
namespace d9.aoc._23.day09;
public class Sequence<T>
    where T : INumber<T>
{
    private readonly List<List<T>> _rows;
    public Sequence(IEnumerable<T> input)
    {
        _rows = [input.ToList()];
        while(_rows.Last().Any(x => x != T.Zero))
            _rows.Add(_rows.Last().Diffs().ToList());
    }
    private T Extrapolate(bool forward = true)
    {
        // if we're extrapolating to the future, add to the end of the sequence; otherwise, add to the beginning
        Action<int, T> addToRow = forward ? (i, x) => _rows[i].Add(x) : (i, x) => _rows[i].Insert(0, x);
        // if we're extrapolating to the future, add the current and next higher derivatives; otherwise, subtract the higher from the current
        Func<T, T, T> combine = forward ? (x, y) => x + y : (x, y) => x - y;
        // if we're extrapolating to the future, get the last item of the row; otherwise, get the first
        Func<int, T> getFromRow = forward ? (i) => _rows[i].Last() : (i) => _rows[i].First();
        addToRow(_rows.Count - 1, T.Zero);
        for(int i = _rows.Count - 2; i >= 0; i--)
            addToRow(i, combine(getFromRow(i), getFromRow(i + 1)));
        return getFromRow(0);
    }
    public T Next()
        => Extrapolate(forward: true);
    public T Prev()
        => Extrapolate(forward: false);
}
