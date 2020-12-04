using System;
using TimeAndTimePeriod;

namespace TimeAndTimePeriodApp
{
    class Program
    {
        static void Main()
        {
            /*Console.WriteLine("Witaj w aplikacji!");
            Console.WriteLine("O to prosta pokazówka działania aplikacji!");
            string hour, minutes, seconds;
            Console.WriteLine("Podaj godzine");
            hour = Console.ReadLine();
            Console.WriteLine("Podaj minuty");
            minutes = Console.ReadLine();
            Console.WriteLine("Podaj sekundy");
            seconds = Console.ReadLine();
            Time t = new Time($"{hour}:{minutes}:{seconds}");
            Console.WriteLine("Jest godzina: " + t);
            */

            Time t1 = new Time(10, 59, 0);
            TimePeriod t2 = new TimePeriod(25, 2, 59);
            Console.WriteLine(t1 - t2);
        }
    }
}
