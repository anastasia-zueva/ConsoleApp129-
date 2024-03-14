using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Timers;

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
        /// Поле _time
        /// количество времени до нового раунда
        /// </summary>
        private int _time = 0;

        /// <summary>
        /// Поле _round
        /// количество сыгранных раундов
        /// </summary>
        private int _round = 0;

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
        /// Поле _enemyCount
        /// количество всех врагов, которые были заспавнены
        /// </summary>
        private int _allEnemys = 0;

        /// <summary>
        /// Поле _annoyersCount
        /// количество надоедливых врагов на карте
        /// </summary>
        private int _annoyersCount = 0;

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
        /// Поле _berserk
        /// наличие навыка обезвреживания врагов без боя
        /// </summary>
        private bool _berserk;

        /// <summary>
        ///  Конструктор Map()
        ///  задает размер карты
        /// </summary>
        /// <param name="mapSize">Размер карты</param>
        /// <param name="Time">Счётчик времени</param>
        public Map(int mapSize, Timer Time)
        {
            _map = new MapObject[mapSize, mapSize];
            Time.Elapsed += Timer_Tick;
            _i = _map.GetLength(0)/2;
            _j = _map.GetLength(1)/2;
            ResetTime(Time);
        }

        /// <summary>
        ///  Метод ResetTime()
        ///  обнуляет таймер для отсчета времени до нового раунда
        /// </summary>
        /// <param name="Time">Игровая карта</param>
        public void ResetTime(Timer Time)
        {
            Time.Enabled = true;
            _round++;
            _time = 40;
            _berserk = true;
        }

        /// <summary>
        ///  Метод Timer_Tick()
        ///  каждую секунду отнимает единицу от времени до нового раунда
        /// </summary>
        private void Timer_Tick(object sender, ElapsedEventArgs e) => _time--;

        /// <summary>
        ///  Метод Serialize()
        ///  создает сохранение последней сыгранной игры
        /// </summary>
        /// <param name="_map">Игровая карта</param>
        static public void Serialize(Map _map)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(_file, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, _map);
            stream.Close();
        }

        /// <summary>
        ///  Метод DeSerialize()
        ///  загружает сохранение последней сыгранной игры
        /// </summary>
        /// <returns>Загруженная со сохранения игровая карта</returns>
        static public Map DeSerialize()
        {
            Map _map;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(_file, FileMode.Open, FileAccess.Read);
                _map = (Map)formatter.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                throw new MyException("Ошибка: не удалось загрузить сохранение!");
            }
            return _map;
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

            Spawn(_map.GetLength(0) / 5);
            _map[_map.GetLength(0) / 2, _map.GetLength(1) / 2] = new Hero();
        }


        /// <summary>
        /// Метод AddEnemys()
        /// спавнит новых врагов на карте в рандомных местах
        /// </summary>
        public void Spawn(int a)
        {
            bool annoyer = false;
            while (_enemyCount < a)
            {
                int A = _rand.Next(0, _map.GetLength(0));
                int B = _rand.Next(0, _map.GetLength(1));
                if (_map[A, B] is Field & A != _map.GetLength(0) / 2 & B != _map.GetLength(1) / 2)
                {
                    _map[A, B] = new Enemy();
                    _enemyCount++;
                    _allEnemys++;
                }
            }
            while (!annoyer)
            {
                int A = _rand.Next(0, _map.GetLength(0));
                int B = _rand.Next(0, _map.GetLength(1));
                if (_map[A, B] is Field & A != _map.GetLength(0) / 2 & B != _map.GetLength(1) / 2)
                {
                    _map[A, B] = new Annoyer();
                    annoyer = true;
                    _annoyersCount++;
                }
            }
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

        public void MoveAnnoyer()
        {
            MapObject[,] newMap = new MapObject[_map.GetLength(0), _map.GetLength(1)];
            Array.Copy(_map, newMap, _map.Length);

            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                    if (_map[i, j] is Annoyer)
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
                                if (_i - i > 0 & (newMap[newX + 1, newY] is Field || newMap[newX + 1, newY] is Tree || newMap[newX + 1, newY] is Wall))
                                    newX = i + 1;
                                else if (_i - i < 0 & (newMap[newX - 1, newY] is Field || newMap[newX - 1, newY] is Tree || newMap[newX - 1, newY] is Wall))
                                    newX = i - 1;
                                else if (_j - j > 0 & (newMap[newX, newY + 1] is Field || newMap[newX, newY + 1] is Tree || newMap[newX, newY + 1] is Wall))
                                    newY = j + 1;
                                else if (_j - j < 0 & (newMap[newX, newY - 1] is Field || newMap[newX, newY - 1] is Tree || newMap[newX, newY - 1] is Wall))
                                    newY = j - 1;
                            }
                        }

                        if (newX == i & newY == j)
                        {
                            int direction = _rand.Next(4);

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
                        }

                        if (newMap[newX, newY] is Field)
                        {
                            newMap[newX, newY] = _map[i, j];
                            newMap[i, j] = new Field();
                        }
                        else if (newMap[newX, newY] is Hero)
                            SetLoss(newX, newY);
                    }

            Array.Copy(newMap, _map, _map.Length);
        }

        /// <summary>
        /// Метод MoveEnemy()
        /// передвигает врага на одну клетку в рандомном направлении
        /// </summary>
        public void MoveEnemy(Timer Time)
        {
            MapObject[,] newMap = new MapObject[_map.GetLength(0), _map.GetLength(1)];
            Array.Copy(_map, newMap, _map.Length);

            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                    if (_map[i, j] is Enemy)
                    {
                        int direction = _rand.Next(4);

                        int newX = i, newY = j;
                        if (newX > 0 & newX < 19 & newY > 0 & newY < 19)
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
                        }

                        if (newMap[newX, newY] is Hero)
                        {
                            Time.Enabled = false;
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
                            Time.Enabled = true;
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
        /// <param name="Time">Счётчик времени</param>
        public void MoveHero(ConsoleKey key, Timer Time)
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
                            case ConsoleKey.W:
                                newX = (i - 1 + _map.GetLength(0)) % _map.GetLength(0);
                                break;
                            case ConsoleKey.S:
                                newX = (i + 1) % _map.GetLength(0);
                                break;
                            case ConsoleKey.A:
                                newY = (j - 1 + _map.GetLength(1)) % _map.GetLength(1);
                                break;
                            case ConsoleKey.D:
                                newY = (j + 1) % _map.GetLength(1);
                                break;
                            case ConsoleKey.E:
                                if (_berserk)
                                    for (int x = -1; x < 2; x++)
                                        for (int y = -1; y < 2; y++)
                                            if (newX + x > 0 & newX + x < 24 & newY + y > 0 & newY + y < 24)
                                            {
                                                if (newMap[newX + x, newY + y] is Enemy)
                                                {
                                                    newMap[newX + x, newY + y] = new Field();
                                                    _enemyCount--;
                                                }
                                                else if (newMap[newX + x, newY + y] is Annoyer)
                                                {
                                                    newMap[newX + x, newY + y] = new Field();
                                                    _enemyCount--;
                                                }
                                            }
                                _berserk = false;           
                                break;
                            default:
                                throw new MyException("Ошибка: нажата недопустимая клавиша!");
                        }

                        if (newMap[newX, newY] is Enemy)
                        {
                            Time.Enabled = false;

                            Battle newBattle = new Battle();
                            if (newBattle.GetResults() == 3)
                                SetLoss(i, j);
                            else if (newBattle.GetResults() == 1)
                            {
                                SetField(ref newMap, i, j, newX, newY);
                                _enemyCount--;
                                _i = newX;
                                _j = newY;
                            }

                            if (_enemyCount == 0)
                                End = true;
                            Time.Enabled = true;
                        }
                        else if (newMap[newX, newY] is Field)
                        {
                            SetField(ref newMap, i, j, newX, newY);
                            _i = newX;
                            _j = newY;
                        }
                        else if (newMap[newX, newY] is Annoyer)
                            SetLoss(i, j);
                       
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

        /// <summary>
        ///  Метод ReturnEnemyCount()
        ///  возвращает количество врагов на карте
        /// </summary>
        /// <returns>Количество врагов на карте/returns>
        public int ReturnEnemyCount => _enemyCount;

        /// <summary>
        ///  Метод ReturnAllEnemys()
        ///  возвращает количество всех врагов на карте
        /// </summary>
        /// <returns>Количество всех врагов на карте/returns>
        public int ReturnAllEnemys => _allEnemys;

        /// <summary>
        ///  Метод ReturnTime()
        ///  возвращает оставшееся время в секундах до нового раунда
        /// </summary>
        /// <returns>Оставшееся время в секундах/returns>
        public int ReturnTime => _time;

        /// <summary>
        ///  Метод ReturnRound()
        ///  возвращает номер раунда
        /// </summary>
        /// <returns>Номер раунда/returns>
        public int ReturnRound => _round;

        /// <summary>
        ///  Метод ReturnBerserk()
        ///  возвращает наличие навыка обезвреживания врагов
        /// </summary>
        /// <returns>Наличие навыка обезвреживания врагов/returns>
        public bool ReturnBerserk => _berserk;
    }
}