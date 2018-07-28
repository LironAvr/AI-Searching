using ATP2016Project.Model;
using ATP2016Project.Model.Algorithms.Search;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    /// <summary>
    /// DisplaySolution Command - prints a solution for a maze
    /// </summary>
    class DisplaySolutionCommand : ACommand
    {
        /// <summary>
        /// DisplaySolutionCommand constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public DisplaySolutionCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// DoCommand - performs the display solution command
        /// </summary>
        /// <param name="parameters">maze name</param>
        public override void DoCommand(params string[] parameters)
        {
            try
            {
                if (parameters.Length != 1) throw (new Exception());
                else if (!m_model.mazeExists(parameters[0]))
                {
                    m_view.errOutput("ERROR - There is no maze named " + parameters[0]);
                    return;
                }
            }
            catch (Exception)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            Solution solution = m_model.displaySolution(parameters[0]); 
            if (null == solution)
                m_view.errOutput("ERROR - The maze named " + parameters[0] + " was not solved yet!\n");
            else
            {
                string solString = "Solution for maze named : " + parameters[0] +"\n(x,y,z)\n";
                foreach (AState state in solution.getSolutionPath())
                {
                    solString += state.getState();
                    solString += "\n";
                }
                solString += "End of Solution for maze " + parameters[0] + "\n";
                m_view.Output(solString);
            }
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "displaySolution <maze name>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "displaysolution";
        }
    }
}