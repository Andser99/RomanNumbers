using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Text.RegularExpressions;

namespace RomanFramework
{
    class Program
    {

        static void Main(string[] args)
        {
            RomanNumber rn = new RomanNumber(2491);
            RomanNumber rn2 = new RomanNumber("XXIV");
            Console.WriteLine($"{rn.Number}: {rn.Numerals}");
            Console.WriteLine($"{rn2.Number}: {rn2.Numerals}");
            Console.WriteLine($"{rn} + {rn} = {rn + rn * rn2 - rn2}");
            Console.WriteLine($"{rn} - {rn2} = {rn - rn2}");
            Console.WriteLine($"{rn} * {rn2} = {rn * rn2}");
            Console.WriteLine($"{rn} / {rn2} = {rn / rn2}");
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Andrej\Source\Repos\RomanFramework\RomanFramework\input.txt");
            String x;
            for (int i = 0; i < 4000; i++)
            {
                if (new RomanNumber(i).Numerals != lines[i])
                {
                    Console.WriteLine(i + "SAAAAA");
                }
            }
            /*RomanNumber[] array = new RomanNumber[4000];
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
            lazy.Start();*/
            while ((x = Console.ReadLine()) != "\n")
            {
                try
                {
                    int number = int.Parse(x);
                    rn = new RomanNumber(number);
                    Console.CursorTop--;
                    Console.WriteLine($"{rn.Number}: {rn.Numerals}");
                }
                catch
                {
                    x = x.ToUpper();
                    for(int i = 0; i < x.Length; i++)
                    {
                        if (x[i] != 'M' && x[i] != 'C' && x[i] != 'D' && x[i] != 'X' && x[i] != 'L' && x[i] != 'V' && x[i] != 'I')
                        {
                            x = "";
                            break;
                        }
                    }
                    if (x != "")
                    {
                        Console.CursorTop--;
                        rn = new RomanNumber(x);
                        Console.WriteLine($"{rn.Numerals}: {rn.Number}");
                    }
                }
            }
        }
    }
}
