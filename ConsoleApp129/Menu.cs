using System;

namespace ConsoleApp129
{
    internal class Menu
    {
        static private string[] _menu = { "1. начать игру\n","2. последнее сохранение\n", "3. выход\n" };

        public Menu() => ShowMenu();

        public void ShowMenu()
        {
            Console.Clear();
            int selectedNum = 0;
            bool tf = true;
            GetKey(selectedNum);
            Console.SetCursorPosition(0, _menu.Length);
            ConsoleKeyInfo a;
            while (tf)
            {
                a = Console.ReadKey();
                switch (a.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedNum > 0)
                            selectedNum--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedNum < _menu.Length - 1)
                            selectedNum++;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedNum == 0)
                        {
                            StartGame();
                            tf = false;
                        }
                        else if (selectedNum == 1)
                        {
                            ResumeLastGame();
                            tf = false;
                        }
                        else if (selectedNum == 2)
                        {
                            tf = false;
                            Program.Exit = true;
                        }
                        break;
                    default:
                        break;
                }
                GetKey(selectedNum);
            }
        }

        static private void GetKey(int selectedNum)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < _menu.Length; i++)
                if (i == selectedNum)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_menu[i]);
                    Console.ResetColor();
                }
                else
                    Console.Write(_menu[i]);
        }

        static private void StartGame() => new Game();

        static private void ResumeLastGame() => new Game(Map.DeSerialize());
    }
}