using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// An Interface for a Searchable problem
    /// Defines the following functions:
    /// Get the searchables intial state
    /// Get the searchables goal state
    /// Get the list of possible successors of a state
    /// Create a solution using the achieved goal state
    /// Print
    /// </summary>
    interface ISearchable
    {
        AState getInitialState();
        AState getGoalState();
        List<AState> getAllSuccessors(AState state);
        Solution createSolution(AState end);
        void print();
    }
}
