using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    /// <summary>
    /// Maze SizeCommand - gets the size (in bytes) of a given maze
    /// </summary>
    class MazeSizeCommand : ACommand
    {
        /// <summary>
        /// Maze Size Command Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public MazeSizeCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// DoCommand - performs maze size command
        /// </summary>
        /// <param name="parameters">maze name</param>
        public override void DoCommand(params string[] parameters)
        {
            if (1 != parameters.Length)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            int size = m_model.mazeSize(parameters[0]);
            if (-1 != size)
            {
                m_view.Output("Maze (" + parameters[0] + ") size is: " + size.ToString() + " bytes\n");
            }

            else m_view.errOutput("ERROR - maze named:" + parameters[0] + "does not exist!\n");
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "mazeSize <maze name>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "mazesize";
        }
    }
}