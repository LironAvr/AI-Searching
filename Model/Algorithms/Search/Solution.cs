using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// Represents a Solution for a searchable problem
    /// </summary>
    class Solution
    {
        private List<AState> m_solution;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Solution()
        {
            m_solution = new List<AState>();
        }

        /// <summary>
        /// Adds a state to the Solution
        /// </summary>
        /// <param name="state"></param>
        public void addState(AState state)
        {
            m_solution.Add(state);
        }

        /// <summary>
        /// Checks if theres a solution - meaning the list contains AStates
        /// </summary>
        /// <returns> bool </returns>
        public bool isSolutionExists()
        {
            return m_solution.Count > 0;
        }

        /// <summary>
        /// Returns the length of the solution - How many steps it takes from Initial to Goal
        /// </summary>
        /// <returns></returns>
        public int getSolutionSteps()
        {
            return m_solution.Count;
        }

        /// <summary>
        /// Returns the solution path
        /// </summary>
        /// <returns> List of the AStates involved in the solution </returns>
        public List<AState> getSolutionPath()
        {
            return m_solution;
        }

        /// <summary>
        /// Reverses the solution for when the states are inserted from goal to initial
        /// </summary>
        public void reverese()
        {
            m_solution.Reverse();
        }

        /// <summary>
        /// Prints the solution states
        /// </summary>
        public void printSolution()
        {           
            foreach (AState state in m_solution)
            {
                state.printState();
            }
        }
    }
}
