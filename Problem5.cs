using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public class Problem5
{

}
public class XToYMap(string title, string lines)
{
    public string From => title.Split("-")[0];
    public string To => title.Split("-")[2];

}
public readonly struct Range(int start, int length)
{
    public readonly int Start = start;
    public readonly int End = start + length;
    public bool Contains(int n) => n >= Start && n <= End;
}
public class MapRange
{
    public Range Source { get; private set; }
    public Range Destination { get; private set; }
    public int Diff => Source.Start - Destination.Start;
    public int Length { get; private set; }
    public MapRange(string line)
    {
        List<int> values = line.ToMany<int>().ToList();
        (int sourceStart, int destStart, int length) = (values[1], values[0], values[2]);
        Source = new(sourceStart, length);
        Destination = new(destStart, length);
    }
    public int? this[int n]
        => Source.Contains(n) ? n + Diff : null;
}