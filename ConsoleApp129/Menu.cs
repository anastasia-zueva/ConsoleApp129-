using System;
using System.Collections.Generic;

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
        static private string[] _menu = { "1. начать игру\n", "2. последнее сохранение\n", "3. рекорды\n", "4. выход\n" };

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
                            new Game();
                            tf = false;
                        }
                        else if (selectedNum == 1)
                        {
                            ResumeLastGame();
                            tf = false;
                        }
                        else if (selectedNum == 2)
                        {
                            ShowRecords();
                            tf = false;
                        }
                        else if (selectedNum == 3)
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
        /// Метод ResumeLastGame() 
        /// загружает сохраненную ранее игру
        /// </summary>
        static private void ResumeLastGame()
        {
            try
            {
                new Game(DeSerialize.DeSerializeMap());
            }
            catch (MyException ex)
            {
                Console.SetCursorPosition(0, 8);
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Метод ShowRecords() 
        /// выводит таблицу рекордов
        /// </summary>
        static private void ShowRecords()
        {
            Console.Clear();
            try
            {
                List<Record> rec = DeSerialize.DeSerializeRecords();
                Console.WriteLine($"номер    раунд     всего врагов     осталось врагов     осталось энноеров     победа");
                for (int i = 0; i < rec.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}        {rec[i].ReturnRound()}            {rec[i].ReturnAllEnemys()}                " +
                        $"{rec[i].ReturnAllEnemys() - rec[i].ReturnEnemy()}                {rec[i].ReturnAnnoyer()}                  " +
                        $"{rec[i].ReturnWin()}");
                }
                Console.ReadKey();
            }
            catch (MyException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}