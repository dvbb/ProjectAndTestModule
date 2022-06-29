using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace _2048Game
{
    public static class EventSets
    {
        public static int[,] InitArray(int length)
        {
            int[,] array = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    array[i, j] = 0;
                }
            }
            return array;
        }

        public static void Show(int[,] array)
        {
            Console.Clear();
            int length = (int)Math.Sqrt(array.Length);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write($"{array[i, j]}\t");
                }
                Console.WriteLine();
            }
        }

        public static void NumberAppear(ref int[,] array)
        {
            int length = (int)Math.Sqrt(array.Length);
            List<int> list = new List<int>();

            // find empty grid
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (array[i, j] == 0)
                    {
                        list.Add(i * length + j);
                    }
                }
            }

            Random rand = new Random();
            int[] tempArray = list.ToArray();
            if (list.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Geme over!");
                System.Environment.Exit(0);
            }
            int location = tempArray[rand.Next(list.Count)];
            int row = location / length;
            int column = location % length;
            if (rand.Next(4)==1)    // 25% probability appear 4
            {
                array[row,column] = 2;
            }
            else
            {
                array[row, column] = 4;
            }
        }
    }
}
