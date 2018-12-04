using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace four
{
    public class Program
    {
        private Regex regex;
        private Regex dateRegex;
        private Dictionary<int, Dictionary<string, bool?[]>> guards = new Dictionary<int, Dictionary<string, bool?[]>>();
        private Dictionary<DateTime, string> orderList = new Dictionary<DateTime, string>();
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            regex = new Regex(@"\[(?<date>.*)\]\s(?<action>.*#(?<id>\d+)|.*)", RegexOptions.Compiled);
            dateRegex = new Regex(@"\[(?<date>.*)\]", RegexOptions.Compiled);
            //First();
            Second();
        }

        private void Second()
        {
            PrepareDataModel();

            int guardId = 0;
            int lastmax = 0;

            foreach (var guard in guards)
            {
                int[] minutes = new int[60];
                for (int i = 0; i < minutes.Length; i++)
                {
                    foreach (var item in guard.Value)
                    {
                        if (item.Value[i].HasValue && item.Value[i].Value)
                        {
                            minutes[i]++;
                        }
                    }
                }

                var max = minutes.Max();
                if(lastmax < max)
                {
                    lastmax = max;
                    guardId = guard.Key;
                    var index = Array.IndexOf(minutes, lastmax);
                    Console.WriteLine($"Guard {guardId} has the most sleeps on the {index} with {max} minutes of sleep");
                    Console.WriteLine(guardId * index);
                }
            }
            Console.ReadLine();
        }

        private void First()
        {
            PrepareDataModel();

            var guardId = guards.OrderByDescending(g => g.Value.Values.Sum(c => c.Count(b => b.HasValue && b.Value == true))).First().Key;

            int[] minutes = new int[60];

            foreach (var item in guards[guardId])
            {
                for (int i = 0; i < item.Value.Length; i++)
                {
                    if (item.Value[i].HasValue && item.Value[i].Value)
                    {
                        minutes[i]++;
                    }
                }
            }

            var maxValue = minutes.Max();

            var index = Array.IndexOf(minutes, maxValue);


            Console.WriteLine(guardId * index);
            Console.ReadLine();
        }

        private void PrepareDataModel()
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {
                var reg = dateRegex.Match(line);

                var date = DateTime.Parse(reg.Groups["date"].Value);
                orderList.Add(date, line);
            }

            int id = 0;

            foreach (var s in orderList.OrderBy(i => i.Key))
            {
                var reg = regex.Match(s.Value);

                if (reg.Groups["id"].Success)
                {
                    id = int.Parse(reg.Groups["id"].Value);
                    if (!guards.ContainsKey(id))
                    {
                        guards.Add(id, new Dictionary<string, bool?[]>());
                    }
                }

                var date = DateTime.Parse(reg.Groups["date"].Value);

                var shortDate = date.ToShortDateString();

                bool?[] sleepingModes;

                if (!guards[id].ContainsKey(shortDate))
                {
                    sleepingModes = new bool?[60];
                }
                else
                {
                    sleepingModes = guards[id][shortDate];
                }

                if (reg.Groups["action"].Value == "wakes up")
                {
                    sleepingModes[date.Minute] = false;
                }
                if (reg.Groups["action"].Value == "falls asleep")
                {
                    sleepingModes[date.Minute] = true;
                }

                if (!guards[id].ContainsKey(shortDate))
                {
                    guards[id].Add(shortDate, sleepingModes);
                }
            }

            foreach (var guard in guards)
            {
                foreach (var item in guard.Value.Values)
                {
                    var sleeping = false;
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (item[i].HasValue && item[i].Value)
                        {
                            sleeping = true;
                        }
                        if (item[i].HasValue && !item[i].Value)
                        {
                            sleeping = false;
                        }

                        if (sleeping)
                        {
                            item[i] = true;
                        }
                    }
                }
            }
        }
    }
}
    