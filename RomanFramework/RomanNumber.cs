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
            private set { _number = value; }
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
                if (IsValidRomanNumber(number))
                {
                    Numerals = IntToRoman(number);
                }
            }
        }

        public RomanNumber(String romanString, bool useOldConverter = false)
        {
            if (!Regex.Match(romanString, "^(nulla)|(M{0,3})(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$").Success)
            {
                throw new Exception($"Invalid roman number format: {romanString}\n");
            }
            Numerals = romanString;
            Number = useOldConverter ? RomanToIntOld(romanString) : RomanToInt(romanString);
        }

        public static bool IsValidRomanNumber(int number)
        {
            if (number >= 4000 || number < 0)
            {
                //throw new Exception($"Number({number}) is not in range [0..3999] for roman number creation\n");
                return false;
            }
            return true;
        }

        private String IntToRoman(int number)
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

        private int RomanToInt(String romanNumber)
        {
            int result = 0;
            //int[] list = new int[12];\
            if (romanNumber[0] == 'n')
            {
                return 0;
            }
            for (int i = 0; i < romanNumber.Length; i++)
            {
                if (NumeralsValueDict[romanNumber[i]].Item2 % 2 == 0 && NumeralsValueDict[romanNumber[i]].Item2 > 0)
                {
                    if (i != romanNumber.Length - 1 && NumeralsValueDict[romanNumber[i + 1]].Item2 > NumeralsValueDict[romanNumber[i]].Item2)
                    {
                        result += NumeralsValueDict[romanNumber[i + 1]].Item1 - NumeralsValueDict[romanNumber[i]].Item1;
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


    }
}
