using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace RomanFramework
{
    class Program
    {

        static void Main(string[] args)
        {
            RomanNumber rn = new RomanNumber(1491);
            RomanNumber rn2 = new RomanNumber("XXX");
            Console.WriteLine($"{rn.Number}: {rn.Numerals}");
            Console.WriteLine($"{rn2.Number}: {rn2.Numerals}");
            String x;
            RomanNumber[] array = new RomanNumber[4000];
            for (int i = 0; i < 4000; i++)
            {
                array[i] = new RomanNumber(i);
            }
            Task init = new Task(() =>
            {
                Stopwatch stopwatchInit = Stopwatch.StartNew();
                for (int i = 0; i < 500000; i++)
                {
                    new RomanNumber(array[i % 300].Numerals);
                }
                stopwatchInit.Stop();
                Console.WriteLine($"Done init {stopwatchInit.ElapsedMilliseconds}");
            });
            Task lazy = new Task(() =>
            {
                Stopwatch stopwatchLazy = Stopwatch.StartNew();
                for (int i = 0; i < 500000; i++)
                {
                    new RomanNumber(array[i % 300].Numerals, true);
                }
                stopwatchLazy.Stop();
                Console.WriteLine($"Done lazy {stopwatchLazy.ElapsedMilliseconds}");
            });
            init.Start();
            lazy.Start();
            while ((x = Console.ReadLine()) != "\n")
            {
                Console.CursorTop--;
                rn = new RomanNumber(int.Parse(x));
                Console.WriteLine($"{rn.Number}: {rn.Numerals}");
            }
        }
    }
}
