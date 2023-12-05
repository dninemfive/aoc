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
        yield return scratchCards.TotalWonCards;
    }
}
public class ScratchCardCollection(IEnumerable<ScratchCard> scratchCards) : IEnumerable<ScratchCard>
{
    private readonly Dictionary<int, ScratchCard> _cards = scratchCards.ToDictWithKey(x => x.CardNumber);
    private readonly Dictionary<int, int> _cardCountCache = new();
    private int GetOrCalculateCount(int index)
    {
        if (_cardCountCache.TryGetValue(index, out int result))
            return result;
        int sum = 1;
        foreach (int i in _cards[index].WonCardIndices)
            sum += GetOrCalculateCount(i);
        _cardCountCache[index] = sum;
        return sum;
    }
    public int TotalWonCards
    {
        get
        {
            int total = 0;
            foreach(int index in _cards.OrderBy(x => x.Value.WinningNumberCount).Select(x => x.Key))
                total += GetOrCalculateCount(index);
            return total;
        }
    }
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
        WinningNumbers = split[1].ToMany<int>();
        HasNumbers = split[2].ToMany<int>();
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