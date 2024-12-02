using d9.aoc.core;
using d9.aoc.core.test;

namespace d9.aoc._23.tests;
[TestClass]
public class Tests
{
    private static AocSolutionGroup? _group = null;
    public static AocSolutionGroup Group
    {
        get
        {
            _group ??= new AocSolutionGroup(typeof(Program).Assembly, "2023");
            return _group;
        }
    }
    [TestMethod]
    public void Test_Day1() => Group.Test(1, 55090, 54845);
    [TestMethod]
    public void Test_Day2() => Group.Test(2, 2061, 72596);
    [TestMethod]
    public void Test_Day3() => Group.Test(3, 520135, 72514855);
    [TestMethod]
    public void Test_Day4() => Group.Test(4, 25004, 14427616);
    [TestMethod]
    public void Test_Day5() => Group.Test(5, 84470622L);
    [TestMethod]
    public void Test_Day6() => Group.Test(6, 500346, 42515755L);
    [TestMethod]
    public void Test_Day7() => Group.Test(7, 248453531, 248781813);
    [TestMethod]
    public void Test_Day8() => Group.Test(8, 12083, 13385272668829);
    [TestMethod]
    public void Test_Day9() => Group.Test(9, 1993300041, 1038);
    [TestMethod]
    public void Test_Day10() => Group.Test(10, 6733);
    [TestMethod]
    public void Test_Day11() => Group.Test(11, 9565386L, 857986849428L);
}