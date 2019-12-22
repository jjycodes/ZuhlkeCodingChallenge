using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Log
    {
        public Log(LogTypeEnum type = LogTypeEnum.Error)
        {
            Type = type;
        }

        public string Message { get; set; }
        public int LineNumber { get; set; }
        public LogTypeEnum Type { get; set; }
    }

    public enum LogTypeEnum
    {
        Info = 1,
        Error = 2
    }
}
