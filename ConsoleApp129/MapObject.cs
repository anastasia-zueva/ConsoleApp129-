using System;

namespace ConsoleApp129
{
    /// <summary>
    ///  Класс MapObject
    ///  абстрактный класс, который используется как наследуемый для всех объектов карты
    ///  создает объекты, которые потом ипользуются на игровой карте
    /// </summary>
    [Serializable]
    public abstract class MapObject
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// абстрактный метод, который возвращает символ и цвет для отрисовки объекта на карте
        /// </summary>
        public abstract char RenderOnMap();
    }

    /// <summary>
    ///  Класс Wall, наследуемый от MapObject
    ///  объект карты "стена"
    /// </summary>
    [Serializable]
    internal class Wall : MapObject
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки стены на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }

    /// <summary>
    ///  Класс Wall, наследуемый от MapObject
    ///  объект карты "стена"
    /// </summary>
    [Serializable]
    internal class Strengthening : MapObject
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки стены на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            return '#';
        }
    }

    /// <summary>
    ///  Класс Field, наследуемый от MapObject
    ///  объект карты "поле"
    /// </summary>
    [Serializable]
    internal class Field : MapObject
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки поля на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }

    /// <summary>
    ///  Класс Tree, наследуемый от MapObject
    ///  объект карты "дерево"
    /// </summary>
    [Serializable]
    internal class Tree : MapObject
    {
        /// <summary>
        /// Метод RenderOnMap()
        /// возвращает символ и цвет для отрисовки дерев на карте
        /// </summary>
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'T';
        }
    }
}