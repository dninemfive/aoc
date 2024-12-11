using d9.aoc.core.utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace d9.aoc._24.day06;
[SolutionToProblem(6)]
[SampleResults(41, 6)]
[FinalResults(5453)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Grid<char> data = Grid<char>.From(lines);
        yield return "preinit";
        MapState initial = MapState.FromInitial(data);
        (HashSet<Point<int>> positions, bool _, Grid<int> _) = initial.Run();
        yield return positions.Count;
        yield return CyclesIn(data, positions).Count(x => x);
    }
    public static IEnumerable<bool> CyclesIn(Grid<char> data, IEnumerable<Point<int>> variantPositions)
    {
        string baseDir = $"_debug/{data.GetHashCode()}",
               cycleDir     = Path.Join(baseDir, "cycle"),
               noCycleDir   = Path.Join(baseDir, "nocycle");
        Directory.CreateDirectory(baseDir);
        Directory.CreateDirectory(cycleDir);
        Directory.CreateDirectory(noCycleDir);
        foreach(Point<int> p in variantPositions)
            if (!data[p].IsGuard() && !data[p].IsObstacle())
            {
                (HashSet<Point<int>> positions, bool isCycle, Grid<int> track) = MapState.FromInitial(data.CopyWith((p, '#'))).Run();
                WriteImage(track, 
                           p,
                           Path.Join(isCycle 
                                        ? cycleDir 
                                        : noCycleDir, 
                                     $"{positions.Count} {p}.png"),
                           isCycle);
                yield return isCycle;
            }
    }
    public static void WriteImage(Grid<int> data, Point<int> newObstacle, string path, bool isCycle, int scale = 8)
    {
        int maxIndex = data.AllPoints.Select(x => data[x]).Max();
        using Image<Rgba32> result = new(data.Width, data.Height);
        float HueFor(int index)
            => (isCycle ? 360f : 320f) * index / maxIndex;
        foreach ((int x, int y) in data.AllPoints)
        {
            result[x, y] = data[x, y] switch
            {
                -1 => Color.Black,
                0 => new Rgba32(69, 69, 69),
                _ => ColorSpaceConverter.ToRgb(new Hsv(HueFor(data[x, y]), 1, 1))
            };
        }
        result[newObstacle.X, newObstacle.Y] = Color.White;
        result.Mutate(x => x.Resize(new ResizeOptions()
        {
            Sampler = KnownResamplers.NearestNeighbor,
            Size = new(result.Width * scale, result.Height * scale)
        }));
        result.SaveAsPng(path);
    }
}
