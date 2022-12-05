using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AdventOfCode2022.Solutions;

public class Day05ThirdTrySolution : PuzzleSolution
{
    public override int DayNumber => 05;

    public override string SolvePartOne(in string[] inputLines) => Solve<CrateMover9000>(inputLines);

    public override string SolvePartTwo(in string[] inputLines) => Solve<CrateMover9001>(inputLines);

    private static string Solve<T>(in string[] inputLines)
        where T : Crane
    {
        var crane = (Activator.CreateInstance(typeof(T)) as Crane)
                    .Initial(in inputLines, out var i);

        for (; i < inputLines.Length; i++)
        {
            crane.ReArrange(inputLines[i].AsSpan());
        }

        return crane.ToString();
    }



    private abstract class Crane
    {
        private readonly List<Stack<char>> _stacks = new();

        public Crane Initial(in string[] inputLines, out int index) => Initial(this, in inputLines, out index);

        private static Crane Initial(Crane crane, in string[] inputLines, out int index)
        {
            List<List<char>> list = new();

            for (index = 0; index < inputLines.Length; index++)
            {
                if (inputLines[index].StartsWith(" 1"))
                {
                    break;
                }

                var inputLine = inputLines[index].AsSpan();
                for (int i = 0; (i * 4) < inputLine.Length; i++)
                {
                    var caret = inputLine[(i * 4)..((i * 4) + 3)];
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

        private static (int From, int To, int Count) ToProcedure(ReadOnlySpan<char> inputLine)
        {
            var fromIndex = inputLine.IndexOf('f');// from
            var toIndex = inputLine.IndexOf('t');// to

            return (From: int.Parse(inputLine[(fromIndex + 4)..toIndex]),
                    To: int.Parse(inputLine[(toIndex + 2)..]),
                    Count: int.Parse(inputLine[4..fromIndex]));
        }

        protected Stack<char> GetStack(int number) => _stacks[number - 1];

        public void ReArrange(ReadOnlySpan<char> inputLine)
        {
            var procedure = ToProcedure(inputLine);
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