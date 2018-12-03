using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace three
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        private List<int>[,] sewingField = new List<int>[2048, 2048];
        private Regex regex;

        public Program()
        {
            regex = new Regex(@"#(?<id>\d+)\s@\s(?<x>\d+),(?<y>\d+):\s(?<sizex>\d+)x(?<sizey>\d+)",
                RegexOptions.Compiled);
            //First();
            Second();
        }

        private void Second()
        {
            var input = File.ReadAllLines("input.txt");

            for (var x = 0; x < sewingField.GetLength(0); x++)
            {
                for (var y = 0; y < sewingField.GetLength(1); y++)
                {
                    sewingField[x, y] = new List<int>();
                }
            }

            foreach (var s in input)
            {
                var regex = this.regex.Match(s);

                var xi = int.Parse(regex.Groups["x"].Value);
                var yi = int.Parse(regex.Groups["y"].Value);
                var xs = int.Parse(regex.Groups["sizex"].Value);
                var ys = int.Parse(regex.Groups["sizey"].Value);
                var id = int.Parse(regex.Groups["id"].Value);


                for (var x = xi; x < xi + xs; x++)
                {
                    for (var y = yi; y < yi + ys; y++)
                    {
                        sewingField[x, y].Add(id);
                    }
                }
            }

            var oneOf = new List<int>();
            var moreOf = new List<int>();

            for (var x = 0; x < sewingField.GetLength(0); x++)
            {
                for (var y = 0; y < sewingField.GetLength(1); y++)
                {
                    var count = sewingField[x, y].Count;
                    if (count == 1)
                    {
                        oneOf.Add(sewingField[x, y].First());
                    }
                    else if (count > 1)
                    {
                        moreOf.AddRange(sewingField[x, y]);
                    }
                }
            }

            Console.WriteLine(oneOf.Except(moreOf).First());
            Console.ReadLine();
        }
    
        private void First()
        {
            var input = File.ReadAllLines("input.txt");

            for (var x = 0; x < sewingField.GetLength(0); x++)
            {
                for (var y = 0; y < sewingField.GetLength(1); y++)
                {
                    sewingField[x,y] = new List<int>();
                }
            }

            foreach (var s in input)
            {
                var regex = this.regex.Match(s);

                var xi = int.Parse(regex.Groups["x"].Value);
                var yi = int.Parse(regex.Groups["y"].Value);
                var xs = int.Parse(regex.Groups["sizex"].Value);
                var ys = int.Parse(regex.Groups["sizey"].Value);
                var id = int.Parse(regex.Groups["id"].Value);


                for (var x = xi; x < xi+xs; x++)
                {
                    for (var y = yi; y < yi+ys; y++)
                    {
                        sewingField[x, y].Add(id);
                    }
                }
            }

            var counter = 0;

            for (var x = 0; x < sewingField.GetLength(0); x++)
            {
                for (var y = 0; y < sewingField.GetLength(1); y++)
                {
                    if (sewingField[x, y].Count > 1)
                    {
                        counter++;
                    }
                }
            }
            Console.WriteLine(counter);
            Console.ReadLine();
        }
    }
}
