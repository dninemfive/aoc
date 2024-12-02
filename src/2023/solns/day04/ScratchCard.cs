using d9.aoc.core;

namespace d9.aoc._23.day04;
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