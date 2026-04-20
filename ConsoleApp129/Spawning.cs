using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Spawning
    ///  для размещения объектов
    /// </summary>
    internal class Spawning
    {
        /// <summary>
        /// Поле _rand
        /// экземпляр класса Random для генерации случайных чисел
        /// </summary>
        static private readonly Random _rand = new Random();

        /// <summary>
        /// Метод GenerateMap()
        /// заполняет массив игровой карты объектами
        /// </summary>
        static public int GenerateMap(ref Map map, ref int I, ref int J)
        {
            for (int i = 0; i < map.MapObj.GetLength(0); i++)
                for (int j = 0; j < map.MapObj.GetLength(1); j++)
                {
                    int A = _rand.Next(0, map.MapObj.GetLength(0) * 4);
                    map.MapObj[i, j] = new Field();

                    if (A < map.MapObj.GetLength(0) / 2)
                        map.MapObj[i, j] = new Wall();
                    else if (A < map.MapObj.GetLength(0) / 1.5)
                        map.MapObj[i, j] = new Tree();
                    else if (A < map.MapObj.GetLength(0) / 1.2)
                        map.MapObj[i, j] = new Strengthening();
                }

            map.MapObj[map.MapObj.GetLength(0) / 2, map.MapObj.GetLength(1) / 2] = new Hero();
            I = map.MapObj.GetLength(0) / 2;
            J = map.MapObj.GetLength(1) / 2;
            Spawn(map.MapObj.GetLength(0) / 5, ref map);
            return _rand.Next(20, 40);
        }

        /// <summary>
        /// Метод StickySpawn()
        /// размещает липкое поле вокруг героя в случайный момент времени
        /// </summary>
        static public int StickySpawn(ref Map map, ref int I, ref int J)
        {
            MapObject[,] newMap = new MapObject[map.MapObj.GetLength(0), map.MapObj.GetLength(1)];
            Array.Copy(map.MapObj, newMap, map.MapObj.Length);

            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    int a = (I + i) % map.MapObj.GetLength(0);
                    int b = (J + j) % map.MapObj.GetLength(1);
                    if (map.MapObj[a, b] is Field)
                        newMap[a, b] = new StickyField();
                }

            Array.Copy(newMap, map.MapObj, map.MapObj.Length);
            return _rand.Next(20, 40);
        }

        /// <summary>
        /// Метод AddEnemies()
        /// размещает новых врагов на карте в случайных местах
        /// </summary>
        static public void Spawn(int a, ref Map map)
        {
            bool annoyer = false;
            while (map.ReturnEnemyCount() < a)
            {
                int A = _rand.Next(0, map.MapObj.GetLength(0));
                int B = _rand.Next(0, map.MapObj.GetLength(1));
                if (map.MapObj[A, B] is Field & A != map.MapObj.GetLength(0) / 2 & B != map.MapObj.GetLength(1) / 2)
                {
                    map.MapObj[A, B] = new Enemy();
                    map.SetEnemyCount(1);
                    map.SetAllEnemyCount();
                }
            }
            while (!annoyer)
            {
                int A = _rand.Next(0, map.MapObj.GetLength(0));
                int B = _rand.Next(0, map.MapObj.GetLength(1));
                if (map.MapObj[A, B] is Field & A != map.MapObj.GetLength(0) / 2 & B != map.MapObj.GetLength(1) / 2)
                {
                    map.MapObj[A, B] = new Annoyer();
                    annoyer = true;
                    map.SetAnnoyerCount(1);
                }
            }
        }
    }
}
