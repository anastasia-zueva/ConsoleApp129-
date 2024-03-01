using System;

namespace ConsoleApp129
{
    internal class Person : MapObject
    {
        int pointX;
        int pointY;

        public Person(int X,int Y)
        {
            pointX = X; pointY = Y; 
        }
        public override char RenderOnMap()
        {
            return '☺';
        }
    }

    internal class Hero : Person
    {
        public Hero(int X, int Y) : base(X, Y)
        {

        }
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return base.RenderOnMap();
        }
    }

    internal class Enemy : Person
    {
        public Enemy(int X, int Y) : base(X, Y)
        {

        }
        public override char RenderOnMap()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return base.RenderOnMap();
        }
    }
}