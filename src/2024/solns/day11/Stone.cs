using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._24.day11;
internal record Stone<T>()
    where T : INumber<T>
{
    public async Task<IEnumerable<Stone<T>>> Evaluate()
    {

    }
}
