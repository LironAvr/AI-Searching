using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Generates a 3 Dimensional Maze (Using DFS Algorithm)
    /// </summary>
    [Serializable]
    class MyMaze3dGenerator : AMazeGenerator
    {
        private int path = 0;
        private int visitedCells = 0;
        private int row = 0;
        private int col = 0;
        private int floor = 0;
        private int totalCells;
        private bool check = false;
        private bool legalMaze = false;
        private int[,,] maze;
        private Stack cellStack;
        private Position3D current, start, end;

        
        /// <summary>
        /// Checks if the current point is a valid point for a path
        /// </summary>
        /// <returns>bool</returns>
        private bool isValidPoint()
        {
            if (col - 1 < 0 | row - 1 < 0 | this.floor < 0 | col + 1 >= maze.GetLength(2) | row + 1 >= maze.GetLength(1) | this.floor >= maze.GetLength(0))
                return false;
            if (maze[this.floor, row, col] != 1)
                return false;
            if (col - 1 >= 0 && maze[this.floor, row, col - 1] != 1)
                return false;
            if (col + 1 < maze.GetLength(2) && maze[floor, row, col + 1] != 1)
                return false;
            if (row - 1 >= 0 && maze[floor, row - 1, col] != 1)
                return false;
            if (row + 1 < maze.GetLength(1) && maze[floor, row + 1, col] != 1)
                return false;
            if (floor  >= 0 && maze[floor, row , col] != 1)
                return false;
            if (floor < maze.GetLength(0) && maze[floor, row, col] != 1)
                return false;
            return true;
        }
        
        /// <summary>
        /// Initiating the board - walls only
        /// </summary>
        /// <param name="level"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        private void initiateMaze(int level, int height, int width)
        {
            maze = new int[level, height, width];
            for (int l = 0; l < level; l++)
               for (int i = 0; i < height; i++)
                   for (int j = 0; j < width; j++)
                       maze[l, i, j] = 1;
            visitedCells = 0;
            row = 0;
            col = 0;
            floor = 0;
            cellStack = new Stack();
            totalCells = level * height * width;
            legalMaze = false;
        }
        
        /// <summary>
        /// Generates a maze (size is determined by a list of dimensions given as argument)
        /// </summary>
        /// <param name="boardSize"></param>
        /// <returns></returns>
                              
        public override AMaze generate(int[] boardSize)
        {
            if (boardSize.Length < 2)
            {
                //error
                Console.WriteLine("Illegal Dimensions");
            }

            initiateMaze(boardSize[0], boardSize[1], boardSize[2]);
            Random random = new Random();
            row = random.Next(1, boardSize[1] - 1);
            cellStack.Push(new Position3D(floor, row, col));
            start = new Position3D(floor, row, col);
            maze[floor, row, col] = 5;           
            
            while (visitedCells < totalCells * 2)
            {
                check = false;
                int[] order = generateRandomDirections(6);
                for (int i = 0; i < 6; i++)
                {
                    switch (order[i])
                    {
                        case 1: //Left
                            goLeft();
                            break;

                        case 2: //Down
                            goDown();
                            break;

                        case 3: //Right
                            goRight();
                            break;

                        case 4: //Up
                            goUp();
                            break;

                        case 5: //Descend
                            Descend();
                            break;

                        case 6: //Ascend
                            Ascend();
                            break;
                    }
                    //If theres no where to go from the current position
                    //go back to the previous position
                    if (!check)
                    {
                        if (cellStack.Count > 0)
                            current = (Position3D)cellStack.Pop();

                        floor = current.Level;
                        row = current.Row;
                        col = current.Col;
                    }
                }
            }
            if (!legalMaze) return generate(boardSize);
            return new Maze3d(maze, start, end);
        }
        /// <summary>
        /// Tries to go Left from the current position
        /// </summary>
        private void goLeft()
        {
            col -= 2;
            if (isValidPoint())
            {
                maze[floor, row, col] = path;
                maze[floor, row, col + 1] = path;
                check = true;
                current = new Position3D(floor, row, col + 1);
                cellStack.Push(current);
                current = new Position3D(floor, row, col);
                cellStack.Push(current);
            }
            else col += 2;
            visitedCells++;
        }
        /// <summary>
        /// Tries to go Right from the current position
        /// also takes care of lcoating the Goal Position of the maze
        /// </summary>
        private void goRight()
        {
            col += 2;
            //Checks if the position is at the left end of the maze
            //and locating the Goal Position if it is yet to be located
            if (col == maze.GetLength(2) && !legalMaze)
            {
                maze[floor, row, col - 1] = 6;
                maze[floor, row, col - 2] = path;
                end = new Position3D(floor, row, col - 1);
                legalMaze = true;
                return;
            }

            else if (col + 1 == maze.GetLength(2) && !legalMaze)
            {
                maze[floor, row, col] = 6;
                maze[floor, row, col - 1] = path;
                end = new Position3D(floor, row, col);
                legalMaze = true;
                return;
            }

            //Regular Right
            else if (isValidPoint())
            {
                maze[floor, row, col] = path;
                maze[floor, row, col - 1] = path;
                check = true;
                cellStack.Push(new Position3D(floor, row, col - 1));
                current = new Position3D(floor, row, col);
                cellStack.Push(current);
            }

            else col -= 2;
            visitedCells++;
        }
        /// <summary>
        /// Tries going up from the current position
        /// </summary>
        private void goUp()
        {
            row -= 2;
            if (isValidPoint())
            {
                maze[floor, row, col] = path;
                maze[floor, row + 1, col] = path;
                check = true;
                cellStack.Push(new Position3D(floor, row, col));
                current = new Position3D(floor, row + 1, col);
                cellStack.Push(current);
            }

            else row += 2;
            visitedCells++;
        }
        /// <summary>
        /// Tries going Down from the current position
        /// </summary>
        private void goDown()
        {
            row += 2;
            if (isValidPoint())
            {
                maze[floor, row, col] = path;
                maze[floor, row - 1, col] = path;
                check = true;
                cellStack.Push(new Position3D(floor, row - 1, col));
                current = new Position3D(floor, row, col);
                cellStack.Push(current);
            }
            else row -= 2;
            visitedCells++;
        }
        /// <summary>
        /// Tries Ascending (floor wise)
        /// </summary>
        private void Ascend()
        {
            floor++;
            if (isValidPoint())
            {
                maze[floor, row, col] = path;
                check = true;
                current = new Position3D(floor, row, col);
                cellStack.Push(current);
            }

            else floor--;
            visitedCells++;
        }
        /// <summary>
        /// Tries Descending (floor wise)
        /// </summary>
        private void Descend()
        {
            floor--;
            if (isValidPoint())
            {
                maze[floor, row, col] = path;
                check = true;
                current = new Position3D(floor, row, col);
                cellStack.Push(current);
            }

            else floor++;
            visitedCells++;
        }
    }
}
