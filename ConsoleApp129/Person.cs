using System;

namespace ConsoleApp129
{
    [Serializable]
    internal class Person : MapObject
    {

        public Person() { }
        public override char RenderOnMap()
        {
            return '☺';
        }
    }

    [Serializable]
    internal class Hero : Person
    {
        public Hero() { }
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return base.RenderOnMap();
        }
    }

    [Serializable]
    internal class Enemy : Person
    {
        public Enemy() { }
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return base.RenderOnMap();
        }
    }
}