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
}