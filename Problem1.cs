using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_solns_2023;
public class Problem1
{
    public const string INPUT_PATH = @"C:\Users\dninemfive\Documents\workspaces\misc\_aoc\2023\1_input.txt";
    public void Solve()
    {
        IEnumerable<string> lines = File.ReadAllLines(INPUT_PATH);
        Console.WriteLine(lines.Sum(CalibrationValue));
    }
    public int CalibrationValue(string line) => FirstDigit(line) * 10 + LastDigit(line);
    protected virtual int GetDigit(string line, bool first)
    {
        foreach(char c in (first ? line : line.Reverse()))
        {
            if (c is >= '0' and <= '9')
                return c - '0';
        }
        throw new ArgumentException($"Could not find a valid digit in line {line}!", nameof(line));
    }
    protected virtual int FirstDigit(string line)
        => GetDigit(line, first: true);
    protected virtual int LastDigit(string line)
        => GetDigit(line, first: false);
}
