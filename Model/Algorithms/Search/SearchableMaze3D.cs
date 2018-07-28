using ATP2016Project.Model.Algorithms.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// An Object adapter converting a 3D maze into a searchable
    /// </summary>
    class SearchableMaze3D : ISearchable
    {
        private Maze3d m_maze3d;
        private AState m_initialState;
        private AState m_goalState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maze3d"></param>
        public SearchableMaze3D(AMaze maze3d) 
        {
            m_maze3d = (Maze3d)maze3d;
            Position3D statrPosition = (Position3D) maze3d.getStartPosition();
            m_initialState = new MazeState(null, statrPosition.Col, statrPosition.Row, statrPosition.Level);
            Position3D goalPosition = (Position3D)maze3d.getGoalPosition();
            m_goalState = new MazeState(null, goalPosition.Col, goalPosition.Row, goalPosition.Level);
        }

        /// <summary>
        /// Returns the Initial State of the Maze
        /// </summary>
        /// <returns> AState (InitialState) </returns>
        public AState getInitialState()
        {
            return m_initialState;
        }

        /// <summary>
        /// Returns the Goal State of the Maze
        /// </summary>
        /// <returns> AState (GoalState) </returns>
        public AState getGoalState()
        {
            return m_goalState;
        }

        /// <summary>
        /// Returns a List containing all the possible (valid) successors of a gives AState
        /// </summary>
        /// <param name="state"></param>
        /// <returns> List of successors  </returns>
        public List<AState> getAllSuccessors(AState state)
        {
            List<AState> successors = new List<AState>();
            MazeState mState = (MazeState)state;
            int col = mState.getX();
            int row = mState.getY();
            int floor = mState.getZ();
            //Right (Col ++)
            if (col + 1 < m_maze3d.getWidth() && m_maze3d.isPath(col + 1, row, floor))
                successors.Add(new MazeState(state, col + 1, row, floor));
            //Left (Col --)
            if (col - 1 >= 0 && m_maze3d.isPath(col - 1, row, floor))
                successors.Add(new MazeState(state, col - 1, row, floor));
            //Up (Row ++)
            if (row + 1 < m_maze3d.getHeight() && m_maze3d.isPath(col, row + 1, floor))
                successors.Add(new MazeState(state, col, row + 1, floor));
            //Down (Row --)
            if (row - 1 >= 0 && m_maze3d.isPath(col, row - 1, floor))
                successors.Add(new MazeState(state, col, row - 1, floor));
            //Ascend (Floor ++)
            if (floor + 1 < m_maze3d.getLevels() && m_maze3d.isPath(col, row, floor + 1))
                successors.Add(new MazeState(state, col, row, floor + 1));
            //Descend (Floor --)
            if (floor - 1 >= 0 && m_maze3d.isPath(col, row, floor - 1))
                successors.Add(new MazeState(state, col, row, floor - 1));
            return successors;
        }

        /// <summary>
        /// Backtracking a solution from a gives state (Using parent states)
        /// </summary>
        /// <param name="end"></param>
        /// <returns> Solution </returns>
        public Solution createSolution(AState end)
        {
            Solution solution = new Solution();
            AState current = end;
            while (null != current)
            {
                solution.addState(current);
                current = current.getParentState();
            }
            solution.reverese();
            return solution;
        }

        /// <summary>
        /// Prints the maze
        /// </summary>
        public void print()
        {
            m_maze3d.print();
        }
    }
}
