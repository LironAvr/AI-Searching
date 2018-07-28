using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.MazeGenerators
{
    /// <summary>
    /// Represents a Generic Maze
    /// </summary>
    [Serializable]
    abstract class AMaze : IMaze
    {    
        protected APosition startPoint;
        protected APosition endPoint;
        public abstract void print();   
        public abstract int getHeight();
        public abstract int getWidth();
        public abstract int getCell(int y, int x, int z = 0);
        /// <summary>
        /// returns the start position
        /// </summary>
        /// <returns></returns>
        public APosition getStartPosition()
        {
            return startPoint;
        }
        /// <summary>
        /// returns the goal position
        /// </summary>
        /// <returns></returns>
        public APosition getGoalPosition()
        {
            return endPoint;
        }

        public AMaze(APosition start, APosition end)
        {
            startPoint = start;
            endPoint = end;
        }

        public AMaze() { }

        protected void setStartEndPosition(APosition start, APosition end)
        {
            startPoint = start;
            endPoint = end;
        }
    }
}
