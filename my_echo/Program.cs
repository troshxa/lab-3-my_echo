using System;
using System.Collections.Generic;
using System.IO;

namespace my_echo
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // Передаємо стандартні потоки вводу/виводу/помилок
            return App.Run(args, Console.In, Console.Out, Console.Error);
        }
    }

    public class App
    {
        public static int Run(string[] args, TextReader stdin, TextWriter stdout, TextWriter stderr)
        {
            try
            {
                // 1. Якщо аргументів немає взагалі — читаємо зі stdin
                if (args.Length == 0)
                {
                    string input = stdin.ReadToEnd();
                    stdout.Write(input);
                    return 0; // 0 — успішне виконання
                }

                // 2. Обробка аргументів командного рядка
                bool addNewline = true;
                var words = new List<string>();

                foreach (var arg in args)
                {
                    // Обробка опції -n (має бути першим аргументом)
                    if (arg == "-n" && words.Count == 0)
                    {
                        addNewline = false;
                    }
                    // Якщо передано невідому опцію (наприклад, --unknown-option)
                    else if (arg.StartsWith("-") && words.Count == 0 && arg != "-n")
                    {
                        stderr.WriteLine($"Error: Unknown option '{arg}'");
                        return 2; // 2 — неправильні аргументи
                    }
                    // Якщо це звичайний текст
                    else
                    {
                        words.Add(arg);
                    }
                }

                // 3. Вивід тексту в stdout
                string outputText = string.Join(" ", words);
                if (addNewline)
                {
                    stdout.WriteLine(outputText);
                }
                else
                {
                    stdout.Write(outputText);
                }

                return 0; // 0 — успішне виконання
            }
            catch (Exception ex)
            {
                // У разі непередбаченої помилки при читанні/запису
                stderr.WriteLine($"Error: {ex.Message}");
                return 1; // 1 — часткова помилка
            }
        }
    }
}
