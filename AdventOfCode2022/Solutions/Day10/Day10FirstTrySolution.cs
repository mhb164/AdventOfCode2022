using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(10)]
public class Day10FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 10;

    enum InsType { nope, addx }
    class Ins
    {
        public InsType Type;
        public int value;
        public int state;

        internal static Ins Parse(string line)
        {
            if (line.StartsWith("noop")) return new Ins() { Type = InsType.nope, state = 1 };
            else return new Ins() { Type = InsType.addx, value = int.Parse(line[4..]), state = 2 };
        }
    }

    class Cpu
    {
        public int X { get; private set; } = 1;
        public int Cycle { get; private set; } = 1;
        public readonly List<Ins> InsSet;
        int insIndex = 0;
        public string output = "\r\n";
        int CRTPosition = 0;

        public Cpu(List<Ins> insSet)
        {
            InsSet = insSet;
        }

        internal bool DoCycle()
        {
            if (insIndex >= InsSet.Count) return false;
            var ins = InsSet[insIndex];
            if (ins.state == 0)
            {
                insIndex++;
                if (insIndex >= InsSet.Count) return false;
                ins = InsSet[insIndex];
            }

            if (ins.Type == InsType.nope)
            {
                DrawOnCRT();
                Cycle++;
                ins.state = 0;
            }
            else //if (ins.Type == InsType.addx)
            {
                if (ins.state == 2)
                {
                    DrawOnCRT();
                    Cycle++;
                    ins.state = 1;
                }
                else
                {
                    DrawOnCRT();
                    Cycle++;
                    X += ins.value;
                    ins.state = 0;
                }
            }
            return true;
        }

        private void DrawOnCRT()
        {
            var p = CRTPosition % 40;
            var dd = X - p + 1;
            if (dd == 0 || dd == 1 || dd == 2)
                output += "#";
            else
                output += ".";
            if (CRTPosition % 40 == 39)
                output += "\r\n";

            CRTPosition++;
        }
    }
    public override object SolvePartOne(string[] lines)
    {
        var result = string.Empty;
        var set = lines.Select(x => Ins.Parse(x)).ToList();
        var cpu = new Cpu(set);

        var targets = new List<int>() { 20, 60, 100, 140, 180, 220 };
        var targetssignals = new List<(int, int, int)>();
        while (cpu.DoCycle())
        {
            if (targets.Contains(cpu.Cycle))
            {
                targetssignals.Add(new(cpu.Cycle * cpu.X, cpu.Cycle, cpu.X));
            }
        }

        return targetssignals.Sum(x => x.Item1);
    }

    public override object SolvePartTwo(string[] lines)
    {
        var result = string.Empty;
        var set = lines.Select(x => Ins.Parse(x)).ToList();
        var cpu = new Cpu(set);

        while (cpu.DoCycle()) ;

        return cpu.output;
    }

}
