using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EPOS_Tools
{
    internal static class Logger
    {
        private static List<LogMessage> logBuffer = new List<LogMessage>();
        private static bool loggerShouldStop = false;

        static Logger()
        {
            var logger = new Thread(() =>
            {
                while (!loggerShouldStop)
                {
                    if (logBuffer.Count > 0)
                    {
                        int y = Console.CursorTop;

                        if (y == Console.BufferHeight - 1)
                        {
                            Console.WriteLine();
                            Console.SetCursorPosition(0, --y);
                        }

                        int x = Console.CursorLeft;
                        Console.MoveBufferArea(0, y, Console.WindowWidth, 1, 0, y + 1);
                        Console.SetCursorPosition(0, y);
                        Console.ForegroundColor = logBuffer[0].color;
                        Console.Write(logBuffer[0].message);
                        Console.SetCursorPosition(x, y + 1);
                        Console.ForegroundColor = ConsoleColor.White;
                        logBuffer.RemoveAt(0);
                    }
                };
            });

            logger.Start();
        }

        public static void Stop()
        {
            loggerShouldStop = true;
        }

        public static void Success(string text)
        {
            logBuffer.Add(new LogMessage("[Success] " + text, ConsoleColor.Green));
        }

        public static void Log(string text)
        {
            logBuffer.Add(new LogMessage("[Info] " + text, ConsoleColor.White));
        }

        public static void Waring(string text)
        {
            logBuffer.Add(new LogMessage("[Warning] " + text, ConsoleColor.Yellow));
        }

        internal static void Error(string text)
        {
            logBuffer.Add(new LogMessage("[Error] " + text, ConsoleColor.Red));
        }
    }

    internal class LogMessage
    {
        public string message;
        public ConsoleColor color;
        private DateTime dateTime;

        public LogMessage(string message, ConsoleColor color)
        {
            dateTime = DateTime.Now;
            this.message = $"[{dateTime.ToString()}]" + message;
            this.color = color;
        }
    }
}
