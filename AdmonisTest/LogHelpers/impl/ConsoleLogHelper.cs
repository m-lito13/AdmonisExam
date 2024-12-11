using System;

namespace AdmonisTest.LogHelpers.impl
{
    public class ConsoleLogHelper : ILogHelper
    {
        /// <summary>
        /// Log message to console
        /// </summary>
        /// <param name="message"></param>
        public void LogMessage(string message)
        {
            Console.WriteLine($"LogHelper: {message}");
        }
    }
}
