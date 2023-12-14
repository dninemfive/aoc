using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
    public T Next(bool print = false)
    {
        _rows[^1].Add(T.Zero);
        if (print) Console.WriteLine(_rows[^1].ListNotation());
        for(int i = _rows.Count - 2; i >= 0; i--)
        {
            _rows[i].Add(_rows[i].Last() + _rows[i + 1].Last());
            if (print) Console.WriteLine(_rows[i].ListNotation());
        }
        return _rows.First().Last();
    }
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
