using System;
using System.Threading;

namespace ConsoleApp129
{
    internal class Game
    {
        public Game()
        {
            Map map = new Map(25);
            map.GenerateMap();
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

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
                        if (cki.Key == ConsoleKey.Escape)
                            map.End = true;
                        else
                            map.MoveHero(cki.Key);
                        break;
                    }
                }
                if (cki.Key != ConsoleKey.Escape)
                {
                    map.GetEnemyCount();
                    Thread.Sleep(200);
                }
            }
            if (cki.Key != ConsoleKey.Escape)
                Console.ReadLine();
        }
    }
}