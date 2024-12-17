using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._24.day11;
internal class Blink(IReadOnlyDictionary<BigInteger, BigInteger> counts, int index = 1)
{
    public readonly int Index = index;
    private readonly CountingDictionary<BigInteger, BigInteger> _counts = new(counts);

    public Blink(IEnumerable<BigInteger> values, int index = 1) : this(new CountingDictionary<BigInteger, BigInteger>(values), index) { }
    public Blink(params BigInteger[] values) : this(values.AsEnumerable()) { }
    public Blink GenerateSuccessor()
    {
        CountingDictionary<BigInteger, BigInteger> result = new();
        // .Ascending() doesn't actually matter, just don't want to have to cast _counts because i probably fucked up somewhere
        foreach((BigInteger stone, BigInteger count) in _counts.Ascending())
            foreach (BigInteger successor in stone.Successors())
                result[successor] += count;
        return new(result, Index + 1);
    }
    public Blink GenerateSuccessor(int n)
    {
        Blink result = GenerateSuccessor();
        for (int i = 0; i < n; i++)
            result = result.GenerateSuccessor();
        return result;
    }
    public BigInteger Count
        => _counts.Total;
    public override string ToString()
        => $"Blink {Index,2}\t{Count,16}";
}
