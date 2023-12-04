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
        Dictionary<int, int> copiesWon = new();
        void increment(int index)
        {
            if (copiesWon.TryGetValue(index, out int copies))
                copiesWon[index] = copies + 1;
            else
                copiesWon[index] = 0;
        }
        foreach(ScratchCard sc in scratchCards)
        {
            int won = sc.WinningNumberCount;
            Console.WriteLine($"{sc.CardNumber,3}\t{won,2}\t{scratchCards.Select(x => x.CardNumber)
                                                                         .Where(x => x > sc.CardNumber && x <= sc.CardNumber + won)
                                                                         .ListNotation()}");
            for (int i = sc.CardNumber + 1; i <= sc.CardNumber + won; i++)
            {
                increment(i);
            }
        }
        yield return copiesWon.Values.Sum();
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