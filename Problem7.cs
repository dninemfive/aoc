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
        IEnumerable<(Hand hand, int bet)> input = lines.Select(x => x.Split(" "))
                                                       .Select(x => (new Hand(x[0]), int.Parse(x[1])));
        yield return input.Select(x => x.hand)
                          .Rank()
                          .Zip(input.Select(x => x.bet))
                          .Select(x => x.First * x.Second)
                          .Sum();
    }
    public static IEnumerable<int> Rank(this IEnumerable<Hand> hands)
    {
        int rank = hands.Count();
        foreach (Hand hand in hands)
            yield return rank--;
    }
}
public readonly struct Hand(params int[] cards)
    : IComparable<Hand>
{
    public readonly int[] Cards = cards;
    public Hand(string line) : this(line.Select(CamelCard.Value).ToArray()) { }
    public override string ToString() => Cards.Select(CamelCard.Name).Merge();
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
public static class CamelCard
{
    public static string Name(this int n) => n switch
    {
        14 => "A",
        13 => "K",
        12 => "Q",
        11 => "J",
        10 => "T",
        >= 2 and <= 9 => $"{n}",
        _ => throw new ArgumentOutOfRangeException(nameof(n))
    };
    public static int Value(char c) => c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 11,
        'T' => 10,
        >= '0' and <= '9' => c - '0',
        _ => throw new ArgumentOutOfRangeException(nameof(c))
    };
}