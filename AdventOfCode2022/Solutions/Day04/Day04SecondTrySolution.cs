using System.Collections.Concurrent;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(04)]
public class Day04SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 04;
    public override object SolvePartOne(string[] lines)
    {
        var pairs = 0;
        Parallel.ForEach(lines, line =>
        {
            var sections = GetSections(line.AsSpan());

            if ((sections.elf1Start <= sections.elf2Start && sections.elf2End <= sections.elf1End) ||
                (sections.elf2Start <= sections.elf1Start && sections.elf1End <= sections.elf2End))
            {
                Interlocked.Increment(ref pairs);
            }
        });

        return pairs;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var pairs = 0;
        Parallel.ForEach(lines, line =>
        {
            var sections = GetSections(line.AsSpan());

            if (sections.elf1End >= sections.elf2Start && sections.elf2End >= sections.elf1Start)
            {
                Interlocked.Increment(ref pairs);
            }
        });

        return pairs;
    }

    private const char CammaChar = ',';
    private const char DashChar = '-';

    private static (int elf1Start, int elf1End, int elf2Start, int elf2End) GetSections(ReadOnlySpan<char> lineAsSpan)
    {
        var cammaIndex = lineAsSpan.IndexOf(CammaChar);
        var elf1Sections = lineAsSpan[..cammaIndex];
        var elf2Sections = lineAsSpan[(cammaIndex + 1)..];

        var elf1SectionsDashIndex = elf1Sections.IndexOf(DashChar);
        var elf1Start = int.Parse(elf1Sections[..elf1SectionsDashIndex]);
        var elf1End = int.Parse(elf1Sections[(elf1SectionsDashIndex + 1)..]);

        var elf2SectionsDashIndex = elf2Sections.IndexOf(DashChar);
        var elf2Start = int.Parse(elf2Sections[..elf2SectionsDashIndex]);
        var elf2End = int.Parse(elf2Sections[(elf2SectionsDashIndex + 1)..]);

        return (elf1Start, elf1End, elf2Start, elf2End);
    }
}
