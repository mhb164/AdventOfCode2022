using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

public class Day01Puzzle : Puzzle
{
    public override int DayNumber => 01;
    public override string[] SampleInput =>
        "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000"
        .ToLines();

    public override string PartOneSampleAnswer => "24000";
    public override string PartTwoSampleAnswer => "45000";

    public override string PartOneActualAnswer => "69883";
    public override string PartTwoActualAnswer => "207576";
}
