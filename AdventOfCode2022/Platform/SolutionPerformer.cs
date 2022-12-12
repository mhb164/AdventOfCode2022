using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public interface ISolutionPerformerPlatform
    {
        ILogger Logger { get; }
        string[] GetActualInput(int dayNumber);

    }
    public partial class SolutionPerformer
    {
        private readonly ISolutionPerformerPlatform _platform;

        public SolutionPerformer(ISolutionPerformerPlatform platform, Puzzle puzzle, PuzzleSolution solution)
        {
            _platform = platform;

            Puzzle = puzzle;
            Solution = solution;

            if (Puzzle.DayNumber != Solution.DayNumber)
            {
                throw new ArgumentException($"DayNumber of Puzzle and solution don't match!");
            }

            ActualInput = _platform.GetActualInput(DayNumber);

            SolutionName = Solution.GetType().Name
                                   .Replace($"Day{DayNumber:d2}", "")
                                   .Replace($"Solution", "");

            LogPrefix = $"Day{DayNumber:d2} {SolutionName}";
        }

        private ILogger Logger => _platform.Logger;
        public Puzzle Puzzle { get; private set; }
        public PuzzleSolution Solution { get; private set; }

        public int DayNumber => Puzzle.DayNumber;
        public string Title => Puzzle.Title;
        public string[] SampleInput => Puzzle.SampleInput;
        public string PartOneSampleAnswer => Puzzle.PartOneSampleAnswer;
        public string PartTwoSampleAnswer => Puzzle.PartTwoSampleAnswer;
        public string[] ActualInput { get; private set; }
        public string PartOneActualAnswer => Puzzle.PartOneActualAnswer;
        public string PartTwoActualAnswer => Puzzle.PartTwoActualAnswer;

        public string SolutionName { get; private set; }
        public string LogPrefix { get; private set; }


        public void LogInfo(string message) => Logger?.Info($"{message}");
        public void LogNotice(string message) => Logger?.Notice($"{message}");
        public void LogError(string message) => Logger?.Error($"{message}");
        public void LogCritical(string message) => Logger?.Critical($"{message}");
        public void LogStartLine(string action) => LogInfo($"--- Day{DayNumber:d2}: {Title} - {SolutionName} ({action}) ---");
        public void LogEndLine() => LogInfo($" ");

        public void SampleTest(bool debuging)
        {
            LogStartLine("Sample Test");
            try
            {
                var answer = Solution.SolvePartOne(SampleInput)?.ToString();

                if (answer == PartOneSampleAnswer)
                {
                    LogNotice($"✔ Test part one passed by sample input ({answer})");
                }
                else
                {
                    LogError($"✘ Test part one failed by sample input! ({answer} != {PartOneSampleAnswer})");
                }
            }
            catch (Exception ex)
            {
                if (debuging)
                {
                    LogCritical($"❎ An exception occurred while testing part one by sample input!");
                    throw;
                }

                LogCritical($"❎ An exception occurred while testing part one by sample input! {Environment.NewLine}  {ex.Message}");
            }

            try
            {
                var answer = Solution.SolvePartTwo(SampleInput)?.ToString();

                if (answer == PartTwoSampleAnswer)
                {
                    LogNotice($"✔ Test part two passed by sample input ({answer})");
                }
                else
                {
                    LogError($"✘ Test part two failed by sample input! ({answer} != {PartTwoSampleAnswer})");
                }
            }
            catch (Exception ex)
            {
                if (debuging)
                {
                    LogCritical($"❎ An exception occurred while testing part two by sample input!");
                    throw;
                }

                LogCritical($"❎ An exception occurred while testing part two by sample input! {Environment.NewLine}  {ex.Message}");
            }
            LogEndLine();
        }

        public void ActualTest(bool debuging)
        {
            LogStartLine("Actual Test");
            try
            {
                var answer = Solution.SolvePartOne(ActualInput)?.ToString();

                if (answer == PartOneActualAnswer)
                {
                    LogNotice($"✔ Test part one passed by actual input ({answer})");
                }
                else
                {
                    LogError($"✘ Test part one failed by actual input! ({answer} != {PartOneActualAnswer})");
                }
            }
            catch (Exception ex)
            {
                if (debuging)
                {
                    LogCritical($"❎ An exception occurred while testing part one by actual input!");
                    throw;
                }

                LogCritical($"❎ An exception occurred while testing part one by actual input! {Environment.NewLine}  {ex.Message}");
            }

            try
            {
                var answer = Solution.SolvePartTwo(ActualInput)?.ToString();

                if (answer == PartTwoActualAnswer)
                {
                    LogNotice($"✔ Test part two passed by actual input ({answer})");
                }
                else
                {
                    LogError($"✘ Test part two failed by actual input! ({answer} != {PartTwoActualAnswer})");
                }
            }
            catch (Exception ex)
            {
                if (debuging)
                {
                    LogCritical($"❎ An exception occurred while testing part two by actual input!");
                    throw;
                }

                LogCritical($"❎ An exception occurred while testing part two by actual input! {Environment.NewLine}  {ex.Message}");
            }
            LogEndLine();
        }

        public string Solve(bool debuging)
        {
            LogStartLine("Solve");
            var partOneAnswer = default(string);
            try
            {
                partOneAnswer = Solution.SolvePartOne(ActualInput)?.ToString();
                LogInfo($"☕ Part one answer by actual input: {partOneAnswer}.");
            }
            catch (Exception ex)
            {
                if (debuging)
                {
                    LogCritical($"❎ An exception occurred while solving part one.");
                    throw;
                }

                LogCritical($"❎ An exception occurred while solving part one:{Environment.NewLine}  {ex.Message}");
                return string.Empty;
            }

            var partTwoAnswer = default(string);
            try
            {
                partTwoAnswer = Solution.SolvePartTwo(ActualInput)?.ToString();
                LogInfo($"☕ Part two answer by actual input: {partTwoAnswer}.");
            }
            catch (Exception ex)
            {
                if (debuging)
                {
                    LogCritical($"❎ An exception occurred while solving part two.");
                    throw;
                }

                LogCritical($"❎ An exception occurred while solving part two:{Environment.NewLine}  {ex.Message}");
                return string.Empty;
            }

            LogEndLine();
            return $"Part one: {partOneAnswer}, Part two: {partTwoAnswer}";
        }


        public void LogPartOne(string message) => LogInfo($"{LogPrefix} Part1> {message}");
        public void LogPartTwo(string message) => LogInfo($"{LogPrefix} Part2> {message}");


    }

    
}
