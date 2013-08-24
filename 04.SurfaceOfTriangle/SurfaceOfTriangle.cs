// Write methods that calculate the surface of a triangle by given: Side and an altitude to it; Three sides; 
// Two sides and an angle between them. Use System.Math.


using System;

class SurfaceOfTriangle
{
    // method for calculating the surface by given side and altitude to it
    static double SurfaceOfTriange(double a, double h)
    {        
        double result = (a * h) / 2;
        return result;
    }
    // method for calculating the surface by given 3 sides
    static double SurfaceOfTriange(double a, double b,double c)
    {        
        double p = ((a + b + c) / 2);
        double result = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        return result;
    }
    // method for calculating the surface by given 2 sides and angle between them
    static double SurfaceOfTriange(double a, double b, int angle)
    {
        double sinOfAngle = Math.Sin((angle * Math.PI) / 180);
        double result = ((a * b * sinOfAngle) / 2);
        return result;
    }
    // method for selecting which variant to use for the calculation of the surface 
    static void GetChoice()
    {
        int number = int.Parse(Console.ReadLine());        
        switch (number)
        {
            case 1:
                Console.WriteLine("Enter  two numbers:");
                Console.Write("side a = ");
                double a = double.Parse(Console.ReadLine());
                Console.Write("altitude h = ");
                double h = double.Parse(Console.ReadLine());
                Console.WriteLine("The surface of triangle is {0}", SurfaceOfTriange(a, h));
                break;
            case 2:
                Console.WriteLine("Enter  three numbers:");
                Console.Write("side a = ");
                a = double.Parse(Console.ReadLine());
                Console.Write("side b = ");
                double b = double.Parse(Console.ReadLine());
                Console.Write("side c = ");
                double c = double.Parse(Console.ReadLine());
                Console.WriteLine("The surface of triangle is {0}", SurfaceOfTriange(a, b, c));
                break;
            case 3:
                Console.WriteLine("Enter  three numbers:");
                Console.Write("side a = ");
                a = double.Parse(Console.ReadLine());
                Console.Write("side b = ");
                b = double.Parse(Console.ReadLine());
                Console.Write("angle = ");
                int angle;
                while (int.TryParse(Console.ReadLine(), out angle) && (angle < 0 || angle > 180))
                {
                    Console.Write("Invalid input! Please enter again: ");
                }
                Console.WriteLine("The surface of triangle is {0}", SurfaceOfTriange(a, b, angle));
                break;
            default:
                Console.WriteLine("Invalid input!");
                break;
        }
    }
    static void Main()
    {
        Console.WriteLine("Calculating the surface of an triangle by:");
        Console.WriteLine("1. Side and an altitude to it");
        Console.WriteLine("2. Three sides");
        Console.WriteLine("3. Two sides and an angle between them");
        Console.Write("Choose from 1 to 3: ");
        GetChoice();
    }
}
