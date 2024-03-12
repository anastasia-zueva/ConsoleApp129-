using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Menu
    ///  начальное меню игры, позволяющее запускать игру
    /// </summary>
    static class Menu
    {
        /// <summary>
        /// Поле _menu
        /// массив с пунктами меню, которые можно выбрать
        /// </summary>
        static private string[] _menu = { "1. начать игру\n","2. последнее сохранение\n", "3. выход\n" };

        /// <summary>
        /// Метод ShowMenu() 
        /// является движком
        /// который проверяет нажатые кнопки и совершает выбранные действия
        /// </summary>
        static public void ShowMenu()
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

        /// <summary>
        /// Метод GetKey() 
        /// является движком 
        /// визуального отображения меню
        /// </summary>
        /// <param name="selectedNum">Номер выбранного пункта в массиве пунктов меню</param>
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

        /// <summary>
        /// Метод StartGame() 
        /// начинает новую игру
        /// </summary>
        static private void StartGame() => new Game();

        /// <summary>
        /// Метод ResumeLastGame() 
        /// загружает сохраненную ранее игру
        /// </summary>
        static private void ResumeLastGame()
        {
            try
            {
                new Game(Map.DeSerialize());
            }
            catch (MyException ex)
            {
                Console.SetCursorPosition(0, 8);
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}