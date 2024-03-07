using System;

namespace ConsoleApp129
{
    [Serializable]
    public abstract class MapObject
    {
        public abstract char RenderOnMap();
    }

    [Serializable]
    internal class Wall : MapObject
    {
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }

    [Serializable]
    internal class Field : MapObject
    {
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }

    [Serializable]
    internal class Tree : MapObject
    {
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'T';
        }
    }
}