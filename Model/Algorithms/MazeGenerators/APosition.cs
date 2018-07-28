using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// represents a Generic Position
    /// </summary>
    [Serializable]
    abstract class APosition
    {

        private int m_row;
        private int m_col;

        /// <summary>
        /// Sets a position to a specific values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public abstract void setPosition(int x, int y, int z = 0);
        /// <summary>
        /// print position
        /// </summary>
        public abstract void print();

        public int Col
        {
            get
            {
                return m_col;
            }

            set
            {
                m_col = value;
            }
        }

        public int Row
        {
            get
            {
                return m_row;
            }

            set
            {
                m_row = value;
            }
        }

        public APosition(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
