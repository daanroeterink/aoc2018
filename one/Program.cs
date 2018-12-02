using System;
using System.Collections.Generic;
using System.IO;

namespace one
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            Second();
        }

        public void First()
        {
            var lines = File.ReadAllLines("input.txt");

            int start = 0;

            foreach (var line in lines)
            {
                int change = int.Parse(line.Replace("+", string.Empty));

                start += change;
            }

            Console.WriteLine($"First part:{start}");

            Console.ReadLine();
        }

        public void Second()
        {
            var lines = File.ReadAllLines("input.txt");

            var list = new List<int>();

            var start = 0;
            
            var found = false;

            while (!found)
            {
                foreach (var line in lines)
                {
                    int change = int.Parse(line.Replace("+", string.Empty));

                    var result = start + change;

                    if (list.Contains(result))
                    {
                        found = true;
                        Console.WriteLine(result);
                        break;
                    }
                    else
                    {
                        list.Add(result);
                        start = result;
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
