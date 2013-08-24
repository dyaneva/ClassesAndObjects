// Write a program that generates and prints to the console 10 random values in the range [100, 200].

using System;

class TenRandomValues
{
    static void Main()
    {
        Random generator = new Random();
        for (int i = 1; i < 11; i++)
        {
            int number = generator.Next(100, 201);
            Console.Write("{0} ", number);
        }
        Console.WriteLine();
    }
}
