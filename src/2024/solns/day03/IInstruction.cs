using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace d9.aoc._24.day03;
internal interface IInstruction<TSelf>
    where TSelf : IInstruction<TSelf>
{
    public static abstract Regex Regex { get; }
    public static abstract TSelf FromMatch(string match);
}
