using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс Person, наследуемый от MapObject
    ///  объект карты "персонаж"
    /// </summary>
    [Serializable]
    internal class Person : MapObject
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ для отрисовки персонажа на карте
        /// </summary>
        public override char RenderOnMap() => '☺';
    }

    /// <summary>
    ///  Класс Hero, наследуемый от Person
    ///  объект карты "герой"
    /// </summary>
    [Serializable]
    internal class Hero : Person
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки героя на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return base.RenderOnMap();
        }
    }

    /// <summary>
    ///  Класс Enemy, наследуемый от Person
    ///  объект карты "враг"
    /// </summary>
    [Serializable]
    internal class Enemy : Person
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки врага на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return base.RenderOnMap();
        }
    }

    /// <summary>
    ///  Класс Enemy, наследуемый от Person
    ///  объект карты "враг"
    /// </summary>
    [Serializable]
    internal class Annoyer : Person
    {
        private bool _confused = false;
        private int _count = 0;

        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки врага на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return base.RenderOnMap();
        }

        public void GetConfusedTrue() => _confused = true;

        public void GetConfusedFalse() => _confused = false;

        public int GetCount() => _count--;

        public int GetCount(int count) => _count = count;


        public bool ReturnConfused() => _confused;

        public int ReturnCount() => _count;
    }
}