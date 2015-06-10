using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LumberjackPile
{
    class Program
    {
        private static string _path;

        static void Main(string[] args)
        {
            _path = args[0];
            Stopwatch watch;

            watch = Stopwatch.StartNew();
            MakePiles();
            watch.Stop();
            Console.WriteLine("List: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            MakePilesDiff();
            watch.Stop();
            Console.WriteLine("List w/ diff: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            MakePilesArray();
            watch.Stop();
            Console.WriteLine("Array: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            MakePilesArrayDiff();
            watch.Stop();
            Console.WriteLine("Array w/ diff: " + watch.Elapsed);
        }

        static void MakePiles()
        {
            var lines = File.ReadAllLines(_path);

            var size = Convert.ToInt32(lines[0]);
            var logs = Convert.ToInt32(lines[1]);

            var pile = new List<int>();
            
            for (var i = 2; i < size; i++)
            {
                pile.AddRange(lines[i].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)));
            }

            while (logs > 0)
            {
                var index = pile.IndexOf(pile.Min());
                pile[index]++;
                logs--;
            }
        }

        static void MakePilesDiff()
        {
            var lines = File.ReadAllLines(_path);

            var size = Convert.ToInt32(lines[0]);
            var logs = Convert.ToInt32(lines[1]);

            var pile = new List<int>();

            for (var i = 2; i < size; i++)
            {
                pile.AddRange(lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)));
            }

            while (logs > 0)
            {
                var diff = 0;
                var index = GetMinIndex(pile, ref diff);
                if (diff == 0)
                {
                    diff = 1;
                }
                pile[index] += diff;
                logs -= diff;
            }
        }

        static void MakePilesArray()
        {
            var reader = new StreamReader(_path);

            var size = Convert.ToInt32(reader.ReadLine());
            size = size * size;

            var logs = Convert.ToInt32(reader.ReadLine());

            var pile = new int[size];

            for (var i = 0; i < size; i++)
            {
                pile[i] = reader.Read();
            }

            while (logs > 0)
            {
                var index = GetMinIndex(pile, size);
                pile[index]++;
                logs--;
            }
        }

        static void MakePilesArrayDiff()
        {
            var reader = new StreamReader(_path);

            var size = Convert.ToInt32(reader.ReadLine());
            size = size * size;

            var logs = Convert.ToInt32(reader.ReadLine());

            var pile = new int[size];

            for (var i = 0; i < size; i++)
            {
                pile[i] = reader.Read();
            }

            while (logs > 0)
            {
                var diff = 0;
                var index = GetMinIndex(pile, size, ref diff);
                if (diff == 0)
                {
                    diff = 1;
                }
                pile[index] += diff;
                logs -= diff;
            }
        }

        static int GetMinIndex(int[] array, int size)
        {
            var min = 0;
            for (var i = 0; i < size; i++)
            {
                if (array[i] < array[min])
                {
                    min = i;
                }
            }

            return min;
        }

        static int GetMinIndex(int[] array, int size, ref int diff)
        {
            var min = 0;
            diff = 0;
            for (var i = 0; i < size; i++)
            {
                if (array[i] < array[min])
                {
                    diff = array[min] - array[i];
                    min = i;
                }
            }

            return min;
        }

        static int GetMinIndex(List<int> list, ref int diff)
        {
            var min = 0;
            diff = 0;
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] < list[min])
                {
                    diff = list[min] - list[i];
                    min = i;
                }
            }

            return min;
        }
    }
}
