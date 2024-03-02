using System;
using System.Data;

namespace ConsoleApp129
{
    internal class Map
    {
        private Random _rand = new Random();
        private MapObject[,] _map = new MapObject[25, 25];
        private bool _loss = false;
        public bool End = false;
        private int _i, _j;

        public void GenerateMap()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    int A = _rand.Next(100);
                    _map[i, j] = new Field();

                    if (A > 1 && A < 6)
                        _map[i, j] = new Wall();
                    else if (A < 1)
                        _map[i, j] = new Enemy(i, j);
                    else if (A > 5 && A < 10)
                        _map[i, j] = new Tree();
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
                            newMap[newX, newY] = _map[i, j];
                            SetLoss(newX, newY);
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
                            SetLoss(i, j);
                        else if (newMap[newX, newY] is Field)
                            SetField(ref newMap, i, j, newX, newY);
                    }

            Array.Copy(newMap, _map, _map.Length);
        }

        private void SetField(ref MapObject[,] newMap, int i, int j, int newX, int newY) => (newMap[i, j], newMap[newX, newY]) = (new Field(), _map[i, j]);

        private void SetLoss(int i, int j) => (_loss, _i, _j) = (true, i, j);
    }
}