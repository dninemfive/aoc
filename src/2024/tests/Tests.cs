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
}