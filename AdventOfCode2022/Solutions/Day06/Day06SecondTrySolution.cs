using System.Net.Sockets;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(06)]
public class Day06SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 06;
    public override object SolvePartOne(string[] lines)
        => ReportStartOfPacket(lines[0].AsSpan(), 4);

    public override object SolvePartTwo(string[] lines)
        => ReportStartOfPacket(lines[0].AsSpan(), 14);

    private static object ReportStartOfPacket(ReadOnlySpan<char> datastream, int length)
    {
        for (int i = 0; i < datastream.Length - length; i++)
        {
            if (HasDuplicate(datastream[i..(i + length)]))
                continue;
            return (i + length);
        }
        return string.Empty;
    }

    private static bool HasDuplicate(ReadOnlySpan<char> slice)
    {
        for (int i = 0; i < slice.Length - 1; i++)
            for (int j = i + 1; j < slice.Length; j++)
            {
                if (slice[i] == slice[j])
                    return true;
            }

        return false;
    }

  
}
