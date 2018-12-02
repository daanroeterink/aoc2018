using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace two
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        private int twos = 0;
        private int threes = 0;

        public Program()
        {
            //First();
            Second();
        }

        private void First()
        {
            var input = File.ReadAllLines("input.txt");

            foreach (var s in input)
            {
                CountTwoAndThrees(s);
            }

            Console.WriteLine(twos * threes);
            Console.ReadLine();
        }

        private void CountTwoAndThrees(string input)
        {
            var distinct = input.Distinct();

            var twofound = false;
            var threefound = false;

            foreach (var letter in distinct)
            {
                var count = input.Count(x => x == letter);
                if (count == 2 && !twofound)
                {
                    twos++;
                    twofound = true;
                }
                if (count == 3 && !threefound)
                {
                    threes++;
                    threefound = true;
                }

                if (twofound && threefound)
                {
                    return;
                }
            }
        }

        private void Second()
        {
            var lines = File.ReadAllLines("input.txt");

            var counter = 0;
            var highesti = 0;
            var highestj = 0;


            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    var internalCounter = lines[i].Where((t, ni) => t == lines[j][ni]).Count();

                    if (internalCounter > counter)
                    {
                        counter = internalCounter;
                        Console.WriteLine($"VALUE {lines[i]} equals {lines[j]} the most");
                        highesti = i;
                        highestj = j;
                    }
                }
            }

            for (var ni = 0; ni < lines[highesti].Length; ni++)
            {
                if (lines[highesti][ni] == lines[highestj][ni])
                {
                    Console.Write(lines[highesti][ni]);
                }
            }
            Console.WriteLine();


            Console.ReadLine();
        }
    }
}
