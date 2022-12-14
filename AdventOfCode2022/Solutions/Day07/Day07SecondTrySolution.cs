using System;
using System.Drawing;
using System.IO;
using static System.Net.WebRequestMethods;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(07)]
public class Day07SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 07;

    public override object SolvePartOne(string[] lines)
    {
        var storage = Storage.ParseTerminalOutput(lines);
        
        return storage.AllDirectories.Where(x => x.Size < 100_000)
                                     .Sum(x => x.Size);
    }

    public override object SolvePartTwo(string[] lines)
    {
        var storage = Storage.ParseTerminalOutput(lines);

        var needSpace = 30_000_000 - (70_000_000 - storage.Size);
        return storage.AllDirectories.Where(x => x.Size >= needSpace)
                                     .Min(x => x.Size);
    }


    class Storage
    {
        private readonly Dictionary<string, Directory> _allDirectories;
        public readonly Directory Root;

        public Storage()
        {
            _allDirectories = new();
            Root = new("root");
            _allDirectories.Add(Root.Fullname, Root);
        }

        public IEnumerable<Directory> AllDirectories => _allDirectories.Values;
        public int Size => Root.Size;

        public void AddDirectory(Directory parent, string name)
        {
            var directory = parent.AddDirectory(name);
            if (directory != null)
            {
                _allDirectories.Add(directory.Fullname, directory);
            }
        }

        public void AddFile(Directory directory, string name, int size)
        {
            directory.AddFile(name, size);
        }

        internal static Storage ParseTerminalOutput(string[] lines)
            => TerminalOutputParser.Parse(lines);

        internal class Directory
        {
            public readonly Directory Parent;
            public readonly string Name;
            private readonly Dictionary<string, Directory> _directories;
            private readonly Dictionary<string, File> _files;

            public Directory(string name) : this(null, name) { }
            public Directory(Directory parent, string name)
            {
                _directories = new Dictionary<string, Directory>();
                _files = new Dictionary<string, File>();
                Parent = parent;
                Name = name;
                Size = 0;
            }

            public int Size { get; private set; }
            public string Fullname => Parent != null ? $"{Parent.Fullname}\\{Name}" : Name;

            private void IncreaseSize(int size)
            {
                Size += size;
                Parent?.IncreaseSize(size);
            }

            internal Directory AddDirectory(string name)
            {
                if (_directories.ContainsKey(name)) return null;
                //if (_files.ContainsKey(name)) return null;

                var directory = new Directory(this, name);
                _directories.Add(name, directory);
                return directory;
            }

            internal File AddFile(string name, int size)
            {
                //if (_directories.ContainsKey(name)) return null;
                if (_files.ContainsKey(name)) return null;
                var file = new File(this, name, size);
                _files.Add(name, file);

                IncreaseSize(size);
                return file;
            }

            internal Directory GetDirectory(string name) => _directories[name];

            public override string ToString() => $"{Name} {Size}";
        }

        internal class File
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
            public string Fullname => $"{Directory.Fullname}\\{Name}";
        }

        internal static class TerminalOutputParser
        {
            private static readonly string ListAll = "$ ls";
            private static readonly string ChangeToRootDirectory = "$ cd /";
            private static readonly string ChangeToUpDirectory = "$ cd ..";
            private static readonly string ChangeToDirectoryPrefix = "$ cd";
            private static readonly int ChangeToDirectoryPrefixJumpCount = ChangeToDirectoryPrefix.Length + 1;
            private static readonly string DirectoryPrefix = "dir";
            private static readonly int DirectoryPrefixJumpCount = DirectoryPrefix.Length + 1;

            public static Storage Parse(string[] lines)
            {
                var storage = new Storage();

                var currecntDirectory = storage.Root;
                foreach (var lineRaw in lines)
                {
                    var line = lineRaw.AsSpan();

                    if (line.StartsWith(ListAll))
                    {
                        continue;
                    }

                    if (line.StartsWith(ChangeToRootDirectory))
                    {
                        currecntDirectory = storage.Root;
                        continue;
                    }

                    if (line.StartsWith(ChangeToUpDirectory))
                    {
                        currecntDirectory = currecntDirectory?.Parent ?? storage.Root;
                        continue;
                    }

                    if (line.StartsWith(ChangeToDirectoryPrefix))//$ cd a
                    {
                        var directoryName = line[ChangeToDirectoryPrefixJumpCount..].ToString();
                        currecntDirectory = currecntDirectory.GetDirectory(directoryName) ?? currecntDirectory;
                        continue;
                    }

                    if (line.StartsWith(DirectoryPrefix))//dir e
                    {
                        var directoryName = line[DirectoryPrefixJumpCount..].ToString();
                        storage.AddDirectory(currecntDirectory, directoryName);
                        continue;
                    }

                    var spaceIndex = line.IndexOf(' ');
                    var size = int.Parse(line[..spaceIndex]);
                    var fileName = line[spaceIndex..].ToString();
                    storage.AddFile(currecntDirectory, fileName, size);
                    continue;

                }

                return storage;
            }
        }
    }


}
