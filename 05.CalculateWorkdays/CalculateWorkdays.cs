// Write a method that calculates the number of workdays between today and given date, passed as parameter. 
// Consider that workdays are all days from Monday to Friday except a fixed list of public holidays specified 
// preliminary as array.

using System;

class CalculateWorkdays
{    
    private static DateTime[] holidays = new DateTime[]
    {
        new DateTime(2013, 9, 6),
        new DateTime(2013, 12, 24),
        new DateTime(2012, 12, 25),
        new DateTime(2012, 12, 26),
        new DateTime(2012, 12, 31)
    };
    static double CalculateNumberOfWorkdays(DateTime now, DateTime after)
    {
        double numberOfWorkdays = 1 + ((after - now).TotalDays * 5 -
        (now.DayOfWeek - after.DayOfWeek) * 2) / 7 - holidays.Length;
        return numberOfWorkdays;
    }
    static void Main()
    {
        DateTime today = DateTime.Today;
        DateTime date = new DateTime(2013, 12, 31);
        Console.WriteLine(CalculateNumberOfWorkdays(today, date));
    }
}
