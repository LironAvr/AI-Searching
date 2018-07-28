using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Interface for a maze generator
    /// defines the following functions:
    /// generate maze
    /// geasureAlgorithmTime (generates a maze and returns the time it took)
    /// </summary>
    interface IMazeGenerator
    {
        AMaze generate(int[] boardSize);
        string geasureAlgorithmTime(int[] boardSize);
    }
}
