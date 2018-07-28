using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Represents a 3 Dimensional Maze
    /// </summary>
    [Serializable]
    class Maze3d : AMaze
    {
        private int[,,] m_maze;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maze">maze board</param>
        /// <param name="start">start position</param>
        /// <param name="end">goal position</param>
        public Maze3d(int[,,] maze, Position3D start, Position3D end) : base(start, end)
        {
            m_maze = maze;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="byteArray">array of bytes-represents a maze</param>
        public Maze3d(byte[] byteArray)
        {
            Position3D start = null;
            Position3D end = null;
            int level = (byteArray.Length - 2) / (byteArray[0] * byteArray[1]);
            m_maze = new int[level, byteArray[1], byteArray[0]];
            int i = 2;
            for (int l = 0; l < m_maze.GetLength(0); l++)
            {
                for (int row = 0; row < m_maze.GetLength(1); row++)
                {
                    for (int col = 0; col < m_maze.GetLength(2); col++)
                    {
                        m_maze[l, row, col] = Convert.ToInt16(byteArray[i]);
                        if (m_maze[l, row, col] == 5)//0 && col == 0)
                        {
                            start = new Position3D(l, row, col);
                            //m_maze[l, row, col] = 5;
                        }
                        else if (m_maze[l, row, col] == 6)//0 && col == m_maze.GetLength(2) - 1)
                        {
                            end = new Position3D(l, row, col);
                            //m_maze[l, row, col] = 6;
                        }
                        i++;
                    }
                }
            }
            setStartEndPosition(start, end);
        }

        /// <summary>
        /// Prints the Maze - Floor by Floor (Each floor is a printed as a 2D maze)
        /// </summary>
        public override void print()
        {
            for (int level = 0; level < getLevels(); level++)
            {
                Console.Write("    ");
                for (int row = 0; row < getWidth(); row++)
                {
                    if (row < 10)
                        Console.Write(" 0" + row + " ");
                    else Console.Write(" " + row + " ");
                }

                Console.WriteLine();
                for (int row = 0; row < getHeight(); row++)
                {
                    if (row < 10)
                        Console.Write(" 0" + row + " ");
                    else Console.Write(" " + row + " ");
                    for (int col = 0; col < getWidth(); col++)
                    {
                        if (getCell(level, row, col) == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("****");
                            Console.ResetColor();
                        }
                        else if (getCell(level, row, col) == 0)
                        {
                            Console.Write("    ");
                        }
                        else if (getCell(level, row, col) == 5)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" ST ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" EN ");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// checks if an obj represents the same maze
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>true if the mazes equals, else- false</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Maze3d)) return false;
            Maze3d other = (Maze3d)obj;
            for (int level = 0; level < m_maze.GetLength(0); level++)
            {
                for (int row = 0; row < m_maze.GetLength(1); row++)
                {
                    for (int col = 0; col < m_maze.GetLength(2); col++)
                    {
                        if (m_maze[level, row, col] != other.m_maze[level, row, col])
                            return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns the Height of the maze (Number of Rows)
        /// </summary>
        /// <returns> int </returns>
        public override int getHeight()
        {
            return m_maze.GetLength(1);
        }

        /// <summary>
        /// Returns the Width of the maze (Number of Columns)
        /// </summary>
        /// <returns> int </returns>
        public override int getWidth()
        {
            return m_maze.GetLength(2);
        }

        /// <summary>
        /// Returns the Number of levels of the maze
        /// </summary>
        /// <returns> int </returns>
        public int getLevels()
        {
            return m_maze.GetLength(0);
        }

        /// <summary>
        /// Gets the value of a specific cell
        /// </summary>
        /// <param name="z">floor</param>
        /// <param name="y">row</param>
        /// <param name="x">column</param>
        /// <returns> int </returns>
        public override int getCell(int z, int y, int x)
        {
            return m_maze[z, y, x];
        }

        /// <summary>
        /// Checks if a specific cell is a path
        /// </summary>
        /// <param name="x">column</param>
        /// <param name="y">row</param>
        /// <param name="z">floor</param>
        /// <returns> True for Path, False for wall</returns>
        public bool isPath(int x, int y, int z)
        {
            return (m_maze[z, y, x] != 1);
        }

        /// <summary>
        /// Change the representation of 3D maze to array of bytes
        /// </summary>
        /// <returns>bytes array that representation 3d maze</returns>
        public byte[] toByteArray()
        {
            byte[] byteArray = new byte[m_maze.Length + 2];
            byteArray[0] = Convert.ToByte(m_maze.GetLength(2));
            byteArray[1] = Convert.ToByte(m_maze.GetLength(1));
            int i = 2;
            for (int level = 0; level < m_maze.GetLength(0); level++)
            {
                for (int row = 0; row < m_maze.GetLength(1); row++)
                {
                    for (int col = 0; col < m_maze.GetLength(2); col++)
                    {
                        byteArray[i] = Convert.ToByte(m_maze[level, row, col]);
                        i++;
                    }
                }
            }
            return byteArray;
        }
        /// <summary>
        /// get the 3d array
        /// </summary>
        /// <returns>3d array maze</returns>
        public int[,,] getMaze()
        {
            return m_maze;
        }
    }
}
