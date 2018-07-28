using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Represents a 2 Dimensional Maze
    /// </summary>
    [Serializable]
    class Maze2d : AMaze
    {
        private int[,] m_maze;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Maze2d (int[,] maze, Position2D start, Position2D end) : base(start, end)
        {
            m_maze = maze ;     
        }

        /// <summary>
        /// Returns the Height of the maze (Number of Rows)
        /// </summary>
        /// <returns> int </returns>
        public override int getHeight()
        {
            return m_maze.GetLength(0);
        }

        /// <summary>
        /// Returns the Width of the maze (Number of Columns)
        /// </summary>
        /// <returns> int </returns>
        public override int getWidth()
        {
            return m_maze.GetLength(1);
        }

        /// <summary>
        /// Prints the Maze
        /// </summary>
        public override void print()
        {
            for (int i = 0; i < getHeight(); i = i + 1)
            {
                for (int j = 0; j < getWidth(); j = j + 1)
                {
                    //wall
                    if (getCell(i, j) == 1 | getCell(i, j) == 10)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("***");
                        Console.ResetColor();
                    }
                    //end point
                    else if ( i == endPoint.Row && j == endPoint.Col )
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" E ");
                        Console.ResetColor();                        
                    }
                    //start point
                    else if ( i == startPoint.Row && j == startPoint.Col )
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" S ");
                        Console.ResetColor();
                    }

                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Gets the value of a specific cell
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns> int </returns>
        public override int getCell(int y, int x, int z = 0)
        {
            return m_maze[y, x];
        }
    }
}
