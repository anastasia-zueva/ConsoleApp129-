using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс MyException, наследуемый от ApplicationException
    ///  создает собственные исключения
    /// </summary>
    internal class MyException : ApplicationException
    {
        /// <summary>
        /// Конструктор MyException() 
        /// является входной точкой работы программы
        /// </summary>
        /// <param name="message">Выводящаяся ошибка</param>
        public MyException(string message) : base(message) { }
    }
}