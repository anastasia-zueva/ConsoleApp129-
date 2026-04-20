using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Drawing
    ///  для отрисовки карты
    /// </summary>
    internal class Drawing
    {
        /// <summary>
        /// Поле _death
        /// символ для отрисовки смерти врага при проигрыше
        /// </summary>
        static private readonly char _death = 'X';

        /// <summary>
        /// Метод DrawMap()
        /// показывает игровую карту в консоли
        /// </summary>
        static public void DrawMap(ref Map map)
        {
            if (!map.ReturnLoss())
                for (int i = 0; i < map.MapObj.GetLength(0); i++)
                {
                    for (int j = 0; j < map.MapObj.GetLength(1); j++)
                    {
                        Console.Write(map.MapObj[i, j].RenderOnMap() + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
            else
            {
                for (int i = 0; i < map.MapObj.GetLength(0); i++)
                {
                    for (int j = 0; j < map.MapObj.GetLength(1); j++)
                    {
                        if (map.I == i & map.J == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(_death + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(map.MapObj[i, j].RenderOnMap() + " ");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }
                map.End = true;
            }
        }

        /// <summary>
        /// Метод GetEnemyCount()
        /// выводит количество оставшихся врагов после проигрыша или окончания игры
        /// </summary>
        static public void GetEnemyCount(ref Map map)
        {
            Console.WriteLine("\n");
            Console.SetCursorPosition(0, map.MapObj.GetLength(0));
            Console.WriteLine($"количество оставшихся врагов: {map.ReturnEnemyCount()}");
            Console.WriteLine($"количество особых врагов: {map.ReturnAnnoyerCount()}");
            Console.SetCursorPosition(0, map.MapObj.GetLength(0) + 1);

            if (map.ReturnEnemyCount() == 0 & map.End)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Ты выиграл!");
            }
            else if (map.ReturnEnemyCount() > 0 & map.End)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Ты проиграл!");
            }
        }
    }
}
