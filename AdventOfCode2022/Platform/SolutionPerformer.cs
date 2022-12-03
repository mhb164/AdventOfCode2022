using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public interface ISolutionPerformer
    {
        public abstract Puzzle Puzzle { get; }
        public PuzzleSolution Solution { get; }
        public string[] ActualInput { get; }
    }

    public sealed class SolutionPerformer<TPuzzle, TPuzzleSolution> : ISolutionPerformer
        where TPuzzle : Puzzle
        where TPuzzleSolution : PuzzleSolution
    {
        private readonly ILogger _logger;

        public SolutionPerformer(ILogger logger)
        {
            _logger = logger;

            Puzzle = Activator.CreateInstance(typeof(TPuzzle)) as Puzzle;
            Solution = Activator.CreateInstance(typeof(TPuzzleSolution)) as PuzzleSolution;

            if (Puzzle.DayNumber != Solution.DayNumber)
            {
                throw new ArgumentException($"DayNumber of Puzzle and solution don't match!");
            }

            ActualInput = File.ReadAllLines(@$"ActualInputs\Day{DayNumber:d2}.txt");

            SolutionName = Solution.GetType().Name
                                   .Replace($"Day{DayNumber:d2}", "")
                                   .Replace($"Solution", "");

            LogPrefix = $"Day{DayNumber:d2} {SolutionName}";
        }

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


        public void LogInfo(string message) => _logger?.Info($"{message}");
        public void LogNotice(string message) => _logger?.Notice($"{message}");
        public void LogWarning(string message) => _logger?.Warning($"{message}");
        public void LogError(string message) => _logger?.Error($"{message}");
        public void LogStartLine(string action) => LogInfo($"--- Day{DayNumber:d2}: {Title} - {SolutionName} ({action}) ---");
        public void LogEndLine() => LogInfo($" ");

        public void SampleTest()
        {
            LogStartLine("Sample Test");
            try
            {
                var answer = Solution.SolvePartOne(SampleInput).Trim();

                if (answer == PartOneSampleAnswer)
                {
                    LogNotice($"✔ Test part one passed by sample input ({answer})");
                }
                else
                {
                    LogWarning($"✘ Test part one failed by sample input! ({answer} != {PartOneSampleAnswer})");
                }
            }
            catch (Exception ex)
            {
                LogError($"❎ An exception occurred while testing part one by sample input! {Environment.NewLine}  {ex.Message}");
            }

            try
            {
                var answer = Solution.SolvePartTwo(SampleInput).Trim();

                if (answer == PartTwoSampleAnswer)
                {
                    LogNotice($"✔ Test part two passed by sample input ({answer})");
                }
                else
                {
                    LogWarning($"✘ Test part two failed by sample input! ({answer} != {PartTwoSampleAnswer})");
                }
            }
            catch (Exception ex)
            {
                LogError($"❎ An exception occurred while testing part two by sample input! {Environment.NewLine}  {ex.Message}");
            }
            LogEndLine();
        }

        public void ActualTest()
        {
            LogStartLine("Actual Test");
            try
            {
                var answer = Solution.SolvePartOne(ActualInput).Trim();

                if (answer == PartOneActualAnswer)
                {
                    LogNotice($"✔ Test part one passed by actual input ({answer})");
                }
                else
                {
                    LogWarning($"✘ Test part one failed by actual input! ({answer} != {PartOneActualAnswer})");
                }
            }
            catch (Exception ex)
            {
                LogError($"❎ An exception occurred while testing part one by actual input! {Environment.NewLine}  {ex.Message}");
            }

            try
            {
                var answer = Solution.SolvePartTwo(ActualInput).Trim();

                if (answer == PartTwoActualAnswer)
                {
                    LogNotice($"✔ Test part two passed by actual input ({answer})");
                }
                else
                {
                    LogWarning($"✘ Test part two failed by actual input! ({answer} != {PartTwoActualAnswer})");
                }
            }
            catch (Exception ex)
            {
                LogError($"❎ An exception occurred while testing part two by actual input! {Environment.NewLine}  {ex.Message}");
            }
            LogEndLine();
        }

        public string Solve()
        {
            LogStartLine("Solve");
            var partOneAnswer = default(string);
            try
            {
                partOneAnswer = Solution.SolvePartOne(ActualInput);
                LogNotice($"✔ Part one answer by actual input: {partOneAnswer}.");
            }
            catch (Exception ex)
            {
                LogError($"❎ An exception occurred while solving part one:{Environment.NewLine}  {ex.Message}");
                return string.Empty;
            }

            var partTwoAnswer = default(string);
            try
            {
                partTwoAnswer = Solution.SolvePartTwo(ActualInput);
                LogNotice($"✔ Part two answer by actual input: {partTwoAnswer}.");
            }
            catch (Exception ex)
            {
                LogError($"❎ An exception occurred while solving part two:{Environment.NewLine}  {ex.Message}");
                return string.Empty;
            }

            LogEndLine();
            return $"Part one: {partOneAnswer}, Part two: {partTwoAnswer}";
        }


        public void LogPartOne(string message) => LogInfo($"{LogPrefix} Part1> {message}");
        public void LogPartTwo(string message) => LogInfo($"{LogPrefix} Part2> {message}");

        //public void TestPartOne() => Test(LogPartOne, Solution.SolvePartOne, SampleInput, PartOneSampleAnswer);
        //public void TestPartTwo() => Test(LogPartTwo, Solution.SolvePartTwo, SampleInput, PartTwoSampleAnswer);
        //private static void Test(Action<string> logAction, Func<string[], string> solveMethod, string[] exampleInput, string exampleAnswer)
        //{
        //    logAction?.Invoke($"★ Test started...");
        //    try
        //    {
        //        var answer = solveMethod.Invoke(exampleInput).Trim();

        //        if (answer == exampleAnswer)
        //        {
        //            logAction?.Invoke($"✔ Text passed ({answer})");
        //        }
        //        else
        //        {
        //            logAction?.Invoke($"✘ Text failed! ({answer} != {exampleAnswer})");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logAction?.Invoke($"❎ An exception occurred during test! {Environment.NewLine}{ex}");
        //    }
        //}

        //public string SolvePartOne() => Solve(LogPartOne, Solution.SolvePartOne, ActualInput);
        //public string SolvePartTwo() => Solve(LogPartTwo, Solution.SolvePartTwo, ActualInput);
        //public static string Solve(Action<string> logAction, Func<string[], string> solveMethod, string[] input)
        //{
        //    logAction?.Invoke($"★ Solve by actual input");
        //    try
        //    {
        //        var answer = solveMethod.Invoke(input).Trim();

        //        logAction?.Invoke($"✔ Answer: {answer}.");

        //        return answer;
        //    }
        //    catch (Exception ex)
        //    {
        //        logAction?.Invoke($"❎ An exception occurred: {ex.Message}");
        //        return string.Empty;
        //    }
        //}
    }
}
