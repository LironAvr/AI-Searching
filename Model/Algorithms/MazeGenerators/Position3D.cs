using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Represents a 3 Dimensional Position (X,Y,Z) Cords
    /// </summary>
    [Serializable]
    class Position3D : APosition
    {
        private int m_level;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="level"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Position3D(int level, int row, int col) : base(row, col)
        {
            m_level = level;
        }

        public int Level
        {
            get
            {
                return m_level;
            }

            set
            {
                m_level = value;
            }
        }

        /// <summary>
        /// Prints the Position coordinates ( x, y, z )
        /// </summary>
        public override void print()
        {
            Console.WriteLine("Position: ( {0}, {1}, {2} )", Col, Row, Level);
        }

        /// <summary>
        /// Sets the position to a given coordinate
        /// </summary>
        /// <param name="level"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public override void setPosition(int level, int row, int col)
        {
            Row = row;
            Col = col;
            Level = level;
        }
    }
}
