using System;
using System.Threading;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Game
    ///  организаует игровой процесс
    /// </summary>
    internal class Game
    {
        /// <summary>
        ///  Конструктор Game()
        ///  задает параметры для игровой карты, генерирует объекты для нее
        /// </summary>
        public Game()
        {
            Map map = new Map(25);
            map.GenerateMap();
            StartGame(map);
        }

        /// <summary>
        ///  Конструктор Game()
        ///  продолжает сохраненную ранее игру
        /// </summary>
        /// <param name="savedMap">Декодированное сохранение</param>
        public Game(Map savedMap)
        {
            Map map = savedMap;
            StartGame(map);
        }

        /// <summary>
        ///  Метод Game()
        ///  запускает игру
        /// </summary>
        /// <param name="map">Игровая карта</param>
        public void StartGame(Map map)
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            while (!map.End)
            {
                Console.SetCursorPosition(0, 0);
                map.DrawMap();
                map.MoveEnemy();
                if (map.ReturnTime == 0)
                {
                    map.AddEnemys();
                    map.ResetTime();
                }
                Console.WriteLine($"\nРаунд: {map.ReturnRound}   \nДо спавна: {map.ReturnTime} сек   ");
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
                    Console.SetCursorPosition(50, 0);
                    Console.WriteLine(ex.Message);
                }
                if (cki.Key != ConsoleKey.Escape)
                {
                    map.GetEnemyCount();
                    Thread.Sleep(200);
                }
                if (map.ReturnEnemyCount > 20)
                    map.End = true;
            }
            if (cki.Key != ConsoleKey.Escape)
            {
                Console.ReadLine();

            }
            else
            {
                map.End = false;
                Map.Serialize(map);
            }

            map.Time.Enabled = false;
        }
    }
}