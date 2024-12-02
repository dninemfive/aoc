namespace d9.aoc._23.day02;
internal static class Extensions
{
    public static int Power(this Dictionary<string, int> cubeSet)
        => cubeSet.Values.Aggregate((x, y) => x * y);
}
