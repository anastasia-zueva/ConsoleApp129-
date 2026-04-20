using System;
using System.Timers;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Movement
    ///  отвечающий за передвижение персонажей
    /// </summary>
    internal class Movement
    {
        /// <summary>
        /// Поле _rand
        /// экземпляр класса Random для генерации случайных чисел
        /// </summary>
        static private readonly Random _rand = new Random();

        /// <summary>
        /// Метод MoveAnnoyer()
        /// передвигает гориллу прямиком за врагом
        /// </summary>
        static public void MoveAnnoyer(ref Map map, ref int I, ref int J)
        {
            MapObject[,] newMap = new MapObject[map.MapObj.GetLength(0), map.MapObj.GetLength(1)];
            Array.Copy(map.MapObj, newMap, map.MapObj.Length);

            for (int i = 0; i < map.MapObj.GetLength(0); i++)
                for (int j = 0; j < map.MapObj.GetLength(1); j++)
                    if (map.MapObj[i, j] is Annoyer annoyer)
                        if (!annoyer.ReturnConfused())
                        {
                            int newX = i, newY = j;
                            if (newX > 0 & newX < 24 & newY > 0 & newY < 24)
                            {
                                if (newMap[newX + 1, newY] is Hero)
                                    newX = i + 1;
                                else if (newMap[newX - 1, newY] is Hero)
                                    newX = i - 1;
                                else if (newMap[newX, newY + 1] is Hero)
                                    newY = j + 1;
                                else if (newMap[newX, newY - 1] is Hero)
                                    newY = j - 1;
                                else if (newMap[newX + 1, newY] is Strengthening)
                                    newX = i + 1;
                                else if (newMap[newX - 1, newY] is Strengthening)
                                    newX = i - 1;
                                else if (newMap[newX, newY + 1] is Strengthening)
                                    newY = j + 1;
                                else if (newMap[newX, newY - 1] is Strengthening)
                                    newY = j - 1;
                                else
                                {
                                    if (I - i > 0 & (newMap[newX + 1, newY] is Field || newMap[newX + 1, newY] is Tree || newMap[newX + 1, newY] is Wall))
                                        newX = i + 1;
                                    else if (I - i < 0 & (newMap[newX - 1, newY] is Field || newMap[newX - 1, newY] is Tree || newMap[newX - 1, newY] is Wall))
                                        newX = i - 1;
                                    else if (J - j > 0 & (newMap[newX, newY + 1] is Field || newMap[newX, newY + 1] is Tree || newMap[newX, newY + 1] is Wall))
                                        newY = j + 1;
                                    else if (J - j < 0 & (newMap[newX, newY - 1] is Field || newMap[newX, newY - 1] is Tree || newMap[newX, newY - 1] is Wall))
                                        newY = j - 1;
                                }
                            }

                            if (newX == i & newY == j)
                            {
                                int direction = _rand.Next(4);

                                switch (direction)
                                {
                                    case 0:
                                        newX = (i - 1 + map.MapObj.GetLength(0)) % map.MapObj.GetLength(0);
                                        break;
                                    case 1:
                                        newX = (i + 1) % map.MapObj.GetLength(0);
                                        break;
                                    case 2:
                                        newY = (j - 1 + map.MapObj.GetLength(1)) % map.MapObj.GetLength(1);
                                        break;
                                    case 3:
                                        newY = (j + 1) % map.MapObj.GetLength(1);
                                        break;
                                }
                            }

                            if (newMap[newX, newY] is Field)
                            {
                                newMap[newX, newY] = map.MapObj[i, j];
                                newMap[i, j] = new Field();
                            }
                            if (newMap[newX, newY] is Tree || newMap[newX, newY] is Wall || newMap[newX, newY] is StickyField)
                            {
                                newMap[newX, newY] = map.MapObj[i, j];
                                newMap[i, j] = new Field();
                                ((Annoyer)newMap[newX, newY]).GetConfusedTrue();
                                ((Annoyer)newMap[newX, newY]).GetCount(3);
                            }
                            else if (newMap[newX, newY] is Hero)
                                SetLoss(newX, newY, ref map);
                            else if (newMap[newX, newY] is Strengthening)
                            {
                                newMap[newX, newY] = new Wall();
                                ((Annoyer)newMap[i, j]).GetConfusedTrue();
                                ((Annoyer)newMap[i, j]).GetCount(3);
                            }
                        }
                        else
                        {
                            if (annoyer.ReturnCount() == 0)
                                annoyer.GetConfusedFalse();
                            annoyer.GetCount();
                        }

            Array.Copy(newMap, map.MapObj, map.MapObj.Length);
        }

        /// <summary>
        /// Метод MoveEnemy()
        /// передвигает врага на одну клетку в случайном направлении
        /// </summary>
        static public void MoveEnemy(Timer Time, ref Map map)
        {
            MapObject[,] newMap = new MapObject[map.MapObj.GetLength(0), map.MapObj.GetLength(1)];
            Array.Copy(map.MapObj, newMap, map.MapObj.Length);

            for (int i = 0; i < map.MapObj.GetLength(0); i++)
                for (int j = 0; j < map.MapObj.GetLength(1); j++)
                    if (map.MapObj[i, j] is Enemy)
                    {
                        int direction = _rand.Next(4);

                        int newX = i, newY = j;
                        if (newX > 0 & newX < 24 & newY > 0 & newY < 24)
                        {
                            if (newMap[newX + 1, newY] is Hero)
                                newX = i + 1;
                            else if (newMap[newX - 1, newY] is Hero)
                                newX = i - 1;
                            else if (newMap[newX, newY + 1] is Hero)
                                newY = j + 1;
                            else if (newMap[newX, newY - 1] is Hero)
                                newY = j - 1;
                        }

                        if (newX == i || newY == j)
                        {
                            switch (direction)
                            {
                                case 0:
                                    newX = (i - 1 + map.MapObj.GetLength(0)) % map.MapObj.GetLength(0);
                                    break;
                                case 1:
                                    newX = (i + 1) % map.MapObj.GetLength(0);
                                    break;
                                case 2:
                                    newY = (j - 1 + map.MapObj.GetLength(1)) % map.MapObj.GetLength(1);
                                    break;
                                case 3:
                                    newY = (j + 1) % map.MapObj.GetLength(1);
                                    break;
                            }
                        }

                        if (newMap[newX, newY] is Hero)
                        {
                            Time.Enabled = false;
                            Battle newBattle = new Battle();
                            if (newBattle.GetResults() == 3)
                                SetLoss(newX, newY, ref map);
                            else if (newBattle.GetResults() == 1)
                            {
                                SetField(ref newMap, newX, newY, i, j);
                                map.SetEnemyCount();
                            }

                            if (map.ReturnEnemyCount() == 0)
                                map.End = true;
                            Time.Enabled = true;
                        }
                        else if (newMap[newX, newY] is Field || newMap[newX, newY] is StickyField)
                            SetField(ref newMap, i, j, newX, newY);
                    }

            Array.Copy(newMap, map.MapObj, map.MapObj.Length);
        }

        /// <summary>
        /// Метод MoveHero()
        /// передвигает героя на одну клетку в направлении, которое зависит от нажатой клавиши
        /// </summary>
        /// <param name="key">Направление передвижения героя</param>
        /// <param name="Time">Счётчик времени</param>
        static public void MoveHero(ConsoleKey key, Timer Time, ref Map map)
        {
            MapObject[,] newMap = new MapObject[map.MapObj.GetLength(0), map.MapObj.GetLength(1)];
            Array.Copy(map.MapObj, newMap, map.MapObj.Length);

            for (int i = 0; i < map.MapObj.GetLength(0); i++)
                for (int j = 0; j < map.MapObj.GetLength(1); j++)
                    if (map.MapObj[i, j] is Hero)
                    {
                        int difX = i, difY = j, newX = i, newY = j;

                        switch (key)
                        {
                            case ConsoleKey.W:
                                newX = (i - 1 + map.MapObj.GetLength(0)) % map.MapObj.GetLength(0);
                                difX = (newX - 1 + map.MapObj.GetLength(0)) % map.MapObj.GetLength(0);
                                break;
                            case ConsoleKey.S:
                                newX = (i + 1) % map.MapObj.GetLength(0);
                                difX = (newX + 1) % map.MapObj.GetLength(0);
                                break;
                            case ConsoleKey.A:
                                newY = (j - 1 + map.MapObj.GetLength(1)) % map.MapObj.GetLength(1);
                                difY = (newY - 1 + map.MapObj.GetLength(1)) % map.MapObj.GetLength(1);
                                break;
                            case ConsoleKey.D:
                                newY = (j + 1) % map.MapObj.GetLength(1);
                                difY = (newY + 1) % map.MapObj.GetLength(1);
                                break;
                            case ConsoleKey.E:
                                if (map.ReturnBerserk())
                                    for (int x = -1; x < 2; x++)
                                        for (int y = -1; y < 2; y++)
                                        {
                                            if (newMap[(newX + x) % map.MapObj.GetLength(0), (newY + y) % map.MapObj.GetLength(1)] is Enemy)
                                            {
                                                newMap[(newX + x) % map.MapObj.GetLength(0), (newY + y) % map.MapObj.GetLength(1)] = new Field();
                                                map.SetEnemyCount();
                                            }
                                            else if (newMap[(newX + x) % map.MapObj.GetLength(0), (newY + y) % map.MapObj.GetLength(1)] is Annoyer)
                                            {
                                                newMap[(newX + x) % map.MapObj.GetLength(0), (newY + y) % map.MapObj.GetLength(1)] = new Field();
                                                map.SetAnnoyerCount();
                                            }
                                            else if (newMap[(newX + x) % map.MapObj.GetLength(0), (newY + y) % map.MapObj.GetLength(1)] is StickyField)
                                                newMap[(newX + x) % map.MapObj.GetLength(0), (newY + y) % map.MapObj.GetLength(1)] = new Field();
                                        }
                                map.SetBerserk();
                                break;
                            default:
                                throw new MyException("Ошибка: нажата недопустимая клавиша!");
                        }

                        if (newMap[newX, newY] is Enemy)
                        {
                            Time.Enabled = false;

                            Battle newBattle = new Battle();
                            if (newBattle.GetResults() == 3)
                                SetLoss(i, j, ref map);
                            else if (newBattle.GetResults() == 1)
                            {
                                SetField(ref newMap, i, j, newX, newY);
                                map.SetEnemyCount();
                                map.I = newX;
                                map.J = newY;
                            }

                            if (map.ReturnEnemyCount() == 0)
                                map.End = true;
                            Time.Enabled = true;
                        }
                        else if (newMap[newX, newY] is Field)
                        {
                            SetField(ref newMap, i, j, newX, newY);
                            map.I = newX;
                            map.J = newY;
                        }
                        else if (newMap[newX, newY] is Annoyer)
                            SetLoss(i, j, ref map);
                        else if (newMap[newX, newY] is Tree || newMap[newX, newY] is Wall)
                        {
                            if (newMap[difX, difY] is Field)
                            {
                                newMap[difX, difY] = map.MapObj[newX, newY];
                                SetField(ref newMap, i, j, newX, newY);
                                map.I = newX;
                                map.J = newY;
                            }
                        }
                        else if (newMap[newX, newY] is Strengthening)
                            newMap[newX, newY] = new Wall();
                    }

            Array.Copy(newMap, map.MapObj, map.MapObj.Length);
        }

        /// <summary>
        /// Метод SetField()
        /// передвигает персонажа на одну клетку 
        /// </summary>
        /// <param name="newMap">Игровая карта представленная в виде массива</param>
        /// <param name="i">Номер строки, в которой находился персонаж 1 ход назад</param>
        /// <param name="j">Номер ряда, в котором находился персонаж 1 ход назад</param>
        /// <param name="newX">Номер строки, в которой находится персонаж</param>
        /// <param name="newY">Номер ряда, в котором находится персонаж</param>
        static private void SetField(ref MapObject[,] map, int i, int j, int newX, int newY) => (map[i, j], map[newX, newY]) = (new Field(), map[i, j]);

        /// <summary>
        /// Метод SetLoss()
        /// записывает, в какой строке и ряду находится герой
        /// завершает игру
        /// </summary>
        /// <param name="i">Номер строки, в которой находится герой</param>
        /// <param name="j">Номер ряда, в котором находится герой</param>
        static private void SetLoss(int i, int j, ref Map map)
        {
            map.SetLoss();
            (map.I, map.J) = (i, j);
        }
    }
}
