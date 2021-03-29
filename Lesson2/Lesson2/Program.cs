using System;

namespace Lesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstNum = args[0];
            string secondNum = args[1];
            string result = new Lesson2().Sum(firstNum, secondNum);
            Console.WriteLine($"{firstNum} + {secondNum} = {result}");
        } 
    }
}
