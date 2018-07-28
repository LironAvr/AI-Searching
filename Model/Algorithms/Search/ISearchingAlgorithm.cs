using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// Represents an Interface for a Searching Algorithm
    /// Defines the following functions:
    /// Solve - solves the given searchable and returns a solution
    /// getNumberOfNodesEvaluated - returns the number of nodes the algorithm evaluated while solving
    /// GetSolvingTimeMiliseconds - returns the time (in Miliseconds) it took the algorithm to solves the searchable
    /// clear - resets the searching algorithm to a clean state - ready to solve a new searchable
    /// </summary>
    interface ISearchingAlgorithm
    {
        Solution Solve(ISearchable searchable);
        int getNumberOfNodesEvaluated();
        int getMaxNumberOfNodesStored();
        double GetSolvingTimeMiliseconds();
        void clear();
    }
}
