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
    }
    public static IEnumerable<int> Rank(this IEnumerable<Hand> hands)
    {
        int rank = hands.Count();
        foreach (Hand hand in hands.OrderDescending())
        {
            // Console.WriteLine($"{hand}\t{rank}");
            yield return rank--;
        }
    }
}
public readonly struct Hand
    : IComparable<Hand>
{
    public readonly IEnumerable<CamelCard> Cards;
    public readonly int Bet;
    public Hand(string line)
    {
        string[] split = line.Split(" ");
        Cards = split.First().Select(x => new CamelCard(x));
        Bet = split.Second().Parse<int>();
    }
    public override string ToString() => $"{Type,-12}\t{Cards.Select(x => x.Name).Merge()}";
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
    public IEnumerable<int> UniqueValues => Cards.Distinct()
                                                 .Select(x => x.Value)
                                                 .Order();
    // 7.2 variation: figure out what the largest *possible* run is here instead
    // (by enumerating all the non-excluded combinations when you ignore Js)
    public int LargestRunCount
    {
        get
        {
            // having to do this because you can't use `this` in lambda expressions is so annoying lol
            IEnumerable<CamelCard> cards = Cards;
            return cards.Select(x => cards.Count(y => y == x)).Max();
        }
    }
    public int NumRuns
    {
        get
        {
            int ct = 0;
            foreach(int i in UniqueValues)
            {
                if (Cards.Count(x => x == i) > 1)
                    ct++;
            }
            return ct;
        }
    }
    public HandType Type => (LargestRunCount, NumRuns) switch
    {
        (5, _) => HandType.FiveOfAKind,
        (4, _) => HandType.FourOfAKind,
        (3, 2) => HandType.FullHouse,
        (3, 1) => HandType.ThreeOfAKind,
        (2, 2) => HandType.TwoPair,
        (2, 1) => HandType.OnePair,
        _ => HandType.HighCard
    };
    public int this[int index] => Cards.ElementAt(index);
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
public readonly struct CamelCard(char c) : IEquatable<CamelCard>, IComparable<CamelCard>
{
    public string Name => $"{c}";
    public int Value => c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 11,
        'T' => 10,
        >= '0' and <= '9' => c - '0',
        _ => throw new ArgumentOutOfRangeException(nameof(c))
    };
    public static implicit operator int(CamelCard cc) => cc.Value;
    public static implicit operator string(CamelCard cc) => cc.Name;
    public override string ToString() => this;
    public override bool Equals(object? obj) 
        => obj is CamelCard card && Equals(card);
    public bool Equals(CamelCard other)
        => this == other;
    public override int GetHashCode()
        => Value.GetHashCode();
    public int CompareTo(CamelCard other)
        => Value.CompareTo(other.Value);
    public static bool operator ==(CamelCard a, CamelCard b) 
        => a.Value == b.Value;
    public static bool operator !=(CamelCard a, CamelCard b) 
        => !(a == b);
}