using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Menu
    ///  карта, на которой происходят все события игры
    /// </summary>
    [Serializable]
    public class Map
    {
        /// <summary>
        /// Поле _rand
        /// экземпляр класса Random для генерации рандомных чисел
        /// </summary>
        static private Random _rand = new Random();

        /// <summary>
        /// Поле _map
        /// игровая карта, представленная в виде двумерного массива
        /// </summary>
        public MapObject[,] _map;

        /// <summary>
        /// Поле _loss
        /// является переменной отслеживания проигрыша
        /// </summary>
        private bool _loss = false;

        /// <summary>
        /// Поле End
        /// является переменной отслеживания завершения игры
        /// </summary>
        public bool End = false;

        /// <summary>
        /// Поле _i
        /// номер строки в массиве игровой карты, на которой находится герой
        /// </summary>
        private int _i;

        /// <summary>
        /// Поле _j
        /// номер ряда в массиве игровой карты, на котором находится герой
        /// </summary>
        private int  _j;

        /// <summary>
        /// Поле _enemyCount
        /// количество врагов на карте
        /// </summary>
        private int _enemyCount = 0;

        /// <summary>
        /// Поле _file
        /// путь к файлу сохранения
        /// </summary>
        static private readonly string _file = "save1.txt";

        /// <summary>
        /// Поле _death
        /// символ для отрисовки смерти врага при проигрыше
        /// </summary>
        static private readonly char _death = 'X';

        /// <summary>
        ///  Конструктор Map()
        ///  задает размер карты
        /// </summary>
        /// <param name="mapSize">Размер карты</param>
        public Map(int mapSize) => _map = new MapObject[mapSize, mapSize];

        /// <summary>
        ///  Метод Serialize()
        ///  создает сохранение последней сыгранной игры
        /// </summary>
        /// <param name="map">Игровая карта</param>
        static public void Serialize(Map map)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(_file, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, map);
            stream.Close();
        }

        /// <summary>
        ///  Метод DeSerialize()
        ///  загружает сохранение последней сыгранной игры
        /// </summary>
        /// <returns>Загруженная со сохранения игровая карта</returns>
        static public Map DeSerialize()
        {
            Map map;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(_file, FileMode.Open, FileAccess.Read);
                map = (Map)formatter.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                throw new MyException("Ошибка: не удалось загрузить сохранение!");
            }
            return map;
        }

        /// <summary>
        /// Метод GenerateMap()
        /// заполняет массив игровой карты объектами
        /// </summary>
        public void GenerateMap()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    int A = _rand.Next(0, _map.GetLength(0) * 4);
                    _map[i, j] = new Field();

                    if (A < 1)
                    {
                        _map[i, j] = new Enemy();
                        _enemyCount++;
                    }
                    else if (A < _map.GetLength(0) / 2)
                        _map[i, j] = new Wall();
                    else if (A < _map.GetLength(0) / 1.5)
                        _map[i, j] = new Tree();
                }

            while (_enemyCount < _map.GetLength(0) / 5)
            {
                int A = _rand.Next(0, _map.GetLength(0));
                int B =_rand.Next(0, _map.GetLength(1));
                _map[A, B] = new Enemy();
                _enemyCount++;
            }

            _map[_map.GetLength(0) / 2, _map.GetLength(1) / 2] = new Hero();
        }

        /// <summary>
        /// Метод DrawMap()
        /// отрисовывает игровую карту в консоли
        /// </summary>
        public void DrawMap()
        {
            if (!_loss)
                for (int i = 0; i < _map.GetLength(0); i++)
                {
                    for (int j = 0; j < _map.GetLength(1); j++)
                    {
                        Console.Write(_map[i, j].RenderOnMap() + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
            else
            {
                for (int i = 0; i < _map.GetLength(0); i++)
                {
                    for (int j = 0; j < _map.GetLength(1); j++)
                    {
                        if (_i == i & _j == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(_death + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(_map[i, j].RenderOnMap() + " ");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }
                End = true;
            }
        }

        /// <summary>
        /// Метод MoveEnemy()
        /// передвигает врага на одну клетку в рандомном направлении
        /// </summary>
        public void MoveEnemy()
        {
            MapObject[,] newMap = new MapObject[_map.GetLength(0), _map.GetLength(1)];
            Array.Copy(_map, newMap, _map.Length);

            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                    if (_map[i, j] is Enemy)
                    {
                        int direction = _rand.Next(4);

                        int newX = i, newY = j;
                        switch (direction)
                        {
                            case 0:
                                newX = (i - 1 + _map.GetLength(0)) % _map.GetLength(0);
                                break;
                            case 1:
                                newX = (i + 1) % _map.GetLength(0);
                                break;
                            case 2:
                                newY = (j - 1 + _map.GetLength(1)) % _map.GetLength(1);
                                break;
                            case 3:
                                newY = (j + 1) % _map.GetLength(1);
                                break;
                        }

                        if (newMap[newX, newY] is Hero)
                        {
                            Battle newBattle = new Battle();
                            if (newBattle.GetResults() == 3)
                                SetLoss(newX, newY);
                            else if (newBattle.GetResults() == 1)
                            {
                                SetField(ref newMap, newX, newY, i, j);
                                _enemyCount--;
                            }

                            if (_enemyCount == 0)
                                End = true;
                        }
                        else if (newMap[newX, newY] is Field)
                            SetField(ref newMap, i, j, newX, newY);
                    }

            Array.Copy(newMap, _map, _map.Length);
        }

        /// <summary>
        /// Метод MoveEnemy()
        /// передвигает героя на одну клетку в направлении, которое зависит от нажатой клавиши
        /// </summary>
        /// <param name="key">Направление передвижения героя</param>
        public void MoveHero(ConsoleKey key)
        {
            MapObject[,] newMap = new MapObject[_map.GetLength(0), _map.GetLength(1)];
            Array.Copy(_map, newMap, _map.Length);

            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                    if (_map[i, j] is Hero)
                    {
                        int newX = i, newY = j;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                newX = (i - 1 + _map.GetLength(0)) % _map.GetLength(0);
                                break;
                            case ConsoleKey.DownArrow:
                                newX = (i + 1) % _map.GetLength(0);
                                break;
                            case ConsoleKey.LeftArrow:
                                newY = (j - 1 + _map.GetLength(1)) % _map.GetLength(1);
                                break;
                            case ConsoleKey.RightArrow:
                                newY = (j + 1) % _map.GetLength(1);
                                break;
                            default:
                                throw new MyException("Ошибка: нажата недопустимая клавиша!");
                        }

                        if (newMap[newX, newY] is Enemy)
                        {
                            Battle newBattle = new Battle();
                            if (newBattle.GetResults() == 3)
                                SetLoss(i, j);
                            else if (newBattle.GetResults() == 1)
                            {
                                SetField(ref newMap, i, j, newX, newY);
                                _enemyCount--;
                            }

                            if (_enemyCount == 0)
                                End = true;
                        }
                        else if (newMap[newX, newY] is Field)
                            SetField(ref newMap, i, j, newX, newY);
                    }
            Array.Copy(newMap, _map, _map.Length);
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
        private void SetField(ref MapObject[,] newMap, int i, int j, int newX, int newY) => (newMap[i, j], newMap[newX, newY]) = (new Field(), _map[i, j]);

        /// <summary>
        /// Метод SetLoss()
        /// записывает, в какой строке и ряду находится герой
        /// завершает игру
        /// </summary>
        /// <param name="i">Номер строки, в которой находится герой</param>
        /// <param name="j">Номер ряда, в котором находится герой</param>
        private void SetLoss(int i, int j) => (_loss, _i, _j) = (true, i, j);

        /// <summary>
        /// Метод GetEnemyCount()
        /// выводит количество оставшихся врагов после проигрыша или окончания игры
        /// </summary>
        public void GetEnemyCount()
        {
            Console.WriteLine("\n");
            Console.SetCursorPosition(0, _map.GetLength(0));
            Console.WriteLine($"количество оставшихся врагов: {_enemyCount}");
            Console.SetCursorPosition(0, _map.GetLength(0) + 1);

            if (_enemyCount == 0 & End)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Ты выиграл!");
            }
            else if (_enemyCount > 0 & End)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Ты проиграл!");
            }
        }
    }
}