using AdventOfCode2022.Puzzles;
using AdventOfCode2022.Solutions;
using System;
using System.Drawing;
using System.Security.Cryptography;

namespace AdventOfCode2022;

internal class Program
{
    static void Main(string[] args)
    {
        //ConsoleSolutionPerformer.PerformDebug(01);
        //ConsoleSolutionPerformer.PerformDebug(02);
        //ConsoleSolutionPerformer.PerformDebug(03);
        //ConsoleSolutionPerformer.PerformDebug(04);
        //ConsoleSolutionPerformer.PerformDebug(05);
        //ConsoleSolutionPerformer.PerformDebug(06);
        //ConsoleSolutionPerformer.PerformDebug(07);
        //ConsoleSolutionPerformer.PerformDebug(08);
        //ConsoleSolutionPerformer.PerformDebug(09);
        //ConsoleSolutionPerformer.PerformDebug(10);
        //ConsoleSolutionPerformer.PerformDebug(11);
        //ConsoleSolutionPerformer.PerformDebug(12);
        //ConsoleSolutionPerformer.PerformDebug(13);
        //ConsoleSolutionPerformer.PerformDebug(14);
        //ConsoleSolutionPerformer.PerformDebug(15);
        //ConsoleSolutionPerformer.PerformDebug(16);
        //ConsoleSolutionPerformer.PerformDebug(17);
        //ConsoleSolutionPerformer.PerformDebug(18);
        //ConsoleSolutionPerformer.PerformDebug(19);
        //ConsoleSolutionPerformer.PerformDebug(20);
        //ConsoleSolutionPerformer.PerformDebug(21);
        //ConsoleSolutionPerformer.PerformDebug(22);
        //ConsoleSolutionPerformer.PerformDebug(23);
        //ConsoleSolutionPerformer.PerformDebug(24);
        //ConsoleSolutionPerformer.PerformDebug(25);

        var today = (DateTime.Now.Date - new DateTime(2022, 12, 1)).Days + 1;
        ///var today = 09; 
        ConsoleSolutionPerformer.PerformDebug(today);

        ////Perform until today
        //for (int i = 1; i <= today; i++) ConsoleSolutionPerformer.Perform(i);

        //Old code
        //ConsoleSolutionPerformer.Perform<Day01Puzzle, Day01FirstTrySolution>();
        //ConsoleSolutionPerformer.Perform<Day01Puzzle, Day01SecondTrySolution>();
    }


    public class ConsoleSolutionPerformer : ISolutionPerformerPlatform, ILogger
    {

        static readonly ConsoleSolutionPerformer _instance = new();
        static readonly string _configFilepath = "C:\\AdventOnCode2022.cfg";
        static readonly string _rootDirectory;

        private static string ActualInputsDirectory => Path.Combine(_rootDirectory, "ActualInputs");

        static ConsoleSolutionPerformer()
        {
            if (File.Exists(_configFilepath))
                _rootDirectory = File.ReadAllText(_configFilepath);
            if (string.IsNullOrWhiteSpace(_rootDirectory) || !Directory.Exists(_rootDirectory))
            {
                throw new FileNotFoundException("Config file (c:\\AdventOnCode2022.cfg)");
            }
        }

        public ConsoleSolutionPerformer()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        internal static void PerformDebug(int dayNumber) => SolutionPerformer.Perform(_instance, dayNumber, ConsoleDebugAction);
        internal static void Perform(int dayNumber) => SolutionPerformer.Perform(_instance, dayNumber, ConsoleAction);

        public static void Perform<TPuzzle, TPuzzleSolution>()
                where TPuzzle : Puzzle
                where TPuzzleSolution : PuzzleSolution
            => SolutionPerformer.Perform<TPuzzle, TPuzzleSolution>(_instance, ConsoleAction);

        public static void ConsoleDebugAction(SolutionPerformer performer)
        {
            performer.SampleTest(true);
            //performer.ActualTest(true);

            performer.Solve(true);
        }

        public static void ConsoleAction(SolutionPerformer performer)
        {
            performer.ActualTest(false);
        }

        //public static void Perform<TPuzzle, TPuzzleSolution>()
        //    where TPuzzle : Puzzle
        //    where TPuzzleSolution : PuzzleSolution
        //{
        //    var performer = new SolutionPerformer<TPuzzle, TPuzzleSolution>(_instance);

        //    //performer.TestPartOne();
        //    //performer.SolvePartOne();

        //    //performer.TestPartTwo();
        //    //performer.SolvePartTwo();

        //    performer.SampleTest(true);
        //    performer.ActualTest(true);

        //    performer.Solve(true);
        //}

        public void Log(string message, ConsoleColor foregroundColor)
        {
            var preForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = preForegroundColor;
        }

        void ILogger.Emergency(string message) => Log(message, ConsoleColor.DarkMagenta);
        void ILogger.Alert(string message) => Log(message, ConsoleColor.Magenta);
        void ILogger.Critical(string message) => Log(message, ConsoleColor.DarkRed);
        void ILogger.Error(string message) => Log(message, ConsoleColor.Red);
        void ILogger.Warning(string message) => Log(message, ConsoleColor.Yellow);
        void ILogger.Notice(string message) => Log(message, ConsoleColor.Green);
        void ILogger.Info(string message) => Log(message, ConsoleColor.Cyan);
        void ILogger.Debug(string message) => Log(message, ConsoleColor.Gray);

        ILogger ISolutionPerformerPlatform.Logger => this;

        string[] ISolutionPerformerPlatform.GetActualInput(int dayNumber)
        {
            return File.ReadAllLines(Path.Combine(ActualInputsDirectory, $"Day{dayNumber:d2}.txt"));
        }
    }
}