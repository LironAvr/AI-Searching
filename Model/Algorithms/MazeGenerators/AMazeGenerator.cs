using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    abstract class AMazeGenerator : IMazeGenerator
    {
        /// <summary>
        /// Generates a maze using boardSize for its dimensions
        /// </summary>
        /// <param name="boardSize">
        /// boardSize[0] = Number of levels
        /// boardSize[1] = Number of rows
        /// boardSize[2] = Number of columns</param>
        /// <returns></returns>
        public abstract AMaze generate(int[] boardSize);

        /// <summary>
        /// return the time is take to generate a maze
        /// </summary>
        /// <param name="boardSize"></param>
        /// <returns></returns>
        public string geasureAlgorithmTime(int[] boardSize)
        {
            DateTime start = DateTime.Now;
            generate(boardSize);
            DateTime end = DateTime.Now;
            TimeSpan time = end - start;
            return "Maze Generating Time: " + time.ToString();
        }

        /// <summary>
        /// generate a random array with numbers 1 to size 
        /// </summary>
        /// <param name="size"></param>
        /// <returns> the array </returns>
        protected int[] generateRandomDirections(int size)
        {
            Random r = new Random();

            // Filling the directions by random moves from 1 -> size
            int[] randoms = new int[size];
            for (int i = 0; i < size; i++)
                randoms[i] = i + 1;

            // Shuffling the array
            for (int i = randoms.Length - 1; i > 0; i--)
            {
                int index = Utils.random.Next(i+1);//r.Next(i + 1);
                // Simple swap
                int a = randoms[index];
                randoms[index] = randoms[i];
                randoms[i] = a;
            }
            return randoms;
        }
        /// <summary>
        /// helping class to the random function
        /// </summary>
        public static class Utils
        {
            public static readonly Random random = new Random();
        }
    }
}
