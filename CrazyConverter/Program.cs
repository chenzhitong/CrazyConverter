using System;

namespace CrazyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                try
                {
                    Console.WriteLine(Helper.StringToBase64String(input));
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception");
                }
            }
        }
    }
}
