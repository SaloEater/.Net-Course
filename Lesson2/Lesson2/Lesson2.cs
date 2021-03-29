using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2
{
    class Lesson2
    {
        private const char NO_CHARACTER = ' ';

        private Dictionary<char, int> SixteenMap = new Dictionary<char, int>()
        {
            { '0', 0},
            { '1', 1},
            { '2', 2},
            { '3', 3},
            { '4', 4},
            { '5', 5},
            { '6', 6},
            { '7', 7},
            { '8', 8},
            { '9', 9},
            { 'a', 10},
            { 'b', 11},
            { 'c', 12},
            { 'd', 13},
            { 'e', 14},
            { 'f', 15},
        };

        private const int MAX_INT_VALUE = 16;

        public string Sum(string firstNum, string secondNum)
        {
            ValidateNum(firstNum);
            ValidateNum(secondNum);
            return CalculateSum(firstNum, secondNum);
        }

        private string CalculateSum(string firstNum, string secondNum)
        {
            int maxLength = firstNum.Length > secondNum.Length ? firstNum.Length : secondNum.Length;
            string result = "";
            SumResult sumResult = new SumResult();
            for (int i = 0; i < maxLength; i++) {
                char firstChar = GetCharAt(firstNum, i);
                char secondChar = GetCharAt(secondNum, i);
                sumResult = SumChar(firstChar, secondChar, sumResult.Overflow);
                result += sumResult.Ch;
            }
            return Reverse(result);
        }

        private void ValidateNum(string number)
        {
            var invalidChars = number.Any(i => i is < '0' or > '9' and < 'a' or > 'f');
            if (invalidChars) {
                throw new ArgumentException();
            }
        }
        private char GetCharAt(string number, int index)
        {
            if (index >= number.Length) {
                return NO_CHARACTER;
            }

            return number[^(index + 1)];
        }

        private SumResult SumChar(char firstChar, char secondChar, bool overflow)
        {
            if (firstChar is NO_CHARACTER) {
                return new SumResult() { Ch = secondChar };
            }

            if (secondChar is NO_CHARACTER) {
                return new SumResult() { Ch = firstChar };
            }

            return getSumResult(firstChar, secondChar, overflow);
        }

        private SumResult getSumResult(char firstChar, char secondChar, bool overflow)
        {
            int firstInt = SixteenMap[firstChar];
            int secondInt = SixteenMap[secondChar];

            int sumInt = firstInt + secondInt;
            if (overflow) {
                sumInt++;
            }

            var result = new SumResult();
            if (sumInt >= MAX_INT_VALUE) {
                result = result with { Overflow = true };
                sumInt %= MAX_INT_VALUE;
            }

            char ch = SixteenMap.First(i => i.Value == sumInt).Key;
            return result with { Ch = ch };
        }

        private string Reverse(string result)
        {
            return new string(result.ToCharArray().Reverse().ToArray());
        }

        record SumResult
        {
            public char Ch { get; init; }
            public bool Overflow { get; init; } = false;
        }
    }
}
