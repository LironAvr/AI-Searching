using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Search
{
    /// <summary>
    /// Represents a Generic state
    /// </summary>
    abstract class AState
    {
        protected string m_state;
        protected AState m_parentState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentState"></param>
        public AState(AState parentState)
        {
            m_parentState = parentState;
        }

        /// <summary>
        /// Checks if the states are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> bool </returns>
        public override bool Equals(object obj)
        {
            return m_state.Equals(((AState)obj).m_state);
        }

        /// <summary>
        /// Returns the parent state of the state
        /// </summary>
        /// <returns> AState </returns>
        public AState getParentState()
        {
            return m_parentState;
        }


        /// <summary>
        /// Returns the state as string
        /// </summary>
        /// <returns> string </returns>
        public string getState()
        {
            return m_state;
        }

        /// <summary>
        /// Prints the state
        /// </summary>
        public void printState()
        {
            Console.WriteLine(m_state);
        }

        public override int GetHashCode()
        {
            return -1;
        }
    }
}
