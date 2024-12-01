using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc.core;
public record AocSolutionPart(object Result, string? Label = null)
{
    public static implicit operator AocSolutionPart((object result, string label) tuple)
        => new(tuple.result, tuple.label);
    public static implicit operator AocSolutionPart(string result)
        => new(result);
}
