using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс Person, наследуемый от MapObject
    /// объект карты "персонаж"
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
    /// Класс Hero, наследуемый от Person
    /// объект карты "герой"
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
    /// Класс Enemy, наследуемый от Person
    /// объект карты "враг"
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
    /// Класс Annoyer, наследуемый от Person
    /// объект карты "надоедливый враг"
    /// </summary>
    [Serializable]
    internal class Annoyer : Person
    {
        /// <summary>
        /// Поле _confused
        /// состояние конфуза
        /// </summary>
        private bool _confused = false;

        /// <summary>
        /// Поле _count
        /// тик конфуза
        /// </summary>
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

        /// <summary>
        /// Метод GetConfusedTrue()
        /// вводит гориллу в состояние конфуза
        /// </summary>
        public void GetConfusedTrue() => _confused = true;

        /// <summary>
        /// Метод GetConfusedFalse()
        /// выводит гориллу из состояния конфуза
        /// </summary>
        public void GetConfusedFalse() => _confused = false;

        /// <summary>
        /// Метод GetCount()
        /// уменьшает тики конфуза
        /// </summary>
        public void GetCount() => _count--;

        /// <summary>
        /// Метод GetCount(int count)
        /// устанавливает количество тиков конфуза
        /// </summary>
        /// <param name="count">Количество тиков</param>
        public void GetCount(int count) => _count = count;

        /// <summary>
        /// Метод ReturnConfused()
        /// возвращает состояние гориллы
        /// </summary>
        /// <returns>Состояние конфуза</returns>
        public bool ReturnConfused() => _confused;

        /// <summary>
        /// Метод ReturnCount()
        /// возвращает тик состояния
        /// </summary>
        /// <returns>Тик конфуза</returns>
        public int ReturnCount() => _count;
    }
}