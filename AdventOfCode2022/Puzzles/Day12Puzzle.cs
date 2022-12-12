using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(12)]
public class Day12Puzzle : Puzzle
{
    public override int DayNumber => 12;
    public override string Title => "Hill Climbing Algorithm";

    public override string[] SampleInput =>
        "Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi"
        .ToLines();

    public override string PartOneSampleAnswer => "31";
    public override string PartTwoSampleAnswer => "29";

    public override string PartOneActualAnswer => "449";
    public override string PartTwoActualAnswer => "443";
}
