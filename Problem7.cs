using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem7
{
    [SolutionToProblem(7)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<Hand> hands = lines.Select(x => new Hand(x)).OrderDescending();
        IEnumerable<int> ranks = hands.Rank();
        yield return hands.Zip(ranks).Select(x => x.First.Bet * x.Second).Sum();
        hands = lines.Select(x => new Hand(x, jokerMode: true)).OrderDescending();
        ranks = hands.Rank();
        foreach((int rank, Hand hand) in ranks.Zip(hands))
        {
            Console.WriteLine($"{rank,4} {hand}");
        }
        yield return hands.Zip(ranks).Select(x => x.First.Bet * x.Second).Sum();
    }
    public static IEnumerable<int> Rank(this IEnumerable<Hand> hands)
    {
        int rank = hands.Count();
        foreach (Hand hand in hands.OrderDescending())
            yield return rank--;
    }
}
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
    public string Name => Cards.Select(x => x.Name).Merge();
    public override string ToString() => $"{Type,-12} {Name} {Runs.ListNotation()}";
    public int CompareTo(Hand other)
    {
        if(other.Type != Type)
        {
            // i accidentally ordered the enum backward lol
            return other.Type.CompareTo(Type);
        }
        for(int i = 0; i < Cards.Count(); i++)
        {
            if (this[i] == other[i])
                continue;
            return this[i].CompareTo(other[i]);
        }
        return 0;
    }
    public IEnumerable<CamelCard> UniqueCards => Cards.Distinct()
                                                      .Order();
    public Run Run(CamelCard cc)
        => (cc, Count(cc));
    public IEnumerable<Run> Runs
    {
        get
        {            
            if(JokerMode && UniqueCards.Any(x => x == 'J'))
            {
                List<Run> result = [.. UniqueCards.Where(x => x != 'J')
                                                  .Select(Run)
                                                  .OrderByDescending(x => x.Count)];
                (_, int jokerCount) = Run(new('J', true));
                if (!result.Any())
                    return [Run(new('J', true))];
                result[0] = result.First() + jokerCount;
                return result;
            } 
            else
            {
                return UniqueCards.Select(Run).OrderByDescending(x => x.Count);
            }
        }
    }
    public int Count(CamelCard cardType)
        => Cards.Count(x => x == cardType);
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
}
public enum HandType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}
public readonly struct CamelCard(char c, bool jokerMode = false) : IEquatable<CamelCard>, IComparable<CamelCard>
{
    private readonly char _c = c;
    public string Name => $"{_c}";
    public readonly bool JokerMode = jokerMode;
    public int Value => _c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => JokerMode ? 1 : 11,
        'T' => 10,
        >= '0' and <= '9' => _c - '0',
        _ => throw new ArgumentOutOfRangeException(nameof(_c))
    };
    public static implicit operator string(CamelCard cc) => cc.Name;
    public static implicit operator char(CamelCard cc) => cc._c;
    public override string ToString() => $"{Name}{(JokerMode ? "'" : "")}";
    public override bool Equals(object? obj) 
        => obj is CamelCard card && Equals(card);
    public bool Equals(CamelCard other)
        => this == other;
    public override int GetHashCode()
        => HashCode.Combine(_c, JokerMode);
    public int CompareTo(CamelCard other)
        => Value.CompareTo(other.Value);
    public static bool operator ==(CamelCard a, CamelCard b) 
        => a.JokerMode == b.JokerMode && a.Value == b.Value;
    public static bool operator !=(CamelCard a, CamelCard b) 
        => !(a == b);
}
public readonly struct Run(CamelCard card, int ct)
{
    public readonly CamelCard Card = card;
    public readonly int Count = ct;
    public void Deconstruct(out CamelCard card, out int count)
    {
        card = Card;
        count = Count;
    }
    public static implicit operator Run((CamelCard card, int ct) tuple)
        => new(tuple.card, tuple.ct);
    public static Run operator +(Run run, int amt) 
        => (run.Card, run.Count + amt);
    public override string ToString()
        => $"({Card}, {Count})";
}