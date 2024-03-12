namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Program
    ///  основной класс программы
    ///  заставляет программу продолжаться после завершения игровой сессии
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Поле Exit() 
        /// является переменной, для продолжения программы после завершения игровой сессии
        /// </summary>
        public static bool Exit = false;

        /// <summary>
        /// Метод Main() 
        /// является входной точкой работы программы
        /// </summary>
        static void Main()
        {
            while (!Exit)
                Menu.ShowMenu();
        }
    }
}