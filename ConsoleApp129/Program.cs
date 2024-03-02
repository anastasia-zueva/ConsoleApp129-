using System;
using System.Threading;

namespace ConsoleApp129
{
    internal class Program
    {
        static void Main()
        {
            Map map = new Map();
            map.GenerateMap();
            ConsoleKeyInfo cki;

            while (!map.End)
            {
                Console.SetCursorPosition(0, 0);
                map.DrawMap();
                map.MoveEnemy();

                while (Console.KeyAvailable)
                {
                    cki = Console.ReadKey();
                    if (!Console.KeyAvailable)
                    {
                        map.MoveHero(cki.Key);
                        break;
                    }
                }

                Thread.Sleep(200);
            }

            Console.ReadLine();
        }
    }
}