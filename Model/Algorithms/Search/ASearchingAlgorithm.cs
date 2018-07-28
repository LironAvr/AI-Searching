using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// Represents a Generic Searching Algorithm
    /// </summary>
    abstract class ASearchingAlgorithm : ISearchingAlgorithm
    {
        protected int m_countGeneratedNodes;
        protected int m_maxNodesStored;
        protected Dictionary<string, AState> m_closedList;
        protected Stopwatch m_stopWatch;

        /// <summary>
        /// Solves the searchable
        /// </summary>
        /// <param name="searchable"></param>
        /// <returns> Solution </returns>
        public abstract Solution Solve(ISearchable searchable);

        /// <summary>
        /// Returns the number of nodes evaluated
        /// </summary>
        /// <returns> m_countGeneratedNodes </returns>
        public int getNumberOfNodesEvaluated()
        {
            return m_countGeneratedNodes;
        }

        public int getMaxNumberOfNodesStored()
        {
            return m_maxNodesStored;
        }

        /// <summary>
        /// Returns the solving time of the algorithm
        /// </summary>
        /// <returns> m_stopWatch.ElapsedTicks </returns>
        public double GetSolvingTimeMiliseconds()
        {
            return m_stopWatch.ElapsedMilliseconds;
        } 
          
        /// <summary>
        /// Clears the fiedls of the class to get it ready to solve a new problem
        /// </summary>
        public void clear()
        {
            m_countGeneratedNodes = 1;
            m_closedList.Clear();
            m_stopWatch.Reset();
        }
    }
}
