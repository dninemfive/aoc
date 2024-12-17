using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._24.day11;
internal class Blink
{
    private readonly CountingDictionary<BigInteger, BigInteger> _counts = new();
    public Blink(IEnumerable<BigInteger> values)
    {
        foreach (BigInteger value in values)
            _counts[value]++;
    }
    public Blink(params BigInteger[] values) : this(values.AsEnumerable()) { }
    public Blink Successor(StoneSuccessorCache? cache = null)
    {
        cache ??= new();
        static BigInteger blink(BigInteger n)
            => 
    }
}
