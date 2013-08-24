// You are given a sequence of positive integer values written into a string, separated by spaces. Write a function 
// that reads these values from given string and calculates their sum.
// Example: string = "43 68 9 23 318"  result = 461

using System;

class Sequence
{    
    static int SumOfNumbersInSequence(string str)
    {
        int sum = 0;
        string[] sequenceOfIntegers = str.Split(' ');
        for (int i = 0; i < sequenceOfIntegers.Length; i++)
        {
            sum += Int32.Parse(sequenceOfIntegers[i]);
        }        
        return sum;
    }
    static void Main()
    {
        string sequence = "43 68 9 23 318";
        Console.WriteLine(SumOfNumbersInSequence(sequence));
    }
}
