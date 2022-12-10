using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(09)]
public class Day09SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 09;

    public override object SolvePartOne(string[] lines) => Solve(lines, 1);
    public override object SolvePartTwo(string[] lines) => Solve(lines, 9);

    private static object Solve(string[] lines, int _knotsCount)
    {
        var rope = new Rope(_knotsCount);
        foreach (var motion in lines.Select(line => Motion.Parse(line.AsSpan())))
        {
            rope.Do(motion);
        }
        return rope.VisitedCount;
    }

    enum MotionDirection { Up = 'U', Right = 'R', Down = 'D', Left = 'L', }

    class Motion
    {
        public readonly MotionDirection Direction;
        public readonly int Steps;

        public Motion(char direction, int steps)
        {
            Direction = (MotionDirection)direction;
            Steps = steps;
        }

        internal static Motion Parse(ReadOnlySpan<char> input) => new(input[0], int.Parse(input[1..]));

        public override string ToString() => $"{Steps} steps to {Direction}";
    }

    class Rope
    {
        private readonly Position _head = new();
        private readonly Knot[] _knots;
        private readonly HashSet<(int, int)> _visited = new();

        public Rope(int _knotsCount)
        {
            _knots = Enumerable.Range(0, _knotsCount)
                               .Select(x => new Knot(x + 1))
                               .ToArray();
        }

        private Position Tail => _knots[_knots.Length - 1].Position;

        public int VisitedCount => _visited.Count;

        public void Do(Motion motion)
        {
            for (int step = 0; step < motion.Steps; step++)
            {
                _head.Move(motion.Direction);
                _knots[0].Follow(_head);
                for (int i = 1; i < _knots.Length; i++)
                {
                    _knots[i].Follow(_knots[i - 1].Position);
                }
                _visited.Add((Tail.Row, Tail.Column));
            }
        }

        class Knot
        {
            public readonly Position Position = new();
            public readonly int Number;

            public Knot(int number)
            {
                Number = number;
            }

            public override string ToString() => $"Knot[{Number}] {Position}";

            internal void Follow(Position head)
            {
                var rowDiff = head.Row - Position.Row;
                var colDiff = head.Column - Position.Column;

                if (rowDiff > 1)
                {
                    Position.MoveUp();                    
                    if (colDiff > 0)
                    {
                        Position.MoveRight();
                    }
                    else if (colDiff < 0)
                    {
                        Position.MoveLeft();
                    }
                }
                else if (rowDiff < -1)
                {
                    Position.MoveDown();
                    if (colDiff > 0)
                    {
                        Position.MoveRight();
                    }
                    else if (colDiff < 0)
                    {
                        Position.MoveLeft();
                    }
                }
                else if (colDiff > 1)
                {
                    Position.MoveRight();
                    if (rowDiff > 0)
                    {
                        Position.MoveUp();
                    }
                    else if (rowDiff < 0)
                    {
                        Position.MoveDown();
                    }
                }
                else if (colDiff < -1)
                {
                    Position.MoveLeft();
                    if (rowDiff > 0)
                    {
                        Position.MoveUp();
                    }
                    else if (rowDiff < 0)
                    {
                        Position.MoveDown();
                    }
                }
            }
        }

        class Position
        {
            public int Row { get; internal set; } = 0;
            public int Column { get; internal set; } = 0;

            internal void Move(MotionDirection direction)
            {
                switch (direction)
                {
                    case MotionDirection.Up: MoveUp(); break;
                    case MotionDirection.Right: MoveRight(); break;
                    case MotionDirection.Down: MoveDown(); break;
                    case MotionDirection.Left:  MoveLeft(); break;
                    default: break;
                }
            }

            internal void MoveUp() => Row++;
            internal void MoveRight() => Column++;
            internal void MoveDown() => Row--;
            internal void MoveLeft() => Column--;

            public override string ToString() => $"({Row}, {Column})";
        }
    }
}
