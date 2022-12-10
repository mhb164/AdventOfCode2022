using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(05)]
public class Day05ThirdTrySolution : PuzzleSolution
{
    public override int DayNumber => 05;

    public override object SolvePartOne(string[] lines) => Solve<CrateMover9000>(lines);

    public override object SolvePartTwo(string[] lines) => Solve<CrateMover9001>(lines);

    private static object Solve<T>(string[] lines)
        where T : Crane
    {
        var crane = (Activator.CreateInstance(typeof(T)) as Crane)
                    .Initial(lines, out var i);

        for (; i < lines.Length; i++)
        {
            crane.ReArrange(lines[i].AsSpan());
        }

        return crane;
    }



    private abstract class Crane
    {
        private readonly List<Stack<char>> _stacks = new();

        public Crane Initial(string[] lines, out int index) => Initial(this, lines, out index);

        private static Crane Initial(Crane crane, string[] lines, out int index)
        {
            List<List<char>> list = new();

            for (index = 0; index < lines.Length; index++)
            {
                if (lines[index].StartsWith(" 1"))
                {
                    break;
                }
                 
                var line = lines[index].AsSpan();
                for (int i = 0; (i * 4) < line.Length; i++)
                {
                    var caret = line[(i * 4)..((i * 4) + 3)];
                    if (list.Count <= i)
                    {
                        list.Add(new List<char>());
                    }

                    if (caret[1] != ' ')
                    {
                        list[i].Add(caret[1]);
                    }
                }
            }

            index += 2;
            for (int i = 0; i < list.Count; i++)
            {
                crane._stacks.Add(new Stack<char>());

                for (int j = list[i].Count - 1; j >= 0; j--)
                {
                    crane._stacks[i].Push(list[i][j]);
                }
            }

            return crane;
        }

        private static (int From, int To, int Count) ToProcedure(ReadOnlySpan<char> line)
        {
            var fromIndex = line.IndexOf('f');// from
            var toIndex = line.IndexOf('t');// to

            return (From: int.Parse(line[(fromIndex + 4)..toIndex]),
                    To: int.Parse(line[(toIndex + 2)..]),
                    Count: int.Parse(line[4..fromIndex]));
        }

        protected Stack<char> GetStack(int number) => _stacks[number - 1];

        public void ReArrange(ReadOnlySpan<char> line)
        {
            var procedure = ToProcedure(line);
            ReArrange(procedure.From, procedure.To, procedure.Count);
        }

        public void ReArrange(int from, int to, int count) => ReArrange(GetStack(from), GetStack(to), count);
        protected abstract void ReArrange(Stack<char> from, Stack<char> to, int count);


        public override string ToString()
            => string.Join(null, _stacks.Where(x => x.Count > 0).Select(x => x.Peek()));
    }

    private class CrateMover9000 : Crane
    {
        protected override void ReArrange(Stack<char> from, Stack<char> to, int count)
        {
            for (int c = 0; c < count; c++)
            {
                to.Push(from.Pop());
            }
        }
    }

    private class CrateMover9001 : Crane
    {
        protected override void ReArrange(Stack<char> from, Stack<char> to, int count)
        {
            var carets = new char[count];
            for (int c = 0; c < carets.Length; c++)
            {
                carets[carets.Length - 1 - c] = from.Pop();
            }

            for (int c = 0; c < carets.Length; c++)
            {
                to.Push(carets[c]);
            }
        }
    }
}