﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Position      = d9.aoc.core.Point<int>;
using Direction     = d9.aoc.core.Point<int>;
using Directions    = d9.aoc.core.Directions<int>;
using d9.aoc.core.utils;
namespace d9.aoc._24.day06;
internal record Guard(Position Position, Direction Direction)
{
    internal static readonly Dictionary<Direction, char> DirectionMap = new()
    {
        { Directions.Down,    '^' },
        { Directions.Right, '>' },
        { Directions.Up,  'v' },
        { Directions.Left,  '<' }
    };
    internal Guard(Position position, char c) : this(position, c.Direction()) { }
    public char Character
        => DirectionMap[Direction];
    public override string ToString()
        => $"{Character} {Position}";
}
internal class Map(Grid<char> map)
{
    private Grid<char> _map = map;
    private HashSet<Position> _touchedPositions = new();
    private Guard _guard = map.FindGuard();
    public void Run()
    {
        _touchedPositions.Add(_guard.Position);
        Position lastGuardPosition = _guard.Position;
        while((_guard = Step()) is not null)
        {
            _touchedPositions.Add(_guard.Position);
            UpdateMap(lastGuardPosition);
            lastGuardPosition = _guard.Position;
        }
    }
    public Guard? Step()
    {
        //Console.WriteLine();
        //Console.WriteLine(_map.LayOut());
        char? next = _map.CellInFrontOf(_guard);
        if (next is null)
        {
            return null;
        }
        (Position pos, Direction dir) = _guard;
        if(next.IsObstacle())
        {
            dir = dir.RotateClockwise();
        }
        return new(pos + dir, dir);
    }
    public void UpdateMap(Position lastGuardPosition)
    {
        _map = _map.CopyWith(
            (lastGuardPosition, '.'),
            (_guard.Position, _guard.Character)
            );
    }
    public int TouchedPositionCount
        => _touchedPositions.Count;
}
internal static class Extensions
{
    public static Position GuardPosition(this Grid<char> grid)
        => grid.AllPoints.Where(x => grid[x].IsGuard()).First();
    public static Guard FindGuard(this Grid<char> grid)
    {
        Position pos = grid.GuardPosition();
        return new(pos, grid[pos]);
    }
    public static Direction Direction(this char c)
    {
        foreach ((Direction k, char v) in Guard.DirectionMap)
        {
            if (v == c)
                return k;
        }
        throw new ArgumentException($"'{c}' does not correspond to a direction!", nameof(c));
    }
    public static bool IsGuard(this char c)
        => c is '^' or '>' or 'v' or '<';
    public static bool IsObstacle(this char? c)
        => c is '#';
    public static char? CellInFrontOf(this Grid<char> grid, Position cell, Direction direction)
        => grid.TryGet(cell + direction, out char? c) ? c : null;
    public static char? CellInFrontOf(this Grid<char> grid, Guard guard)
        => grid.CellInFrontOf(guard.Position, guard.Direction);
    public static Direction RotateClockwise(this Direction d)
        => d switch
        {
            ( 0,  1) => (-1,  0),
            ( 1,  0) => ( 0,  1),
            ( 0, -1) => ( 1,  0),
            (-1,  0) => ( 0, -1),
            _ => throw new ArgumentException($"{d} is not a direction i know how to rotate, sorry!", nameof(d))
        };
}