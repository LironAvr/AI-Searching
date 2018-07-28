using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// This class solves a searchable using a Depth First Search algorithm
    /// </summary>
    class DFS : ASearchingAlgorithm
    {
        private Stack<AState> m_openListStates;
        //private Dictionary<string, AState> m_openList;

        /// <summary>
        /// Constructor
        /// </summary>
        public DFS()
        {
            m_openListStates = new Stack<AState>();
            //m_openList = new Dictionary<string, AState>();
            m_closedList = new Dictionary<string, AState>();
            m_stopWatch = new Stopwatch();
        }

        /// <summary>
        /// Solves the Searchable using DFS algorithm - Finds a path from the InitialState to the GoalState
        /// </summary>
        /// <param name="searchable"></param>
        /// <returns> Solution </returns>
        public override Solution Solve(ISearchable searchable)
        {
            clear();
            m_openListStates.Clear();
            m_stopWatch.Restart();
            Solution solution = new Solution();
            AState current = searchable.getInitialState();
            AState goal = searchable.getGoalState();
            m_closedList.Add(current.getState(), current);
            while (!goal.Equals(current))
            {
                List<AState> successors = searchable.getAllSuccessors(current);
                addToOpenList(successors);
                //Pops the next state to evaluate - Skips states that where already evaluated (exist in the closedList)
                while (m_openListStates.Count > 0 && m_closedList.ContainsKey(current.getState()))
                    current = m_openListStates.Pop();
                //Adds the current state (thats currently being evaluated) to the closed list so that it wont be evaluated again
                if (!m_closedList.ContainsKey(current.getState()))
                {
                    m_closedList.Add(current.getState(), current);
                    m_countGeneratedNodes++;
                }
            } //While loop is done - means the current state is the goal state

            solution = searchable.createSolution(current);
            m_stopWatch.Stop();
            return solution;
        }

        /// <summary>
        /// Adds a List of AStates into the openList (Stack) if they dont exist in the closedList
        /// </summary>
        /// <param name="list"></param>
        private void addToOpenList(List<AState> list)
        {
            foreach (AState state in list)
            {
                if (/*!m_openList.ContainsKey(state.getState()) && */!m_closedList.ContainsKey(state.getState()))
                {
                    m_openListStates.Push(state);
                    //m_openList.Add(state.getState(), state);
                    //m_countGeneratedNodes++;
                }
            }
        }
    }
}
