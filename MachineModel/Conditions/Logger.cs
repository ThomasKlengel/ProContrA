using System;
using System.Collections.Generic;

namespace Model.Conditions
{
    /// <summary>
    /// Class to log every start and end of a skill
    /// </summary>
    public static class Logger
    {
        private static readonly object locker = new object();

        public static List<string> Logs { get; set; } = new List<string>();

        /// <summary>
        /// Logs a text in a list
        /// </summary>
        /// <param name="text">the text to log</param>
        public static void LogToList (string text)
        {
            string time = DateTime.Now.TimeOfDay.ToString();
            time = time.Substring(0, 12);

            Logs.Add($"Condition: [{time}] : {text}");
        }

        /// <summary>
        /// Logs a text to the console
        /// </summary>
        /// <param name="text">the text to log</param>
        public static void Log(string text)
        {
            lock (locker)
            {

                string time = DateTime.Now.TimeOfDay.ToString();
                time = time.Substring(0, 12);
                Console.BackgroundColor = ConsoleColor.Black;

                Int16 station = -1; Int16.TryParse(text.Remove(4).Remove(0, 2), out station);
                switch (station)
                {
                    case 0: Console.ForegroundColor = ConsoleColor.White; break;
                    case 1: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case 2: Console.ForegroundColor = ConsoleColor.Red; break;
                    case 3: Console.ForegroundColor = ConsoleColor.Magenta; break;
                    case 4: Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case 5: Console.ForegroundColor = ConsoleColor.Blue; break;
                    case 6: Console.ForegroundColor = ConsoleColor.Green; break;
                    default: Console.ForegroundColor = ConsoleColor.Gray; break;
                }

                if (text.StartsWith("##########"))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else if (text.Contains("Finished Station"))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }

                Console.WriteLine($"Condition: [{time}] : {text}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;


            }
        }

        private static List<string> lastLines = new List<string>() { "test1" };
        /// <summary>
        /// Logs a text to the debug console
        /// </summary>
        /// <param name="text">the text to log</param>
        public static void Debug(string text)
        {
            return; // take care: does need a lot of resources
            if (!text.Contains("Run"))
            {
                try
                {
                    //if (/*!lastLines.Contains(text) &&*/ !(lastLines.All((line) => { return line.ToLower().StartsWith("dont need to execute"); }) && text.ToLower().StartsWith("dont need to execute")))
                    //{
                    string time = DateTime.Now.TimeOfDay.ToString();
                    time = time.Substring(0, 12);
                    System.Diagnostics.Debug.WriteLine($"Condition: [{time}] : {text}");
                    //lastLines.Add(text);
                    //try
                    //{
                    //    if (lastLines.Count > 10)
                    //    {
                    //        lastLines.RemoveAt(0);
                    //    }
                    //}
                    //catch (IndexOutOfRangeException)
                    //{ }
                    //}
                }
                catch (InvalidOperationException) { }
            }
        }
    }
}
