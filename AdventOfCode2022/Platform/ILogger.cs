using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public interface ILogger
    {
        void Emergency(string message);
        void Alert(string message);
        void Critical(string message);
        void Error(string message);
        void Warning(string message);
        void Notice(string message);
        void Info(string message);
        void Debug(string message);
    }
}
