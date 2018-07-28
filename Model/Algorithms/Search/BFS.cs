using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    class BFS : ASearchingAlgorithm
    {
        private Dictionary<string, AState> m_openList;
        private Queue<AState> m_openQueue;
        /// <summary>
        /// This class solve searchable problems using Breadth-first search algorithm
        /// </summary>
        public BFS()
        {
            m_openQueue = new Queue<AState>();
            m_closedList = new Dictionary<string, AState>();
            m_openList = new Dictionary<string, AState>();
            m_countGeneratedNodes = 1;
            m_stopWatch = new Stopwatch();
        }
        /// <summary>
        /// Solves the Searchable- BFS algorithm: finds the path from initial state to goal state
        /// </summary>
        /// <param name="searchable">searchable problem to solve</param>
        /// <returns>legal solution </returns>
        public override Solution Solve(ISearchable searchable)
        {
            clear();
            m_openQueue.Clear();
            m_openList.Clear();
            m_stopWatch.Restart();
            Solution solution = new Solution();
            // add the initial state to the queue and to openList
            m_openQueue.Enqueue(searchable.getInitialState());
            m_openList.Add(searchable.getInitialState().getState(), searchable.getInitialState());
            AState current;
            while (m_openQueue.Count > 0)
            {
                // remove the current state from the openList and queue and add him to closedList
                current = m_openQueue.Dequeue();
                m_openList.Remove(current.getState());
                m_closedList.Add(current.getState(), current);

                // if the current state is the goal create and return the solution
                if (current.Equals(searchable.getGoalState()))
                {
                    solution = searchable.createSolution(current);
                    break;
                }

                // add all the successors that not found to the queue and to the openList
                List<AState> successors = searchable.getAllSuccessors(current);
                foreach (AState successor in successors)
                {
                    if (!m_openList.ContainsKey(successor.getState()) && !m_closedList.ContainsKey(successor.getState()))
                    {
                        m_openList.Add(successor.getState(), successor);
                        m_openQueue.Enqueue(successor);
                        m_countGeneratedNodes++;
                    }
                }
            }
            m_stopWatch.Stop();
            return solution;
        }
    }
}