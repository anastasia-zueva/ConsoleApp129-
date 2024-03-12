using System;

namespace ConsoleApp129
{
    internal class MyException : ApplicationException
    {
        public MyException(string message) : base(message) { }
    }
}