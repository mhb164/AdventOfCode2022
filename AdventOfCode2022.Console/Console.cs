using AdventOfCode2022.Puzzles;
using AdventOfCode2022.Solutions;
using System;
using System.Drawing;

namespace AdventOfCode2022;

internal class Program
{

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //ConsoleSolutionPerformer.Perform<Day01Puzzle, Day01FirstTrySolution>();
        //ConsoleSolutionPerformer.Perform<Day01Puzzle, Day01SecondTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day02Puzzle, Day02FirstTrySolution>();
        //ConsoleSolutionPerformer.Perform<Day02Puzzle, Day02SecondTrySolution>();

        ConsoleSolutionPerformer.Perform<Day03Puzzle, Day03FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day04Puzzle, Day04FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day05Puzzle, Day05FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day06Puzzle, Day06FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day07Puzzle, Day07FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day08Puzzle, Day08FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day09Puzzle, Day09FirstTrySolution>();

        //ConsoleSolutionPerformer.Perform<Day10Puzzle, Day10FirstTrySolution>();


        Console.ForegroundColor = ConsoleColor.Gray;

    }


    public class ConsoleSolutionPerformer : ILogger
    {
        static readonly ConsoleSolutionPerformer _instance = new();

        public static void Perform<TPuzzle, TPuzzleSolution>()
            where TPuzzle : Puzzle
            where TPuzzleSolution : PuzzleSolution
        {
            var performer = new SolutionPerformer<TPuzzle, TPuzzleSolution>(_instance);

            //performer.TestPartOne();
            //performer.SolvePartOne();

            //performer.TestPartTwo();
            //performer.SolvePartTwo();

            performer.SampleTest();
            performer.ActualTest();

            performer.Solve();
        }

        public void Log(string message, ConsoleColor foregroundColor)
        {
            var preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = preForegroundColor;
        }
        void ILogger.Emergency(string message)=> Log(message, ConsoleColor.DarkMagenta);
        void ILogger.Alert(string message) => Log(message, ConsoleColor.Magenta);
        void ILogger.Critical(string message) => Log(message, ConsoleColor.DarkRed);
        void ILogger.Error(string message) => Log(message, ConsoleColor.Red);
        void ILogger.Warning(string message) => Log(message, ConsoleColor.Yellow);
        void ILogger.Notice(string message) => Log(message, ConsoleColor.Green);
        void ILogger.Info(string message) => Log(message, ConsoleColor.Cyan);
        void ILogger.Debug(string message) => Log(message, ConsoleColor.Gray);
    }
}