using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    class AStarEarly : ASearchingAlgorithm
    {
        private Dictionary<string, AState> m_openList;
        private Queue<AState> m_openQueue;

        public AStarEarly()
        {
            m_openQueue = new Queue<AState>();
            m_closedList = new Dictionary<string, AState>();
            m_openList = new Dictionary<string, AState>();
            m_countGeneratedNodes = 1;
            m_stopWatch = new Stopwatch();
        }

        public override Solution Solve(ISearchable searchable)
        {
            throw new NotImplementedException();
        }
    }
}
