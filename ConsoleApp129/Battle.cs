using System;

namespace ConsoleApp129
{
    internal class Battle
    {
        private Random _rand = new Random();
        private int _heroDmg = 0;
        private int _enemyDmg = 0;
        private int[] _atkOrDef = new int[5];
        private int[] _enemyAtkOrDef = new int[5];

        public Battle()
        {
            Console.Clear();
            StartBattle();
        }

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

        private void PrintAtk(int i) => Console.WriteLine($"{i}. Атака");

        private void PrintDef(int i) => Console.WriteLine($"{i}. Защита");

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