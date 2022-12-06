using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public partial class SolutionPerformer
    {
        public static void Perform(ILogger logger, int dayNumber, Action<SolutionPerformer> action)
            => Perform(typeof(Puzzle).Assembly, logger, dayNumber, action);

        public static void Perform(Assembly assembly, ILogger logger, int dayNumber, Action<SolutionPerformer> action)
        {
            var puzzleType = GetDayPuzzleType(assembly, dayNumber);
            var puzzleSolutionsTypes = GetDayPuzzleSolutionsTypes(assembly, dayNumber);

            if (puzzleType == null)
            {
                logger?.Error($"Day{dayNumber:d2} puzzle not found!");
                return;
            }

            if (puzzleSolutionsTypes.Count == 0)
            {
                logger?.Error($"Day{dayNumber:d2} solution not found!");
                return;
            }

            foreach (var puzzleSolutionType in puzzleSolutionsTypes)
            {
                var puzzle = Activator.CreateInstance(puzzleType) as Puzzle;
                var solution = Activator.CreateInstance(puzzleSolutionType) as PuzzleSolution;
                var performer = new SolutionPerformer(logger, puzzle, solution);
                action?.Invoke(performer);
            }
        }

        private static Type GetDayPuzzleType(Assembly assembly, int dayNumber)
           => GetDayTypes(assembly, dayNumber).Where(x => x.IsAssignableTo(typeof(Puzzle))).FirstOrDefault();

        private static List<Type> GetDayPuzzleSolutionsTypes(Assembly assembly, int dayNumber)
            => GetDayTypes(assembly, dayNumber).Where(x => x.IsAssignableTo(typeof(PuzzleSolution))).ToList();

        private static IEnumerable<Type> GetDayTypes(Assembly assembly, int dayNumber)
           => assembly.GetTypes()
                      .Select((type, attribute) => (attribute: (Attribute.GetCustomAttribute(type, typeof(PuzzleDayNumberAttribute)) as PuzzleDayNumberAttribute), type))
                      .Where(x => x.attribute is not null && x.attribute.Value == dayNumber)
                      .Select(x => x.type);


        public static void Perform<TPuzzle, TPuzzleSolution>(ILogger logger, Action<SolutionPerformer> action)
              where TPuzzle : Puzzle
            where TPuzzleSolution : PuzzleSolution
        {
            action?.Invoke(new SolutionPerformer<TPuzzle, TPuzzleSolution>(logger));
        }
    }
}
