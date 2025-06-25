using System.Numerics;

namespace d9.aoc._24.day11;
public class Blink(IReadOnlyDictionary<BigInteger, BigInteger> counts, int index = 0)
{
    public readonly int Index = index;
    private readonly CountingDictionary<BigInteger, BigInteger> _counts = new(counts);

    public Blink(IEnumerable<BigInteger> values, int index = 0) 
        : this(new CountingDictionary<BigInteger, BigInteger>(values), index) { }
    public Blink(params BigInteger[] values) : this(values.AsEnumerable()) { }
    // stolen from @OolongTimeNoTea
    public Blink GenerateSuccessor()
    {
        CountingDictionary<BigInteger, BigInteger> counts = new();
        // .Ascending() doesn't actually matter, just don't want to have to cast _counts because i probably fucked up somewhere
        foreach((BigInteger stone, BigInteger count) in _counts.Order())
            foreach (BigInteger successor in stone.Successors())
                counts[successor] += count;
        return new(counts, Index + 1);
    }
    public Blink GenerateSuccessor(int n)
    {
        Blink result = GenerateSuccessor();
        for (int i = 0; i < n - 1; i++)
            result = result.GenerateSuccessor();
        return result;
    }
    public BigInteger Count
        => _counts.Total;
    public override string ToString()
        => $"Blink {Index,2}\t{Count,16}";
}
