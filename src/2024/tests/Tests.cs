using d9.aoc.core;
using d9.aoc.core.test;

namespace d9.aoc._24.tests;
[TestClass]
public class Tests
{
    private static AocSolutionGroup? _group = null;
    public static AocSolutionGroup Group
    {
        get
        {
            _group ??= new AocSolutionGroup(typeof(Program).Assembly);
            return _group;
        }
    }
    [TestMethod]
    public void Test_Day01() => Group.Test(1, 1660292, 22776016);
    [TestMethod]
    public void Test_Day02() => Group.Test(2, 257, 328);
    [TestMethod]
    public void Test_Day03() => Group.Test(3, 179834255, 80570939);
    [TestMethod]
    public void Test_Day04_ExampleInput()
    {
        string[] exampleInput =
        {
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        };
        IEnumerable<AocSolutionPart> exampleOutput = Group[4].Execute(exampleInput);
        Assert.AreEqual(18, exampleOutput.First(x => x.ValidIndex == 1).Result.Value);
        Assert.AreEqual( 9, exampleOutput.First(x => x.ValidIndex == 2).Result.Value);
    }
    [TestMethod]
    public void Test_Day04_DoubleX_MAS()
    {
        string[] exampleInput =
        {
            "SMS",
            "SAM",
            "MSM"
        };
        IEnumerable<AocSolutionPart> exampleOutput = Group[4].Execute(exampleInput);
        Assert.AreEqual(2, exampleOutput.First(x => x.ValidIndex == 2).Result.Value);
    }
}