using System.Net.Sockets;

namespace AdventOfCode2022.Solutions;

public class Day06FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 06;
    public override string SolvePartOne(in string[] inputLines)
        => ReportStartOfPacket(inputLines[0].AsSpan(), 4);

    public override string SolvePartTwo(in string[] inputLines)
        => ReportStartOfPacket(inputLines[0].AsSpan(), 14);

    private static string ReportStartOfPacket(ReadOnlySpan<char> datastream, int length)
    {
        for (int i = 0; i < datastream.Length - length; i++)
        {
            if (datastream[i..(i + length)].ToArray().Distinct().Count() == length)
                return (i + length).ToString();
        }
        return string.Empty;
    }

}
