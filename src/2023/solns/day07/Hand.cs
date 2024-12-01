namespace d9.aoc._23.day7;
using d9.aoc.core;

public readonly struct Hand
    : IComparable<Hand>
{
    public readonly IEnumerable<CamelCard> Cards;
    public readonly int Bet;
    public readonly bool JokerMode;
    public Hand(string line, bool jokerMode = false)
    {
        string[] split = line.Split(" ");
        Cards = split.First().Select(x => new CamelCard(x, jokerMode)).ToList();
        Bet = split.Second().Parse<int>();
        JokerMode = jokerMode;
    }
    public IEnumerable<CamelCard> UniqueCards => Cards.Distinct();
    public IEnumerable<CamelCard> NonJokerCards => UniqueCards.Where(x => x != 'J');
    public int Count(CamelCard cardType)
        => Cards.Count(x => x == cardType);
    public Run RunFor(CamelCard cc)
        => (cc, Count(cc));
    public IEnumerable<Run> Runs
    {
        get
        {
            if (JokerMode && UniqueCards.Any(x => x == 'J'))
            {
                List<Run> result = [.. NonJokerCards.Select(RunFor)
                                                    .OrderByDescending(x => x.Count)];
                (_, int jokerCount) = RunFor(new('J', true));
                if (!result.Any())
                    return [RunFor(new('J', true))];
                result[0] = result.First() + jokerCount;
                return result;
            }
            else
            {
                return UniqueCards.Select(RunFor).OrderByDescending(x => x.Count);
            }
        }
    }
    // this particular implementation stolen from OtherwiseJunk, because i accidentally spoiled myself.
    // it was quite similar to what i already had, though :willemsmug:
    public HandType Type => Runs.Select(x => x.Count).ToList() switch
    {
        [5] => HandType.FiveOfAKind,
        [4, 1] => HandType.FourOfAKind,
        [3, 2] => HandType.FullHouse,
        [3, 1, 1] => HandType.ThreeOfAKind,
        [2, 2, 1] => HandType.TwoPair,
        [2, 1, 1, 1] => HandType.OnePair,
        _ => HandType.HighCard
    };
    public CamelCard this[int index] => Cards.ElementAt(index);
    public int CompareTo(Hand other)
    {
        if (other.Type != Type)
        {
            // i accidentally ordered the enum backward lol
            return other.Type.CompareTo(Type);
        }
        for (int i = 0; i < Cards.Count(); i++)
        {
            if (this[i] == other[i])
                continue;
            return this[i].CompareTo(other[i]);
        }
        return 0;
    }
}