using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(14)]
public class Day14FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 14;


    class CavePoint
    {
        public int X;
        public int Y;
        public char C;

        public CavePoint(int x, int y, char c)
        {
            X = x;
            Y = y;
            C = c;
        }

        public override string ToString() => $"({X},{Y}) {C}";
    }
    class Cave
    {
        public CavePoint SandSource;
        public List<CavePoint> ClosedPoints = new List<CavePoint>();
        public Dictionary<int, Dictionary<int, CavePoint>> _closedPoints = new();
        public int MaxY { get; private set; } = 0;
        public int Floor => MaxY + 2;
        public Cave(int x, int y)
        {
            SandSource = new CavePoint(x, y, '.');
            AddClosedPoint(SandSource, true);
        }
        internal CavePoint AddClosedPoint(int x, int y, char c)
        {
            var point = new CavePoint(x, y, c);
            AddClosedPoint(point, true);
            if (c == '.' || c == '#')
                MaxY = Math.Max(MaxY, point.Y);
            return point;
        }

        private void AddClosedPoint(CavePoint point, bool addToList)
        {
            if (!_closedPoints.ContainsKey(point.X))
                _closedPoints.Add(point.X, new Dictionary<int, CavePoint>());

            if (!_closedPoints[point.X].ContainsKey(point.Y))
            {
                if (addToList)
                {

                    ClosedPoints.Add(point);
                }
                _closedPoints[point.X].Add(point.Y, point);
            }
        }

        internal bool MoveDown(CavePoint sand, Func<CavePoint, bool> stopAction)
        {
            if (stopAction.Invoke(sand)) return false;
            if (Move(sand, sand.X, sand.Y + 1)) return true; //down
            if (Move(sand, sand.X - 1, sand.Y + 1)) return true; //down-left
            if (Move(sand, sand.X + 1, sand.Y + 1)) return true; //down-right
            return false;
        }

        private bool Move(CavePoint sand, int x, int y)
        {
            var otherPoint = Get(x, y);
            if (otherPoint is not null) return false;

            Remove(sand.X, sand.Y, false);
            sand.X = x; sand.Y = y;
            AddClosedPoint(sand, false);
            return true;
        }

        private void Remove(int x, int y, bool removeFromList)
        {
            if (removeFromList)
            {
                var existed = Get(x, y);
                if (existed is null) return;
                _closedPoints[x].Remove(y);
                ClosedPoints.Remove(existed);
            }
            else
            {
                _closedPoints[x].Remove(y);
            }
        }

        private CavePoint Get(int x, int y)
        {
            if (!_closedPoints.ContainsKey(x)) return null;
            if (!_closedPoints[x].ContainsKey(y)) return null;
            return _closedPoints[x][y];
        }
    }
    public override object SolvePartOne(string[] lines)
    {
        var cave = ParseCave(lines);

        var conter = 0;
        while (true)
        {
            conter++;
            var sand = cave.AddClosedPoint(500, 0, 'o');
            while (cave.MoveDown(sand, (CavePoint sand) => sand.Y + 1 > cave.MaxY))
            {
                //Console.WriteLine(sand.ToString());   
            }

            if (cave.MaxY < sand.Y)
            {
                break;
            }
        }

        return conter - 1;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var cave = ParseCave(lines);
        var conter = 0;
        while (true)
        {
            conter++;
            var sand = cave.AddClosedPoint(500, 0, 'o');
            while (cave.MoveDown(sand, (CavePoint sand) => sand.Y + 1 >= cave.Floor))
            {
                //Console.WriteLine(sand.ToString());   
            }


            if (sand.X == 500 && sand.Y == 0)
            {
                break;
            }
        }

        return conter;
    }

    private static Cave ParseCave(string[] lines)
    {
        var cave = new Cave(500, 0);
        foreach (var line in lines)
        {
            var steps = line.Split("->");
            for (int i = 0; i < steps.Length - 1; i++)
            {
                var x1 = int.Parse(steps[i].Split(',')[0]);
                var y1 = int.Parse(steps[i].Split(',')[1]);

                var x2 = int.Parse(steps[i + 1].Split(',')[0]);
                var y2 = int.Parse(steps[i + 1].Split(',')[1]);

                if (x1 == x2)
                {
                    var min = Math.Min(y1, y2);
                    var max = Math.Max(y1, y2);
                    for (int y = min; y <= max; y++)
                    {
                        cave.AddClosedPoint(x1, y, '#');
                    }
                }
                else if (y1 == y2)
                {
                    var min = Math.Min(x1, x2);
                    var max = Math.Max(x1, x2);
                    for (int x = min; x <= max; x++)
                    {
                        cave.AddClosedPoint(x, y1, '#');
                    }
                }
            }
        }
        return cave;
    }
}
