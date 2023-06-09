using System;
using System.Collections.Generic;
using UnityEditor;

namespace Build
{
    public static class BuildManager
    {
        private static void ParseCommandLineArguments(out Dictionary<string, string> result)
        {
            result = new Dictionary<string, string>();
            var args = Environment.GetCommandLineArgs();

            // Extract flags with optional values
            for (int current = 0, next = 1; current < args.Length; current++, next++)
            {
                // Parse flag
                var isFlag = args[current].StartsWith("-");
                if (!isFlag) continue;
                var flag = args[current].TrimStart('-');

                // Parse optional value
                var flagHasValue = next < args.Length && !args[next].StartsWith("-");
                var value = flagHasValue ? args[next].TrimStart('-') : "";

                result[flag] = value;
            }
        }

        public static void BuildMethod()
        {
            Console.WriteLine("Build succeeded !");
            EditorApplication.Exit(0);
        }
    }
}