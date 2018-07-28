using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Randomly generates a 2 Dimensional Maze
    /// </summary>
    class SimpleMaze2dGenerator : AMazeGenerator
    {
        private int[,] maze;
        private Position2D end;

        public override AMaze generate(int[] boardSize)
        {
            if (boardSize.Length < 2)
            {
                //error
                Console.WriteLine("Illegal size ");                
            }
            int height = boardSize[0];
            int width = boardSize[1];
            Random rnd = new Random();
            Position2D start = new Position2D(rnd.Next(1, height - 1), 0);
            maze = new int[height, width];
            generateFrame();
            maze[start.Row, start.Col] = 5;
            maze[start.Row, start.Col + 1] = 2;
            generatePath(start.Col + 1, start.Row);
            generateWalls();
            Maze2d maze2d = new Maze2d(maze, start, end);
            return maze2d;
        }

        /// <summary>
        /// create the board frame
        /// </summary>
        private void generateFrame()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == maze.GetLength(0)-1 || j == maze.GetLength(1)-1)
                    {
                        maze[i, j] = 10;                                        
                    }                   
                }
            }
        }
        /// <summary>
        /// Recursive function that generate a path. if the currnt point is next to the farme set it as a end point and return. 
        /// </summary>
        /// <param name="currX"> the currnt X value </param>
        /// <param name="currY"> the currnt Y value </param>
        private void generatePath(int currX, int currY)
        {
            //  if the currnt point is next to the farme
            if (currX == maze.GetLength(1) - 2)
            {
                currX++;
                maze[currY, currX] = 6;
                end = new Position2D(currY, currX);
                return;
            }

            if (currY == maze.GetLength(0) - 2)
            {
                currY++;
                maze[currY, currX] = 6;
                end = new Position2D(currY, currX);
                return;
            }

            //call to function that generate a random next 4 steps
            int[] randSteps = generateRandomDirections(4);
            bool found = false;
            //stop when the next legal step if found
            for (int i = 0; i < 4 && !found; i++)
            {
                switch (randSteps[i])//nextStep
                {
                    //up
                    case 1:
                        if (currY > 0 && (maze[currY - 1, currX] == 0 || maze[currY - 1, currX] == 2))
                        {
                            currY--;
                            maze[currY, currX] = 2;
                            found = true;
                        }
                        break;
                    //right
                    case 2:
                        if (currX < maze.GetLength(1) - 1 && (maze[currY, currX + 1] == 0 || maze[currY, currX + 1] == 2))
                        {
                            currX++;
                            maze[currY, currX] = 2;
                            found = true;
                        }
                        break;
                    //down
                    case 3:
                        if (currY < maze.GetLength(0) - 1 && (maze[currY + 1, currX] == 0 || maze[currY + 1, currX] == 2))
                        {
                            currY++;
                            maze[currY, currX] = 2;
                            found = true;
                        }
                        break;
                    //left
                    case 4:
                        if (currX > 0 && (maze[currY, currX - 1] == 0 || maze[currY, currX - 1] == 2))
                        {
                            currX--;
                            maze[currY, currX] = 2;
                            found = true;
                        }
                        break;
                }
            }
            generatePath(currX, currY);
        }

        /// <summary>
        /// For each cell that is not part of the path solution this function generates a wall in probability 0.6
        /// </summary>
        private void generateWalls()
        {
            Random random = new Random();
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == 0)
                    {
                        double probToWall = random.NextDouble();
                        if (probToWall <= 0.6)
                        {
                            maze[i, j] = 1;
                        }
                    }
                }
            }
        }

    }
}

