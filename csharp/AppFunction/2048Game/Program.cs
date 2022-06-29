using System;
using static _2048Game.EventSets;

namespace _2048Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int length = 4;
            int[,] array = InitArray(length);
            while (true)
            {
                Show(array);
                string key = (Console.ReadKey()).Key.ToString();
                NumberAppear(ref array);
                switch (key)
                {
                    case "S":
                    case "DownArrow":
                        Console.WriteLine("down Arrow event");
                        break;
                    case "W":
                    case "UpArrow":
                        Console.WriteLine("upArrow Arrow event");
                        break;
                    case "A":
                    case "LeftArrow":
                        Console.WriteLine("LeftArrow Arrow event");
                        break;
                    case "D":
                    case "RightArrow":
                        Console.WriteLine("RightArrow Arrow event");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
