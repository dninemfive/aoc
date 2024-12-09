namespace d9.aoc._24.day09;
[SolutionToProblem(9)]
[SampleResults(1928)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Filesystem filesystem = Filesystem.FromLine(lines.First());
        Console.WriteLine(filesystem);
        Console.WriteLine();
        yield return "preinit";
        Filesystem compressed = filesystem.Compress();
        Console.WriteLine(compressed);
        yield return compressed.Checksum;
    }
}
internal readonly struct Filesystem(IEnumerable<int?> data)
{
    internal readonly IReadOnlyList<int?> _data = data.ToList();
    public static Filesystem FromLine(string line)
    {
        IEnumerable<int?> generate()
        {
            for (int i = 0; i < line.Length; i++)
            {
                int blockSize = int.Parse($"{line[i]}");
                int? blockContents = i % 2 == 0 ? i / 2 : null;
                for (int j = 0; j < blockSize; j++)
                    yield return blockContents;
            }
        }
        return new(generate());
    }
    public Filesystem Compress()
    {
        List<int?> list = _data.ToList();
        for(int _ = 0; _ < 1000; _++)
        {
            bool encounteredNull = false;
            bool isSorted = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is null)
                {
                    encounteredNull = true;
                    int l = list.IndexOfLastNonNullValue();
                    if(l > i)
                        (list[i], list[l]) = (list[l], list[i]);
                } 
                else if (encounteredNull)
                {
                    isSorted = false;
                } 
            }
            // Console.WriteLine(list.Select(x => x.PrintNull(".")).Join());
            if (isSorted)
                break;
        } 
        return new(list);
    }
    public int Checksum
    {
        get
        {
            int result = 0;
            for(int i = 0; i < _data.Count; i++)
                if (_data[i] is int n)
                    result += i * n;
            return result;
        }
    }
    public override string ToString()
        => _data.Chunk(47)
                .Select(y => y.Select(x => x is int n ? $"{n,4}" : "____")
                              .JoinWithDelimiter(" "))
                .JoinWithDelimiter("\n");
}
internal static class Extensions
{
    public static bool CanBeCompressed<T>(this IEnumerable<T?> items)
    {
        bool foundNull = false;
        foreach (T? item in items)
        {
            if(item is null)
            {
                foundNull = true;
            }
            else if(foundNull)
            {
                return false;
            }
        }
        return true;
    }
    public static void Swap<T>(this List<T> list, int indexA, int indexB)
    {
        (list[indexB], list[indexA]) = (list[indexA], list[indexB]);
    }
    public static int IndexOfLastNonNullValue<T>(this IEnumerable<T?> enumerable)
    {
        int index = enumerable.Count() - 1;
        foreach(T? item in enumerable.Reverse())
        {
            if (item is not null)
                return index;
            index--;
        }
        throw new Exception($"Enumerable did not contain any non-null values!");
    }
}