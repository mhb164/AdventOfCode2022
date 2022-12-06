using System;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(03)]
public class Day03SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 03;
    public override string SolvePartOne(in string[] inputLines)
    {
        var sum = 0;
        for (int i = 0; i < inputLines.Length; i++)
        {
            var inputLine = inputLines[i].AsSpan();
            var middel = inputLine.Length / 2;

            var first = inputLine[..middel];
            var second = inputLine[middel..];

            var badge = ' ';
            foreach (var item in first)
                if (second.Contains(item))
                {
                    badge = item;
                    break;
                }

            sum += BadgeToPeriority(in badge);
        }
        return sum.ToString();
    }

    public override string SolvePartTwo(in string[] inputLines)
    {
        var sum = 0;
        for (int i = 0; i < inputLines.Length / 3; i++)
        {
            var first = inputLines[i * 3 + 0];
            var second = inputLines[i * 3 + 1];
            var third = inputLines[i * 3 + 2];

            var badge = ' ';
            foreach (var item in first)
                if (second.Contains(item)&& third.Contains(item))
                {
                    badge = item;
                    break;
                }

            sum += BadgeToPeriority(in badge);
        }
        return sum.ToString();
    }

    private static int BadgeToPeriority(in char badge)
    {       
        if (badge > 97) return (badge - 'a') + 1;
        else return (badge - 'A') + 27;
    }


}
