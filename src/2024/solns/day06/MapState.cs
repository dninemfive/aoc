using d9.aoc.core.utils;
using System.Diagnostics.CodeAnalysis;
using Direction = d9.aoc.core.Point<int>;
using GuardReport = (System.Collections.Generic.HashSet<d9.aoc.core.Point<int>> initialPositions, bool isCycle);
using Map = d9.aoc.core.Grid<char>;
using Position = d9.aoc.core.Point<int>;
namespace d9.aoc._24.day06;
internal readonly struct MapState(Map map, Guard? guard)
{
    public readonly Map Map = map;
    public readonly Guard? Guard = guard;
    private char? this[Position? pos]
    {
        get
        {
            if (pos is Position p && Map.TryGet(p, out char? result))
                return result;
            return null;
        }
    }
    public static MapState FromInitial(Map map)
    {
        Guard guard = map.FindGuard();
        return new(map.CopyWith((guard.Position, '.')), guard);
    }
    public bool Step([NotNullWhen(true)] out MapState? nextState)
    {
        if (this[Guard?.Ahead] is char c)
        {
            (Position p, Direction d) = Guard!;
            if (c == '#')
                d = d.RotateClockwise();
            nextState = new(Map, new(p + d, d));
            return true;
        }
        nextState = null;
        return false;
    }
    public GuardReport Run()
    {
        HashSet<Position> touchedPositions = [Guard!.Position];
        HashSet<Guard> guardStates = [Guard];
        MapState? state = this;
        bool isCycle = false;
        // int step = 0;
        while(state?.Step(out state) ?? false)
        {
            // Console.WriteLine($"\nStep {++step}");
            // Console.WriteLine(state);
            if(state is MapState s && s.Guard is Guard g)
            {
                if (guardStates.Contains(g))// || step > 10000)
                {
                    isCycle = true;
                    break;
                }
                touchedPositions.Add(g.Position);
                guardStates.Add(g);
            }
        }
        return new(touchedPositions, isCycle);
    }
    public override string ToString()
    {
        if (Guard is not null)
            return Map.CopyWith((Guard.Position, Guard.Character)).LayOut();
        return Map.LayOut();
    }
}