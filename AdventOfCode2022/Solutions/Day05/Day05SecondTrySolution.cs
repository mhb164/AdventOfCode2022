﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(05)]
public class Day05SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 05;

    public override object SolvePartOne(in string[] inputLines)
    {
        var stacks = ToStacks(in inputLines, out var i);

        for (; i < inputLines.Length; i++)
        {
            var procedure = ToProcedure(inputLines[i].AsSpan());

            var fromStack = stacks[procedure.From - 1];
            var toStack = stacks[procedure.To - 1];
            for (int c = 0; c < procedure.Count; c++)
            {
                toStack.Push(fromStack.Pop());
            }
        }

        var cartsOnTop = stacks.Where(x => x.Count > 0)
                               .Select(x => x.Peek());

        return string.Join(null, cartsOnTop);
    }


    public override object SolvePartTwo(in string[] inputLines)
    {
        var stacks = ToStacks(in inputLines, out var i);

        for (; i < inputLines.Length; i++)
        {
            var procedure = ToProcedure(inputLines[i].AsSpan());
            var fromStack = stacks[procedure.From - 1];
            var toStack = stacks[procedure.To - 1];

            var carets = new char[procedure.Count];
            for (int c = 0; c < carets.Length; c++)
            {
                carets[carets.Length - 1 - c] = fromStack.Pop();
            }

            for (int c = 0; c < carets.Length; c++)
            {
                toStack.Push(carets[c]);
            }

        }

        var cartsOnTop = stacks.Where(x => x.Count > 0)
                              .Select(x => x.Peek());
        return string.Join(null, cartsOnTop);
    }

    private static (int From, int To, int Count) ToProcedure(ReadOnlySpan<char> inputLine)
    {
        var fromIndex = inputLine.IndexOf('f');// from
        var toIndex = inputLine.IndexOf('t');// to

        return (From: int.Parse(inputLine[(fromIndex + 4)..toIndex]),
                To: int.Parse(inputLine[(toIndex + 2)..]),
                Count: int.Parse(inputLine[4..fromIndex]));
    }

    private static Stack<char>[] ToStacks(in string[] inputLines, out int index)
    {
        List<List<char>> list = new List<List<char>>();

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
        var stacks = new Stack<char>[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            stacks[i] = new Stack<char>();

            for (int j = list[i].Count - 1; j >= 0; j--)
            {
                stacks[i].Push(list[i][j]);
            }
        }

        return stacks;
    }
}
