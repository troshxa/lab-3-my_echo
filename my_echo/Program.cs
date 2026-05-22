using System;
using System.Collections.Generic;
using System.IO;

namespace my_echo
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return App.Run(args, Console.In, Console.Out, Console.Error);
        }
    }

    public class App
    {
        public static int Run(string[] args, TextReader stdin, TextWriter stdout, TextWriter stderr)
        {
            try
            {
                if (args.Length == 0)
                {
                    string input = stdin.ReadToEnd();
                    stdout.Write(input);
                    return 0;
                }

                bool addNewline = true;
                var words = new List<string>();

                foreach (var arg in args)
                {
                    if (arg == "-n" && words.Count == 0)
                    {
                        addNewline = false;
                    }
                    else if (arg.StartsWith("-") && words.Count == 0 && arg != "-n")
                    {
                        stderr.WriteLine($"Error: Unknown option '{arg}'");
                        return 2;
                    }

                    else
                    {
                        words.Add(arg);
                    }
                }

                string outputText = string.Join(" ", words);
                if (addNewline)
                {
                    stdout.WriteLine(outputText);
                }
                else
                {
                    stdout.Write(outputText);
                }

                return 0; 
            }
            catch (Exception ex)
            {

                stderr.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }
    }
}
