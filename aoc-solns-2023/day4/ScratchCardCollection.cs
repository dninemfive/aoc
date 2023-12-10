using System.Collections;
using d9.aoc._23.shared;
namespace d9.aoc._23.day4;
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
            foreach (int index in _cards.OrderBy(x => x.Value.WinningNumberCount).Select(x => x.Key))
                total += GetOrCalculateCount(index);
            return total;
        }
    }
    public IEnumerator<ScratchCard> GetEnumerator()
        => _cards.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_cards.Values).GetEnumerator();
}