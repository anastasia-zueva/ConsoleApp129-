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
            StartGame(map);
        }

        public Game(Map savedMap)
        {
            Map map = savedMap;
            StartGame(map);
        }

        public void StartGame(Map map)
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            while (!map.End)
            {
                Console.SetCursorPosition(0, 0);
                map.DrawMap();
                map.MoveEnemy();

                try
                {
                    while (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey();
                        if (!Console.KeyAvailable)
                        {
                            if (cki.Key == ConsoleKey.Escape)
                                map.End = true;
                            else
                            {
                                map.MoveHero(cki.Key);
                                Console.SetCursorPosition(50, 0);
                                Console.Write("                                                       ");
                            }
                            break;
                        }
                    }
                }
                catch (MyException ex) 
                {
                    Console.SetCursorPosition(50,0);
                    Console.WriteLine(ex.Message);
                }
                if (cki.Key != ConsoleKey.Escape)
                {
                    map.GetEnemyCount();
                    Thread.Sleep(200);
                }
            }
            if (cki.Key != ConsoleKey.Escape)
                Console.ReadLine();
            else
            {
                map.End = false;
                Map.Serialize(map);
            }
        }
    }
}