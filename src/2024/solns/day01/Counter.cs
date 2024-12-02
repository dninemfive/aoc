using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._24.day01;
internal class Count
{
    private Dictionary<int, int> _count = new();
    public Count(IEnumerable<int> values)
    {
        foreach (int value in values)
            this[value] += 1;
    }
    public int this[int index]
    {
        get => _count.TryGetValue(index, out int value) ? value : 0;
        set => _count[index] = value;
    }
}
