using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(15)]
public class Day15FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 15;

    abstract class SurfacePosition
    {
        public int Number;
        public Surface Surface;
        public int X;
        public int Y;
        public char Character;

        public SurfacePosition(int number, Surface surface, int x, int y)
        {
            Number = number;
            Surface = surface;
            X = x;
            Y = y;
        }
    }
    class Sensor : SurfacePosition
    {
        public Sensor(int number, Surface surface, int x, int y, Beacon beacon) : base(number, surface, x, y)
        {
            Character = 'S';
            Beacon = beacon;
            //Beacon.Sensors.Add(this);
            Manhattan = XDistance + YDistance;
        }


        public Beacon Beacon;
        //public List<SensorScope> SensorScopes = new();

        public int XDistance => Math.Abs(X - Beacon.X);
        public int YDistance => Math.Abs(Y - Beacon.Y);
        public int Manhattan { get; private set; }

        public void FillSensorScope()
        {

            var xMin = X - Manhattan;
            var xMax = X + Manhattan;
            var counter = 0;

            var xDis = 0;
            var yMin = 0;
            var yMax = 0;
            //var yDis = 0;
            //var manDis = 0;
            for (int x = xMin; x <= xMax; x++)
            {
                xDis = Math.Abs(X - x);
                yMin = Y - Manhattan + xDis;
                yMax = Y + Manhattan - xDis;
                for (int y = yMin; y <= yMax; y++)
                {
                    counter++;

                    //yDis = Math.Abs(Y - y);
                    //manDis = xDis + yDis;
                    //if (manDis <= Manhattan)
                    //{
                    Surface.AddSensorScope(x, y);
                    //}
                }
            }
        }

        public int FillSensorScopeRow(int x, int max)
        {
            var xDis = Math.Abs(X - x);
            if (xDis > Manhattan)
                return 0;
            var yMin = Math.Max(0, Y - Manhattan);
            var yMax = Math.Min(Y + Manhattan, max);
            var countOnRow = 0;
            for (int y = yMin; y <= yMax; y++)
            {
                var yDis = Math.Abs(Y - y);
                var manDis = xDis + yDis;
                if (manDis <= Manhattan)
                {
                    countOnRow = Surface.AddSensorScope(x, y);
                    if (countOnRow > max)
                    {
                        break;
                    }
                }
            }
            return countOnRow;
        }

        public range GetSensorScopeRow(in int row, in int max)
        {
            var yDis = Math.Abs(Y - row);
            if (yDis > Manhattan)
            {
                return null;
            }
            var start = Math.Max(0, (X - (Manhattan - (yDis))));
            var end = Math.Min((X + (Manhattan - (yDis))), max);
            return new(start, end);
        }


        public void FillSensorScopeRow(int row, int max, HashSet<int> set)
        {
            var xDis = Math.Abs(X - row);
            if (xDis > Manhattan) return;
            var yMin = Math.Max(0, (Y - (Manhattan - (xDis))));
            var yMax = Math.Min((Y + (Manhattan - (xDis))), max);

            foreach (var y in Enumerable.Range(yMin, yMax - yMin + 1))
            {
                set.Add(y);
                if (set.Count > max)
                {
                    return;
                }
            };
        }

        public void FillSensorScope(int y)
        {
            var xMin = X - Manhattan;
            var xMax = X + Manhattan;
            for (int x = xMin; x <= xMax; x++)
            {
                var yDis = Math.Abs(Y - y);
                var xDis = Math.Abs(X - x);
                var manDis = xDis + yDis;
                if (manDis <= Manhattan)
                {
                    Surface.AddSensorScope(x, y);
                }

            }
        }

        public void FillSensorScope(int y, int max)
        {
            var xMin = Math.Max(0, X - Manhattan);
            var xMax = Math.Min(X + Manhattan, max);
            for (int x = xMin; x <= xMax; x++)
            {
                var yDis = Math.Abs(Y - y);
                var xDis = Math.Abs(X - x);
                var manDis = xDis + yDis;
                if (manDis <= Manhattan)
                {
                    Surface.AddSensorScope(x, y);
                }

            }
        }

        public override string ToString() => $"S{Number:d2} ({X},{Y}) [{Manhattan} > {XDistance},{YDistance}]";
    }
    class Beacon : SurfacePosition
    {
        public Beacon(int number, Surface surface, int x, int y) : base(number, surface, x, y)
        {
            Character = 'B';
        }
        //public List<Sensor> Sensors = new();
        public override string ToString() => $"B{Number:d2} ({X},{Y})";
    }

    //class SensorScope : SurfacePosition
    //{
    //    public SensorScope(Surface surface, int x, int y) : base(surface, x, y)
    //    {
    //        Character = '#';
    //    }
    //    //public List<Sensor> Sensors = new();
    //    public override string ToString() => $"Scope ({X},{Y})";
    //}

    class Surface
    {
        Dictionary<int, Dictionary<int, SurfacePosition>> _positions = new();
        public List<SurfacePosition> Positions = new();
        public List<Sensor> Sensors = new();
        public List<Beacon> Beacons = new();

        Dictionary<int, HashSet<int>> _sensorScopes = new();
        //public List<SensorScope> SensorScopes = new();

        public void ClearSensorScope()
        {
            _sensorScopes.Clear();
            GC.Collect();
        }

        public int MinX { get; private set; } = int.MaxValue;
        public int MinY { get; private set; } = int.MaxValue;
        public int MaxX { get; private set; } = int.MinValue;
        public int MaxY { get; private set; } = int.MinValue;

        public void Add(SurfacePosition position)
        {

            MinX = Math.Min(MinX, position.X); MinY = Math.Min(MinY, position.Y);
            MaxX = Math.Max(MaxX, position.X); MaxY = Math.Max(MaxY, position.Y);


            if (!_positions.ContainsKey(position.X))
                _positions.Add(position.X, new Dictionary<int, SurfacePosition>());

            if (!_positions[position.X].ContainsKey(position.Y))
            {
                _positions[position.X].Add(position.Y, position);
                Positions.Add(position);
                if (position is Sensor sensor)
                    Sensors.Add(sensor);
                if (position is Beacon beacon)
                    Beacons.Add(beacon);
            }
        }

        public SurfacePosition Get(int x, int y)
        {
            if (!_positions.ContainsKey(x)) return null;
            if (!_positions[x].ContainsKey(y)) return null;
            return _positions[x][y];
        }

        public int AddSensorScope(int x, int y)
        {
            MinX = Math.Min(MinX, x); MinY = Math.Min(MinY, y);
            MaxX = Math.Max(MaxX, x); MaxY = Math.Max(MaxY, y);

            if (!_sensorScopes.ContainsKey(x))
                _sensorScopes.Add(x, new HashSet<int>());

            _sensorScopes[x].Add(y);

            return _sensorScopes[x].Count;
            //if (!_sensorScopes[x].ContainsKey(sensorScope.Y))
            //{
            //    _sensorScopes[sensorScope.X].Add(sensorScope.Y, sensorScope);
            //    //SensorScopes.Add(sensorScope);
            //}
        }
        public bool HasSensorScope(int x, int y)
        {
            if (!_sensorScopes.ContainsKey(x)) return false;
            return _sensorScopes[x].Contains(y);
        }

        internal static Surface Parse(string[] lines)
        {
            var surface = new Surface();
            foreach (var line in lines)
            {
                var items = line.Split(':');
                var sensorPosition = items[0].Replace("Sensor at ", "").Replace("x=", "").Replace("y=", "").Split(',');
                var beaconPosition = items[1].Replace("closest beacon is at", "").Replace("x=", "").Replace("y=", "").Split(',');

                var beaconX = int.Parse(beaconPosition[0]);
                var beaconY = int.Parse(beaconPosition[1]);
                var beacon = (Beacon)surface.Get(beaconX, beaconY);
                if (beacon == null)
                {
                    beacon = new Beacon(surface.Beacons.Count + 1, surface, int.Parse(beaconPosition[0]), int.Parse(beaconPosition[1])); ;
                    surface.Add(beacon);
                }

                var sensor = new Sensor(surface.Sensors.Count + 1, surface, int.Parse(sensorPosition[0]), int.Parse(sensorPosition[1]), beacon);

                surface.Add(sensor);
            }

            return surface;
        }
    }

    public override object SolvePartOne(string[] lines)
    {
        //return null;
        var surface = Surface.Parse(lines);
        // Show(surface);

        if (lines.Length == 14)
            return GetScopeCountOnRow(surface, 10);
        else
            return GetScopeCountOnRow(surface, 2_000_000);
    }

    private static void Show(Surface surface)
    {
        foreach (var item in surface.Sensors)
        {
            item.FillSensorScope();
        }

        Console.WriteLine();
        for (int y = surface.MinY; y <= surface.MaxY; y++)
        {
            for (int x = surface.MinX; x <= surface.MaxX; x++)
            {
                var pos = surface.Get(x, y);
                if (pos == null) Console.Write('.');
                else Console.Write(pos.Character);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();


        for (int y = surface.MinY; y <= surface.MaxY; y++)
        {
            for (int x = surface.MinX; x <= surface.MaxX; x++)
            {
                var scope = surface.HasSensorScope(x, y);
                if (scope) Console.Write('#');
                else
                {
                    var pos = surface.Get(x, y);
                    if (pos == null) Console.Write(".");
                    else Console.Write(pos.Character);
                }
            }
            Console.WriteLine();
        }
    }

    private static int GetScopeCountOnRow(Surface surface, int row)
    {
        foreach (var sensor in surface.Sensors)
        {
            sensor.FillSensorScope(row);
        }

        if (row < surface.MinY) return 0;
        if (row > surface.MaxY) return 0;
        var counter = 0;
        for (int x = surface.MinX; x <= surface.MaxX; x++)
        {
            if (!surface.HasSensorScope(x, row)) continue;
            if (surface.Get(x, row) == null)
            {
                counter++;
            }
        }

        return counter;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var surface = Surface.Parse(lines);
        //Show(surface);

        if (lines.Length == 14)
           return SearchBeacon(surface, 20);
        else
            return SearchBeacon(surface, 4_000_000);
    }

    private long SearchBeacon(Surface surface, int max)
    {
        var rows = Enumerable.Range(0, max + 1);

        var mainRange = new range(0, max);
        var result = -1L;
        Parallel.ForEach(rows, row =>
        {
            if (row > max) return;

            var ranges = new List<range>();
            foreach (var sensor in surface.Sensors)
            {
                var range = sensor.GetSensorScopeRow(row, max);
                if (range != null) ranges.Add(range);
            }

            var covered = range.Cover(ranges, mainRange);
            if (covered.Item1)
            {
                return;
            }

            var set = new HashSet<int>();
            foreach (var range in covered.merged)
                for (int i = range.Start; i <= range.End; i++)
                {
                    set.Add(i);
                }

            if (set.Count <= max)
            {
                var x = GetScopeCountOnRow(surface, row, set, max);
                if (x != -1)
                {
                    result = (x * 4_000_000L) + row;
                    Console.WriteLine($"{row},{x} >> {result}");                    
                    return;
                }
            }
            Debug.WriteLine($"Row {row} finished ");
        });

        return result;
    }

    class range
    {
        public int Start { get; set; }
        public int End { get; set; }

        public range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override string ToString() => $"{{{Start},{End}}}";

        public static (bool, IEnumerable<range> merged) Cover(List<range> ranges, range other)
        {
            if (!ranges.Any()) return (false, ranges);
            var merged = Merge(ranges);
            //Console.WriteLine($"{string.Join(", ", merged)}");
            if (merged.Count() > 1) return (false, merged);
            var first = merged.First();
            return ((first.Start == other.Start && first.End == other.End), merged);
        }


        public static IEnumerable<range> Merge(List<range> ranges)
        {
            if (ranges.Count <= 1)
            {
                yield return ranges[0];

                yield break;
            }

            ranges.Sort((first, second) =>
            {
                if (first.Start == second.Start)
                {
                    return first.End - second.End;
                }
                return first.Start - second.Start;
            });

            var last = new range(ranges[0].Start, ranges[0].End);
            for (int i = 1; i < ranges.Count; i++)
            {
                if (last.End < ranges[i].Start)
                {
                    yield return last;
                    last = new range(ranges[i].Start, ranges[i].End);
                }
                else if (last.End < ranges[i].End)
                {
                    last.End = ranges[i].End;
                }
            }
            yield return last;
        }

    }

    private static int GetScopeCountOnRow(Surface surface, int row, HashSet<int> set, int max)
    {
        for (int x = 0; x <= max; x++)
        {
            var hasScope = set.Contains(x);

            if (!hasScope)
            {
                var SensorOrBeacon = surface.Get(row, x);
                if (SensorOrBeacon == null)
                {
                    return x;

                }
            }
        }

        return -1;

    }
    private static int GetScopeCountOnRow(Surface surface, int row, int max)
    {
        for (int y = 0; y <= max; y++)
        {
            var hasScope = surface.HasSensorScope(row, y);

            if (!hasScope)
            {
                var SensorOrBeacon = surface.Get(row, y);
                if (SensorOrBeacon == null)
                {
                    return y;

                }
            }
        }

        return -1;
    }
}
