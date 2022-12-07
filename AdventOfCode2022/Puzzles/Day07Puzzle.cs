using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(07)]
public class Day07Puzzle : Puzzle
{
    public override int DayNumber => 07;
    public override string Title => "No Space Left On Device";

    public override string[] SampleInput =>
        "$ cd /\r\n$ ls\r\ndir a\r\n14848514 b.txt\r\n8504156 c.dat\r\ndir d\r\n$ cd a\r\n$ ls\r\ndir e\r\n29116 f\r\n2557 g\r\n62596 h.lst\r\n$ cd e\r\n$ ls\r\n584 i\r\n$ cd ..\r\n$ cd ..\r\n$ cd d\r\n$ ls\r\n4060174 j\r\n8033020 d.log\r\n5626152 d.ext\r\n7214296 k"
        .ToLines();

    public override string PartOneSampleAnswer => "95437";
    public override string PartTwoSampleAnswer => "24933642";

    public override string PartOneActualAnswer => "1297159";
    public override string PartTwoActualAnswer => "3866390";
}
