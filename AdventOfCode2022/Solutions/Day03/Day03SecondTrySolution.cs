using System;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(03)]
public class Day03SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 03;
    public override object SolvePartOne(string[] lines)
    {
        var sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].AsSpan();
            var middel = line.Length / 2;

            var first = line[..middel];
            var second = line[middel..];

            var badge = ' ';
            foreach (var item in first)
                if (second.Contains(item))
                {
                    badge = item;
                    break;
                }

            sum += BadgeToPeriority(in badge);
        }
        return sum;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var sum = 0;
        for (int i = 0; i < lines.Length / 3; i++)
        {
            var first = lines[i * 3 + 0];
            var second = lines[i * 3 + 1];
            var third = lines[i * 3 + 2];

            var badge = ' ';
            foreach (var item in first)
                if (second.Contains(item)&& third.Contains(item))
                {
                    badge = item;
                    break;
                }

            sum += BadgeToPeriority(in badge);
        }
        return sum;
    }

    private static int BadgeToPeriority(in char badge)
    {       
        if (badge > 97) return (badge - 'a') + 1;
        else return (badge - 'A') + 27;
    }


}
