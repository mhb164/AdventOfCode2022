﻿using System.Drawing;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(07)]
public class Day07FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 07;

    public override object SolvePartOne(in string[] inputLines)
    {
        var storage = Parse(inputLines);
        return storage.all.Values.Where(x => x.Size < 100_000).Sum(x => x.Size);
    }

    public override object SolvePartTwo(in string[] inputLines)
    {
        var storage = Parse(inputLines);

        var freeSpace = 70000000 - storage.root.Size;
        var needSpace = 30000000 - freeSpace;
        var target = storage.all.Values.Where(x => x.Size >= needSpace).ToList();

        return target.Min(x => x.Size);
    }

    class Directory
    {
        public Directory(string name) : this(null, name) { }
        public Directory(Directory parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public Directory Parent { get; private set; }
        public string Name { get; private set; }
        public List<Directory> Directories { get; private set; } = new List<Directory>();
        public List<File> Files { get; private set; } = new List<File>();

        public override string ToString() => $"{Name} {Size}";

        public string Fullname => Parent != null ? $"{Parent.Fullname}\\{Name}" : Name;

        public int Size
        {
            get
            {
                var size = 0;
                if (Directories.Count > 0)
                    size = Directories.Sum(x => x.Size);
                if (Files.Count > 0)
                    size += Files.Sum(x => x.Size);
                return size;
            }
        }
    }

    class File
    {
        public File(Directory directory, string name, int size)
        {
            Directory = directory;
            Name = name;
            Size = size;
        }

        public Directory Directory { get; private set; }
        public string Name { get; private set; }
        public int Size { get; private set; }

        public override string ToString() => $"{Name} {Size}";

    }

    private static (Directory root, Dictionary<string, Directory> all) Parse(string[] inputLines)
    {
        var rootDirectory = new Directory("root");
        var allDirectories = new Dictionary<string, Directory>
        {
            { rootDirectory.Fullname, rootDirectory }
        };

        var currecntDirectory = rootDirectory;
        foreach (var inputLine in inputLines)
        {
            if (inputLine.StartsWith("$"))
            {
                if (inputLine.StartsWith("$ cd /"))
                {
                    currecntDirectory = rootDirectory;
                }
                else if (inputLine.StartsWith("$ ls"))
                {
                    continue;
                }
                else if (inputLine.StartsWith("$ cd .."))
                {
                    currecntDirectory = currecntDirectory?.Parent ?? rootDirectory;
                }
                else if (inputLine.StartsWith("$ cd"))
                {
                    var dirName = inputLine.Replace("$ cd", "").Trim();
                    var dir = currecntDirectory?.Directories.FirstOrDefault(d => d.Name == dirName);
                    if (dir != null) { currecntDirectory = dir; }
                    else
                    {

                    }
                }
            }
            else if (inputLine.StartsWith("dir"))
            {
                if (currecntDirectory != null)
                {
                    var dirName = inputLine.Replace("dir", "").Trim();
                    var existed = currecntDirectory.Directories.FirstOrDefault(x => x.Name == dirName);
                    if (existed == null)
                    {
                        var newdirectory = new Directory(currecntDirectory, dirName);
                        currecntDirectory.Directories.Add(newdirectory);
                        allDirectories.Add(newdirectory.Fullname, newdirectory);
                    }
                }
            }
            else
            {
                var splitted = inputLine.Split(" ");
                if (currecntDirectory != null && int.TryParse(splitted[0], out var size))
                {
                    var filename = splitted[1];
                    var existedfile = currecntDirectory.Files.FirstOrDefault(f => f.Name == filename);
                    if (existedfile == null)
                    {
                        currecntDirectory.Files.Add(new File(currecntDirectory, filename, size));
                    }
                }
            }
        }

        return (rootDirectory, allDirectories);
    }

}
