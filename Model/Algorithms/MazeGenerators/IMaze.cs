using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Maze interface
    /// Defines the following functions:
    /// Print the maze
    /// get the start position of the maze
    /// get the goal (end) position of a maze
    /// </summary>
    interface IMaze
    {
        void print();
        APosition getStartPosition();
        APosition getGoalPosition();
    }
}
