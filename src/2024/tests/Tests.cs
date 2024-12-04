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
    public void Test_2024() => Group.TestAll();
}