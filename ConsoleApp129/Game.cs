using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Game
    ///  управляет игровым процессом
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// Поле Time
        /// таймер для счета времени по секундам
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
            Map map = new Map(25);
            map.SetStickyTime(Spawning.GenerateMap(ref map, ref map.I, ref map.J));
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
            Time.Elapsed += map.Timer_Tick;
            Time.Enabled = true;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            bool annoyerStep = true;
            while (!map.End)
            {
                Console.SetCursorPosition(0, 0);
                Drawing.DrawMap(ref map);
                Movement.MoveEnemy(Time, ref map);

                if (map.ReturnTime() == 0)
                {
                    Spawning.Spawn(map.ReturnEnemyCount() + 5, ref map);
                    map.ResetTime();
                }

                if (map.ReturnStickyTime() == 0)
                    map.SetStickyTime(Spawning.StickySpawn(ref map, ref map.I, ref map.J));

                Console.WriteLine($"\nРаунд: {map.ReturnRound()}   \nДо появления: {map.ReturnTime()} сек   " +
                    $"\nЛипкое поле через: {map.ReturnStickyTime()} сек \nРежим берсерка: {map.ReturnBerserk()}   ");

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
                                Movement.MoveHero(cki.Key, Time, ref map);
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
                    Movement.MoveAnnoyer(ref map, ref map.I, ref map.J);
                    annoyerStep = false;
                }
                else
                    annoyerStep = true;

                if (cki.Key != ConsoleKey.Escape)
                {
                    Drawing.GetEnemyCount(ref map);
                    Thread.Sleep(200);
                }
                if (map.ReturnEnemyCount() > 20)
                    map.End = true;
            }
            Console.Clear();

            if (cki.Key != ConsoleKey.Escape)
            {
                bool win;

                if (map.ReturnEnemyCount() == 0)
                    win = true;
                else
                    win = false;

                Record record = new Record(map.ReturnRound(), map.ReturnAllEnemies(), map.ReturnAllEnemies() - map.ReturnEnemyCount(), map.ReturnAnnoyerCount(), win);
                List<Record> records;

                try
                {
                    records = DeSerialize.DeSerializeRecords();
                }
                catch (MyException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    records = new List<Record>();
                }

                records.Add(record);
                Serialize.SerializeRecords(records);
                Console.WriteLine("Результат записан в рекорды!");
                Console.ReadLine();
            }
            else
            {
                map.End = false;
                Serialize.SerializeMap(map);
            }

            Time.Enabled = false;
        }
    }
}