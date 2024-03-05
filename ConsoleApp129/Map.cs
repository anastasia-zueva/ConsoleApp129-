using System;

namespace ConsoleApp129
{
    internal class Map
    {
        private Random _rand = new Random();
        private MapObject[,] _map;
        private bool _loss = false;
        public bool End = false;
        private int _i, _j;
        private int _enemyCount = 0;

        public Map(int mapSize) => _map = new MapObject[mapSize, mapSize];

        public void GenerateMap()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    int A = _rand.Next(0, _map.GetLength(0) * 4);
                    _map[i, j] = new Field();

                    if (A < 1)
                    {
                        _map[i, j] = new Enemy(i, j);
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
                _map[A, B] = new Enemy(A, B);
                _enemyCount++;
            }

            _map[_map.GetLength(0) / 2, _map.GetLength(1) / 2] = new Hero(_map.GetLength(0) / 2, _map.GetLength(1) / 2);
        }

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
                            Console.Write("X ");
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
                            else if (newBattle.GetResults() == 2)
                            {
                                bool tf = true;
                                for (int x = 0; x < 3; x++)
                                {
                                    if (tf)
                                        for (int y = 0; y < 3; y++)
                                            if (_map[i - 1 + x, j - 1 + x] is Field)
                                            {
                                                _map[i - 1 + x, j - 1 + x] = new Enemy(i - 1 + x, j - 1 + x);
                                                _map[i, j] = new Field();
                                                tf = false;
                                                break;
                                            }
                                            else { }
                                    else
                                        break;
                                }
                            }
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
                        }

                        if (newMap[newX, newY] is Enemy)
                        {
                            Battle newBattle = new Battle();
                            if (newBattle.GetResults() == 3)
                                SetLoss(i, j);
                            else if (newBattle.GetResults() == 2)
                            {
                                bool tf = true;
                                for (int x = 0; x < 3; x++)
                                {
                                    if (tf)
                                        for (int y = 0; y < 3; y++)
                                            if (_map[i - 1 + x, j - 1 + x] is Field)
                                            {
                                                _map[i - 1 + x, j - 1 + x] = new Hero(i - 1 + x, j - 1 + x);
                                                _map[i, j] = new Field();
                                                tf = false;
                                                break;
                                            }
                                            else { }
                                    else
                                        break;
                                }
                            }
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

        private void SetField(ref MapObject[,] newMap, int i, int j, int newX, int newY) => (newMap[i, j], newMap[newX, newY]) = (new Field(), _map[i, j]);

        private void SetLoss(int i, int j) => (_loss, _i, _j) = (true, i, j);

        public void GetEnemyCount()
        {
            Console.WriteLine("\n");
            Console.SetCursorPosition(0, _map.GetLength(0));
            Console.WriteLine($"количество оставшихся врагов: {_enemyCount}");
            Console.SetCursorPosition(0, _map.GetLength(0) + 1);

            if (_enemyCount == 0 & End)
            {
                Console.Clear();
                Console.WriteLine("Ты выиграл!");
            }
            else if (_enemyCount > 0 & End)
            {
                Console.Clear();
                Console.WriteLine("Ты проиграл!");
            }
        }
    }
}