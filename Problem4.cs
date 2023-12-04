using System;
using System.Collections;
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
        ScratchCardCollection scratchCards = new(inputLines.Select(x => new ScratchCard(x)));
        yield return scratchCards.Select(x => x.Value).Sum();
        yield return scratchCards.WonCardCountFor(1);
    }
}
public class ScratchCardCollection(IEnumerable<ScratchCard> scratchCards) : IEnumerable<ScratchCard>
{
    private readonly Dictionary<int, ScratchCard> _cards = scratchCards.ToDictWithKey(x => x.CardNumber);
    public IEnumerable<int> WonCards(int index)
    {
        Console.WriteLine($"");
    }
    public int WonCardCountFor(int index, int tabs = 0)
    {
        string tabstr = "";
        for (int i = 0; i < tabs; i++)
            tabstr += "  ";
        int result = 1;
        foreach(int i in _cards[index].WonCardIndices)
        {
            result += WonCardCountFor(i, tabs + 1);
        }
        Console.WriteLine($"{tabstr}WonCardCountFor({index}, {tabs}) = {result}");
        return result;
    }
    public int TotalWonCards => _cards.Keys.Select(WonCardCountFor).Sum();
    public IEnumerator<ScratchCard> GetEnumerator()
        => _cards.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_cards.Values).GetEnumerator();
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
    public IEnumerable<int> WonCardIndices
    {
        get
        {
            for (int i = 1; i <= WinningNumberCount; i++)
                yield return CardNumber + i;
        }
    }
}