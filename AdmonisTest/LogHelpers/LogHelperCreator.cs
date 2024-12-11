using AdmonisTest.LogHelpers;
using AdmonisTest.LogHelpers.impl;
using System;

namespace AdmonisTest
{
    public class LogHelperCreator
    {
        public ILogHelper CreateLogHelper(string logType)
        {
            switch (logType)
            {
                case LogTypes.Console:
                    return new ConsoleLogHelper();
                default:
                    throw new ArgumentException("Unsupported log type");
            }
        }
    }
}
