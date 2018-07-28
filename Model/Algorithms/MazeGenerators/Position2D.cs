using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Represents a 2 Dimensional Position (X,Y) Cords
    /// </summary>
    [Serializable]
    class Position2D : APosition
    {
        public Position2D(int row, int col) : base(row, col) { }

        /// <summary>
        /// Prints the position ( x, y )
        /// </summary>
        public override void print()
        {
            Console.WriteLine("Position: ( {0}, {1} )", Col, Row);
        }

        /// <summary>
        /// Sets the position to a given coordinate
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="level"></param>
        public override void setPosition(int row, int col, int level = 0)
        {
            Row = row;
            Col = col;
        }
    }
}
