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
            _group ??= new AocSolutionGroup(typeof(Program).Assembly, "2024");
            return _group;
        }
    }
    [TestMethod]
    public void Test_Day1() => Group.Test(1, 1660292, 22776016);
}