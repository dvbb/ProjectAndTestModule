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
                switch (key)
                {
                    case "S":
                    case "DownArrow":
                        DownArrowEvent(ref array);
                        break;
                    case "W":
                    case "UpArrow":
                        UpArrowEvent(ref array);
                        break;
                    case "A":
                    case "LeftArrow":
                        LeftArrowEvent(ref array);
                        break;
                    case "D":
                    case "RightArrow":
                        RightArrowEvent(ref array);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
