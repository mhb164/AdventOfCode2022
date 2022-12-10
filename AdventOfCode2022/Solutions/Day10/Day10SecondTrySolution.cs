using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(10)]
public class Day10SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 10;

    public override object SolvePartOne(in string[] inputLines)
    {
        var signalStrengths = new int[] { 20, 60, 100, 140, 180, 220 }.ToDictionary(x => x, y => 0);
        var afterCycleAction = (ProgramExecuter executer) =>
        {
            if (!signalStrengths.ContainsKey(executer.CycleCounter)) return;
            signalStrengths[executer.CycleCounter] = executer.SignalStrength;
        };

        new ProgramExecuter(new CPU(), Instruction.Parse(inputLines)).Execute(null, afterCycleAction);

        return signalStrengths.Values.Sum();
    }

    public override object SolvePartTwo(in string[] inputLines)
    {
        var crt = new CRT();
        var beforeCycleAction = (ProgramExecuter executer) =>
        {
            crt.Draw(executer.CPU.SignalRegister);
        };

        new ProgramExecuter(new CPU(), Instruction.Parse(inputLines)).Execute(beforeCycleAction, null);

        return crt.Output;
    }

    class ProgramExecuter
    {
        public readonly CPU CPU;
        public readonly List<Instruction> Instructions;

        public ProgramExecuter(CPU cpu, List<Instruction> instructions)
        {
            CPU = cpu;
            Instructions = instructions;
        }

        public int CycleCounter { get; private set; } = 1;
        public int SignalStrength => CycleCounter * CPU.SignalRegister;

        internal void Execute(Action<ProgramExecuter> beforeCycleAction, Action<ProgramExecuter> afterCycleAction)
        {
            foreach (var instruction in Instructions)
            {
                var finished = false;
                while (!finished)
                {
                    beforeCycleAction?.Invoke(this);
                    CycleCounter++;
                    finished = instruction.Execute(CPU);
                    afterCycleAction?.Invoke(this);
                }
            }
        }
    }

    abstract class Instruction
    {
        protected Instruction(int initialState)
        {
            State = initialState;
        }

        public int State { get; private set; }

        internal bool Execute(CPU cpu)
        {
            execute(cpu);
            State--;
            return State == 0;
        }

        protected virtual void execute(CPU cpu)
        {
            //Nothing
        }

        public static List<Instruction> Parse(string[] lines)
                 => InstructionParser.Parse(lines);
    }

    class NoopInstruction : Instruction
    {
        public NoopInstruction() : base(1)
        {
        }
        public override string ToString() => $"noop [State: {State}]";
    }

    class AddXInstruction : Instruction
    {
        public readonly int Value;
        public AddXInstruction(int value) : base(2)
        {
            Value = value;
        }
        protected override void execute(CPU cpu)
        {
            if (State == 2)
            {
                //Nothing
                return;
            }
            if (State == 1)
            {
                cpu.AddSignalRegister(Value);
            }
        }

        public override string ToString() => $"addx {Value} [State: {State}]";
    }

    static class InstructionParser
    {
        public static List<Instruction> Parse(string[] lines)
            => lines.Select(line => Parse(line.AsSpan()))
                    .ToList();

        private static Instruction Parse(ReadOnlySpan<char> line)
        {
            if (line.StartsWith("noop")) return new NoopInstruction();
            if (line.StartsWith("addx")) return new AddXInstruction(int.Parse(line[4..]));
            throw new ArgumentException($"{nameof(line)} argument is wrong ({line})");
        }
    }

    class CPU
    {
        public int SignalRegister { get; private set; } = 1;

        internal void AddSignalRegister(int value)
        {
            SignalRegister += value;
        }
    }

    class CRT
    {
        private readonly StringBuilder _output;
        public CRT()
        {
            _output = new StringBuilder();
            _output.AppendLine();
        }

        private int _position = 0;
        public string Output => _output.ToString();

        internal void Draw(int signalRegister)
        {
            var linePosition = _position % 40;
            var spritePosittion = signalRegister - linePosition + 1;

            if (spritePosittion == 0 || spritePosittion == 1 || spritePosittion == 2)
            {
                _output.Append('#');
            }
            else
            {
                _output.Append('.');
            }

            if (_position % 40 == 39)
                _output.AppendLine();

            _position++;
        }
    }



}
