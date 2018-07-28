using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    /// <summary>
    /// Solve Maze Command
    /// </summary>
    class SolveMazeCommand : ACommand
    {
        /// <summary>
        /// Solve Maze Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public SolveMazeCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// Request the model to solve a given maze using a given search algorithm (BFS/DFS)
        /// </summary>
        /// <param name="parameters">[0] maze name [1] searching algorithm</param>
        public override void DoCommand(params string[] parameters)
        {
            string algorithm;
            string mazeName;
            if (2 == parameters.Length)
            {
                mazeName = parameters[0].ToLower();
                algorithm = parameters[1].ToUpper();
            }
            else
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            m_model.solveMaze(mazeName, algorithm);
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "solveMaze <maze name> <algorithm (BFS/DFS)>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "solvemaze";
        }
    }
}
