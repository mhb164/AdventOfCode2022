using System.Collections.Generic;
using System.Security.Claims;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(12)]
public class Day12FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 12;

    const char StartChar = 'S';//==a
    const char BestSignal = 'E';//==z

    class Map
    {
        public readonly List<MapItem> Items = new();
        public readonly Dictionary<int, Dictionary<int, MapItem>> dic = new();

        public MapItem Start { get; private set; }
        public MapItem BestSignal { get; private set; }

        internal static Map Parse(string[] lines)
        {
            var map = new Map();
            for (int x = 0; x < lines.Length; x++)
                for (int y = 0; y < lines[x].Length; y++)
                {
                    var item = new MapItem(map, x, y, lines[x][y]);
                    map.Add(item);

                }

            map.Initial();
            return map;
        }

        private void Add(MapItem item)
        {
            Items.Add(item);
            if (!dic.ContainsKey(item.X))
                dic.Add(item.X, new Dictionary<int, MapItem>());
            if (!dic[item.X].ContainsKey(item.Y))
                dic[item.X].Add(item.Y, item);

            if (item.IsStart)
            {
                Start = item;
            }
            else if (item.IsBestSignal)
            {
                BestSignal = item;
            }
        }

        private void Initial()
        {
            foreach (var item in Items)
            {
                item.Top = GetItem(item.X - 1, item.Y);
                item.Right = GetItem(item.X, item.Y + 1);
                item.Down = GetItem(item.X + 1, item.Y);
                item.Left = GetItem(item.X, item.Y - 1);

                item.Around = new List<MapItem>();
                if (item.Top is not null) item.Around.Add(item.Top);
                if (item.Right is not null) item.Around.Add(item.Right);
                if (item.Down is not null) item.Around.Add(item.Down);
                if (item.Left is not null) item.Around.Add(item.Left);
            }
        }

        private MapItem GetItem(int x, int y)
        {
            if (!dic.ContainsKey(x)) return null;
            if (!dic[x].ContainsKey(y)) return null;
            return dic[x][y];
        }
    }

    class MapItem
    {
        public readonly Map Map;
        public readonly int X;
        public readonly int Y;

        public readonly char Char;
        public readonly int Height;
        public readonly bool IsBestSignal;
        public readonly bool IsStart;

        public MapItem Top { get; set; }
        public MapItem Right { get; set; }
        public MapItem Down { get; set; }
        public MapItem Left { get; set; }
        public List<MapItem> Around;

        public override string ToString() => $"[{X},{Y}]";
        //public override string ToString() => $"[{X},{Y}] {Char}";
        public MapItem(Map map, int x, int y, char @char)
        {
            Map = map;
            X = x;
            Y = y;
            Char = @char;
            if (Char == StartChar)
            {
                Height = 'a' - 'a';
                IsStart = true;
            }
            else if (Char == BestSignal)
            {
                Height = 'z' - 'a';
                IsBestSignal = true;
            }
            else
            {
                Height = @char - 'a';
            }
        }

       
    }

    public override object SolvePartOne(string[] lines)
    {
        var map = Map.Parse(lines);
        var climbing = ClimbUp(map.Start);
        return climbing.Count;
    }

    private List<MapItem> ClimbUp(MapItem item)
    {
        var queue = new Queue<List<MapItem>>();
        queue.Enqueue(new List<MapItem>() { item });
        var visited = new List<MapItem>() { item };
        while (queue.Count > 0)
        {
            var climbing = queue.Dequeue();
            var lastItem = climbing.Last();
            //Console.WriteLine(climbing);
            //Console.WriteLine(string.Join(", ", climbing.Passed.Select(x => x.Char)));
            if (lastItem.IsBestSignal)
            {
                return climbing;
            }


            foreach (var another in lastItem.Around)
            {
                if (visited.Contains(another)) continue;

                if (lastItem.Height >= another.Height - 1)                
                {

                    if (another.IsBestSignal)
                    {
                        return climbing;
                    }

                    visited.Add(another);
                    var newclimbing = climbing.ToList();
                    newclimbing.Add(another);
                    queue.Enqueue(newclimbing);
                }
            }

        }

        return null;
    }
    
    public override object SolvePartTwo(string[] lines)
    {
        var map = Map.Parse(lines);
        var starts = map.Items.Where(x => x.Char == 'a').ToList();
        starts.Add(map.Start);

        var climbings = starts.Select(x => ClimbUp(x)).Where(x => x != null).ToList();
        var minClimbing = climbings.MinBy(x => x.Count);
        return minClimbing?.Count;
    }

}
