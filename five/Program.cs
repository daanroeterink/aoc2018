using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;

namespace five
{
    public class Program
    {
        private string alfabet = "abcdefghijklmnopqrstuvwxyz";
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            //First();
            Second();
        }

        private void Second()
        {
            var input = File.ReadAllText("input.txt");
            var lowestInput = input.Length;
            char lowestChar = 'd';

            foreach (char c in alfabet)
            {
                var currentInput = input.Replace(c.ToString(), string.Empty).Replace(char.ToUpper(c).ToString(), string.Empty);

                var stringBuilder = new StringBuilder();
                var changed = RemoveDouble(currentInput, stringBuilder);
                var newInput = stringBuilder.ToString();
                while (changed)
                {
                    stringBuilder.Clear();
                    changed = RemoveDouble(newInput, stringBuilder);
                    newInput = stringBuilder.ToString();
                }

                if (lowestInput > newInput.Length)
                {
                    lowestInput = newInput.Length;
                    lowestChar = c;
                    Console.WriteLine($"Lowest char is {lowestChar} with {lowestInput} left");
                }
            }
            Console.WriteLine($"Lowest char is {lowestChar} with {lowestInput} left");
            Console.ReadLine();
        }

        private void First()
        {
            var input = File.ReadAllText("input.txt");

            var stringBuilder = new StringBuilder();
            var changed = RemoveDouble(input, stringBuilder);
            var newInput = stringBuilder.ToString();
            while (changed)
            {
               stringBuilder = new StringBuilder();
               changed = RemoveDouble(newInput, stringBuilder);
               newInput = stringBuilder.ToString();
            }

            Console.WriteLine(newInput.Length);
            Console.ReadLine();
        }

        private bool RemoveDouble(string input, StringBuilder stringBuilder)
        {
            var changed = false;
            for (int i = 0; i < input.Length; i++)
            {
                var checkChar = input[i];

                if (i == input.Length -1)
                {
                    stringBuilder.Append(checkChar);
                    return changed;
                }

                if (!IsEnglishLetter(checkChar))
                {
                    return changed;
                }

                if (char.IsLower(checkChar))
                {
                    if (input[i + 1] == char.ToUpper(checkChar))
                    {
                        i++;
                        changed = true;
                    }
                    else
                    {
                        stringBuilder.Append(checkChar);
                    }
                }
                else
                {
                    if (input[i + 1] == char.ToLower(checkChar))
                    {
                        i++;
                        changed = true;
                    }
                    else
                    {
                        stringBuilder.Append(checkChar);
                    }
                }
            }

            return changed;
        }

        private bool IsEnglishLetter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }
    }


}
