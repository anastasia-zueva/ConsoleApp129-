using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Battle
    ///  миниигра, запускающаяся при столкновении героя и врага
    /// </summary>
    internal class Battle
    {
        /// <summary>
        /// Поле _rand
        /// экземпляр класса Random для генерации рандомных чисел
        /// </summary>
        static private Random _rand = new Random();

        /// <summary>
        /// Поле _heroDmg
        /// количество нанесенного героем урона
        /// </summary>
        private int _heroDmg = 0;

        /// <summary>
        /// Поле _enemyDmg
        /// количество нанесенного врагом урона
        /// </summary>
        private int _enemyDmg = 0;

        /// <summary>
        /// Поле _atkOrDef
        /// выбранные героем позиции 
        /// </summary>
        private int[] _atkOrDef = new int[5];

        /// <summary>
        /// Поле _enemyAtkOrDef
        /// выбранные врагом позиции 
        /// </summary>
        private int[] _enemyAtkOrDef = new int[5];

        /// <summary>
        ///  Конструктор Battle()
        ///  очищает консоль для последующей организации боя
        /// </summary>
        public Battle()
        {
            Console.Clear();
            StartBattle();
        }

        /// <summary>
        ///  Метод StartBattle()
        ///  отвечает за визуальное отображение миниигры
        /// </summary>
        private void StartBattle()
        {
            int i = 0;
            ConsoleKeyInfo a;

            Console.WriteLine("нажмите enter чтобы начать");
            while (true)
            {
                a = Console.ReadKey();
                if (a.Key == ConsoleKey.Enter)
                    break;
            }
            Console.Clear();
            Console.WriteLine("вверх - атака\nвниз - защита\n\nвыбор героя:");
            while (i != 5)
            {
                a = Console.ReadKey();
                switch (a.Key)
                {
                    case ConsoleKey.UpArrow:
                        _atkOrDef[i] = 0;
                        PrintAtk(++i);
                        break;
                    case ConsoleKey.DownArrow:
                        _atkOrDef[i] = 1;
                        PrintDef(++i);
                        break;
                    default:
                        break;
                }
            }

            GenerateEnemyAtk();
            CalculateDmg();
        }

        /// <summary>
        ///  Метод GenerateEnemyAtk()
        ///  генерирует выбор противника
        /// </summary>
        private void GenerateEnemyAtk()
        {
            Console.WriteLine("\n\nвыбор противника:");
            for (int i = 0; i < 5; )
            {
                _enemyAtkOrDef[i] = _rand.Next(0, 2);
                if (_enemyAtkOrDef[i] == 0)
                    PrintAtk(++i);
                else
                    PrintDef(++i);
            }
        }

        /// <summary>
        ///  Метод CalculateDmg()
        ///  подсчитывает нанесенный урон
        /// </summary>
        private void CalculateDmg()
        {
            Console.WriteLine("\n\nбаттл:");
            for (int i = 0; i < 5; i++)
            {
                if (_enemyAtkOrDef[i] == _atkOrDef[i] & _enemyAtkOrDef[i] == 1)
                    Console.WriteLine($"{i + 1}. ничья");
                else if (_enemyAtkOrDef[i] == _atkOrDef[i] & _enemyAtkOrDef[i] == 0)
                {
                    _enemyDmg += 3;
                    _heroDmg += 3;
                    Console.WriteLine($"{i + 1}. оба атаковали +3 каждому");
                }
                else if (_enemyAtkOrDef[i] > _atkOrDef[i])
                {
                    _enemyDmg++;
                    Console.WriteLine($"{i + 1}. противник поставил блок +1 врагу");
                }
                else
                {
                    _heroDmg++;
                    Console.WriteLine($"{i + 1}. герой поставил блок +1 игроку");
                }
            }

            Console.Write($"\n\nрезультаты боя:\nгерой: {_heroDmg}\nвраг: {_enemyDmg}");

            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        ///  Метод PrintAtk()
        ///  выводит информацию о выборе атакующей позиции
        /// </summary>
        /// <param name="i">Номер выбранного действия</param>
        private void PrintAtk(int i) => Console.WriteLine($"{i}. Атака");

        /// <summary>
        ///  Метод PrintDef()
        ///  выводит информацию о выборе защитной позиции
        /// </summary>
        /// <param name="i">Номер выбранного действия</param>
        private void PrintDef(int i) => Console.WriteLine($"{i}. Защита");

        /// <summary>
        ///  Метод GetResults()
        ///  подсчитывает чей нанесенный урон больше, у героя или врага
        /// </summary>
        /// <returns>Результат боя</returns>
        public int GetResults()
        {
            if (_heroDmg > _enemyDmg)
                return 1;
            else if (_heroDmg == _enemyDmg)
                return 2;
            else
                return 3;
        }
    }
}