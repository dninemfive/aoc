using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23.day9;
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
        Action<int, T> addToRow = forward ? (i, x) => _rows[i].Add(x) : (i, x) => _rows[i].Insert(0, x);
        Func<T, T, T> addTwoNumbers = forward ? (x, y) => x + y : (x, y) => x - y;
        addToRow(_rows.Count - 1, T.Zero);
        for(int i = _rows.Count - 2; i >= 0; i--)
            addToRow(i, addTwoNumbers(_rows[i].Last(), _rows[i + 1].Last()));
        return forward ? _rows.First().Last() : _rows.First().First();
    }
    public T Next()
        => Extrapolate(forward: true);
    public T Prev()
        => Extrapolate(forward: false);
    public string Pyramid
    {
        get
        {
            string result = "", prefix = "";
            foreach(string s in _rows.Select(x => x.ListNotation()))
            {
                result += $"{prefix}{s}\n";
                prefix += " ";
            }
            return result;
        }
    }
}
