using System;

namespace ConsoleApp129
{
    internal class Program
    {
        public static bool Exit = false;
        static void Main()
        {
            while (!Exit)
            {
                new Menu();
            }
        }
    }
}