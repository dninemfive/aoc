using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem7
{
    public static string CamelCardName(this int n) => n switch
    {
        14 => "A",
        13 => "K",
        12 => "Q",
        11 => "J",
        10 => "T",
        >= 2 and <= 9 => $"{n}",
        _ => throw new ArgumentOutOfRangeException(nameof(n))
    };
    public static IEnumerable<(Hand hand, int rank)> Rank(IEnumerable<Hand> hands)
    {
        int rank = hands.Count();
        foreach(Hand hand in hands)
            yield return (hand, rank--);
    }
}
public readonly struct Hand(params int[] cards)
    : IComparable<Hand>
{
    public readonly int[] Cards = cards;
    public override string ToString() => Cards.Select(Problem7.CamelCardName).Merge();
    public int CompareTo(Hand other)
    {
        if(other.Type != Type)
        {
            return Type.CompareTo(other.Type);
        }
        for(int i = 0; i < Cards.Length; i++)
        {
            if (this[i] == other[i])
                continue;
            return this[i].CompareTo(other[i]);
        }
        return 0;
    }
    public IEnumerable<int> UniqueValues => Cards.Distinct().Order();
    public int LargestRunCount
    {
        get
        {
            // having to do this because you can't use `this` in lambda expressions is so annoying lol
            IEnumerable<int> cards = Cards;
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
    public int this[int index] => Cards[index];
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