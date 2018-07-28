using ATP2016Project.Model.Algorithms.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// Represents a specific state of the maze
    /// </summary>
    class MazeState : AState
    {
        private Position3D m_position;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentState"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="pointZ"></param>
        public MazeState(AState parentState, int pointX, int pointY, int pointZ)
            : base(parentState)
        {
            m_state = "(" + pointX + "," + pointY + "," + pointZ + ")";
            m_position = new Position3D(pointZ, pointY, pointX);          
        }

        /// <summary>
        /// Gets the X Coordinate of the state ( X = Column )
        /// </summary>
        /// <returns> col </returns>
        public int getX()
        {
            return m_position.Col;
        }

        /// <summary>
        /// Gets the Y Coordinate of the state ( Y = Row )
        /// </summary>
        /// <returns> row </returns>
        public int getY()
        {
            return m_position.Row;
        }

        /// <summary>
        /// Gets the Z Coordinate of the state ( Z = Level/Floor )
        /// </summary>
        /// <returns> level </returns>
        public int getZ()
        {
            return m_position.Level;
        }
    }
}
