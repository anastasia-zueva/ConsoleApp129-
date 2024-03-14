using System;
using System.Collections.Generic;
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
        /// Поле Time
        /// таймер для счёта времени
        /// </summary>
        public System.Timers.Timer Time = new System.Timers.Timer()
        {
            Interval = 1000,
            Enabled = false,
        };

        /// <summary>
        ///  Конструктор Game()
        ///  задает параметры для игровой карты, генерирует объекты для нее
        /// </summary>
        public Game()
        {
            Map map = new Map(25, Time);
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
        ///  Метод StartGame()
        ///  запускает игру
        /// </summary>
        /// <param name="map">Игровая карта</param>
        public void StartGame(Map map)
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            bool annoyerStep = true;
            while (!map.End)
            {
                Console.SetCursorPosition(0, 0);
                map.DrawMap();
                map.MoveEnemy(Time);

                if (map.ReturnTime == 0)
                {
                    map.Spawn(map.ReturnEnemyCount + 5);
                    map.ResetTime(Time);
                }
                Console.WriteLine($"\nРаунд: {map.ReturnRound}   \nДо спавна: {map.ReturnTime} сек   \nРежим берсерка: {map.ReturnBerserk}   ");
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
                                map.MoveHero(cki.Key, Time);
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

                if (annoyerStep)
                {
                    map.MoveAnnoyer();
                    annoyerStep = false;
                }
                else
                    annoyerStep = true;

                if (cki.Key != ConsoleKey.Escape)
                {
                    map.GetEnemyCount();
                    Thread.Sleep(200);
                }
                if (map.ReturnEnemyCount > 20)
                    map.End = true;
            }
            Console.Clear();

            if (cki.Key != ConsoleKey.Escape)
            {
                bool win;
                if (map.ReturnEnemyCount == 0)
                    win = true;
                else
                    win = false;
                Record record = new Record(map.ReturnRound, map.ReturnAllEnemys, map.ReturnAllEnemys - map.ReturnEnemyCount, win);
                List<Record> records;
                try
                {
                    records = Record.DeSerialize();
                }
                catch (MyException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    records = new List<Record>();
                }

                records.Add(record);
                Record.Serialize(records);
                Console.WriteLine("Результат записан в рекорды!");
                Console.ReadLine();
            }
            else
            {
                map.End = false;
                Map.Serialize(map);
            }

            Time.Enabled = false;
        }
    }
}