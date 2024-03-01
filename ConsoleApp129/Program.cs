using System;
//using System.Threading;

namespace ConsoleApp129
{
    internal class Program
    {
        //static bool movingEnabled = true;
        static void Main()
        {
            //Timer time = new Timer(Timer1_Tick, null, 0, 1000);
            Map map = new Map();
            map.GenerateMap();
            ConsoleKeyInfo cki;
            while (true)
            {
                map.DrawMap();
                cki = Console.ReadKey();
                map.MoveHero(cki.Key);
                map.MoveEnemy();
                Console.Clear();
            }
        }
        //private static void Timer1_Tick(object state) => movingEnabled = true;  
    }
}