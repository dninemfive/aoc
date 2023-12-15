using d9.aoc._23.day1;
using d9.aoc._23.day2;
using d9.aoc._23.day3;
using d9.aoc._23.day4;
using d9.aoc._23.day5;
using d9.aoc._23.day6;
using d9.aoc._23.day7;
using d9.aoc._23.day8;
using d9.aoc._23.day9;
namespace d9.aoc._23.tests;
[TestClass]
public class Tests
{
    [TestMethod]
    public void Test_Day1() => Utils.AssertSolution(day1.Solution.Solve, 55090, 54845);
    [TestMethod]
    public void Test_Day2() => Utils.AssertSolution(day2.Solution.Solve, 2061, 72596);
    [TestMethod]
    public void Test_Day3() => Utils.AssertSolution(day3.Solution.Solve, 520135, 72514855);
    [TestMethod]
    public void Test_Day4() => Utils.AssertSolution(day4.Solution.Solve, 25004, 14427616);
    [TestMethod]
    public void Test_Day5() => Utils.AssertSolution(day5.Solution.Solve, 84470622L);
    [TestMethod]
    public void Test_Day6() => Utils.AssertSolution(day6.Solution.Solve, 500346, 42515755L);
    [TestMethod]
    public void Test_Day7() => Utils.AssertSolution(day7.Solution.Solve, 248453531, 248781813);
    [TestMethod]
    public void Test_Day8() => Utils.AssertSolution(day8.Solution.Solve, 12083, 13385272668829);
    [TestMethod]
    public void Test_Day9() => Utils.AssertSolution(day9.Solution.Solve, 1993300041, 1038);
    [TestMethod]
    public void Test_Day10() => Utils.AssertSolution(day10.Solution.Solve, 6733);
    [TestMethod]
    public void Test_Day11() => Utils.AssertSolution(day11.Solution.Solve, 9565386L, 857986849428L);
}