using d9.aoc.core.utils;
using System.Diagnostics.CodeAnalysis;
using Direction = d9.aoc.core.Point<int>;
using GuardReport = (System.Collections.Generic.HashSet<d9.aoc.core.Point<int>> initialPositions,
                     bool isCycle,
                     d9.aoc.core.Grid<char> track);
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
            if (c.IsObstacle())
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
        Map track = MapWithGuard;
        bool isCycle = false;
        while(state?.Step(out state) ?? false)
        {
            if(state is MapState s && s.Guard is Guard g)
            {
                track = track.CopyWith(g);
                if (guardStates.Contains(g))
                {
                    isCycle = true;
                    break;
                }
                touchedPositions.Add(g.Position);
                guardStates.Add(g);
            }
        }
        return (touchedPositions, isCycle, track);
    }
    public Map MapWithGuard
        => Guard is null ? Map : Map.CopyWith(Guard);
    public override string ToString()
        => MapWithGuard.LayOut();
}