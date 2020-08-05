using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RomanFramework
{
    class RomanNumber
    {
        private bool isInitialised = true;

        private int _number;
        public int Number
        {
            get { return _number; }
            private set { _number = value % 4000; }
        }

        private String _numerals;
        public string Numerals
        {
            get
            {
                if (!isInitialised)
                {
                    _numerals = IntToRoman(Number);
                }
                return _numerals;
            }
            private set { _numerals = value; }
        }


        private static int[] modulos = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        private static readonly Dictionary<char, (int, int)> NumeralsValueDict = new Dictionary<char, (int, int)> ()
        {
            { 'M', (1000, 0) },
            { 'D', (500, 1)},
            { 'C', (100, 2)},
            { 'L', (50, 3)},
            { 'X', (10, 4)},
            { 'V', (5, 5)},
            { 'I', (1, 6)},
        };
        private static String[] numerals = new String[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        private static int[] modulosSub = new int[] { 1000, -200, 500, -200, 100, -20, 50, -20, 10, -2, 5, -2, 1 };

        public RomanNumber(int number, bool isLazy = false)
        {
            Number = number;
            if (isLazy)
            {
                isInitialised = false;
            }
            else
            {
                Numerals = IntToRoman(Number);
            }
        }

        public RomanNumber(String romanString, bool useOldConverter = false)
        {
            romanString = romanString.ToUpper();
            if (!Regex.Match(romanString, "^((nulla)|(M{0,3})(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3}))$").Success)
            {
                throw new Exception($"Invalid roman number format: {romanString}\n");
            }
            Numerals = romanString;
            Number = useOldConverter ? RomanToIntOld(romanString) : RomanToInt(romanString);
        }

        /*private int ParseRomanNumber(int number)
        {
            if (number >= 4000)
            {
                Console.WriteLine($"Number({number}) is too big too convert to Roman. Returning {number} modulo 4000.\n");
                Number = number % 4000;
                return Number;
            }
            return number;
        }*/

        private static String IntToRoman(int number)
        {
            if (number == 0)
            {
                return "nulla";
            }
            String result = "";
            int i = 0;
            while(number > 0)
            {
                while (number / modulos[i] > 0)
                {
                    result += numerals[i];
                    number -= modulos[i];
                }
                i++;
            }
            return result;
        }

        private static int RomanToInt(String romanNumber)
        {
            int result = 0;
            if (romanNumber[0] == 'n')
            {
                return 0;
            }
            for (int i = 0; i < romanNumber.Length; i++)
            {
                if (NumeralsValueDict[romanNumber[i]].Item2 % 2 == 0 && NumeralsValueDict[romanNumber[i]].Item2 > 0)
                {
                    if (i != romanNumber.Length - 1 && NumeralsValueDict[romanNumber[i + 1]].Item2 < NumeralsValueDict[romanNumber[i]].Item2)
                    {
                        result += NumeralsValueDict[romanNumber[i + 1]].Item1 - NumeralsValueDict[romanNumber[i]].Item1;
                        i++;
                    }
                    else
                    {
                        result += NumeralsValueDict[romanNumber[i]].Item1;
                    }
                }
                else
                {
                    result += NumeralsValueDict[romanNumber[i]].Item1;
                }
            }
            return result;
        }

        private int RomanToIntOld(String romanNumber)
        {
            int result = 0;
            foreach(var x in numerals)
            {
                result += Regex.Matches(romanNumber, x).Count * modulosSub[Array.FindIndex(numerals, _ => _.Equals(x))];
            }
            return result;
        }

        public override string ToString()
        {
            return $"{Number}({Numerals})";
        }

        public static RomanNumber operator +(RomanNumber num1, RomanNumber num2)
        {
            return new RomanNumber(num1.Number + num2.Number);
        }

        public static RomanNumber operator -(RomanNumber num1, RomanNumber num2)
        {
            return new RomanNumber(num1.Number - num2.Number);
        }

        public static RomanNumber operator *(RomanNumber num1, RomanNumber num2)
        {
            return new RomanNumber(num1.Number * num2.Number);
        }

        public static RomanNumber operator /(RomanNumber num1, RomanNumber num2)
        {
            return new RomanNumber(num1.Number / num2.Number);
        }

        public static bool operator ==(RomanNumber num1, RomanNumber num2)
        {
            return (num1.Number == num2.Number);
        }

        public static bool operator !=(RomanNumber num1, RomanNumber num2)
        {
            return (num1.Number != num2.Number);
        }
    }
}