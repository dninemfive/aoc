using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem4
{
    [SolutionToProblem(4)]
    public static IEnumerable<object> Solve(string[] inputLines)
    {
        List<ScratchCard> scratchCards = inputLines.Select(x => new ScratchCard(x)).ToList();
        yield return scratchCards.Select(x => x.Value).Sum();
        int index = 0;
        while(index < scratchCards.Count)
        {
            Console.WriteLine($"{index,5}\t{scratchCards.Count,10}");
            ScratchCard sc = scratchCards[index];
            int won = sc.WinningNumberCount;
            for(int i = index + 1; i <= index + won; i++)
            {
                scratchCards.Add(scratchCards[i]);
            }
            index++;
        }
        yield return scratchCards.Count;
    }
}
public class ScratchCard
{
    public int CardNumber { get; private set; }
    public IEnumerable<int> WinningNumbers { get; private set; }
    public IEnumerable<int> HasNumbers { get; private set; }
    public ScratchCard(string line)
    {
        List<string> split = line.SplitAndTrim(": ", " | ");
        CardNumber = int.Parse(split[0].SplitAndTrim(" ")[1]);
        WinningNumbers = split[1].ToInts();
        HasNumbers = split[2].ToInts();
    }
    public int WinningNumberCount 
        => HasNumbers.Distinct()
                     .Where(x => WinningNumbers.Contains(x))
                     .Count();
    public int Value
        => WinningNumberCount > 0 ? (int)Math.Pow(2, WinningNumberCount - 1) : 0;
}