using ATP2016Project.Model;
using ATP2016Project.View;
using System;

namespace ATP2016Project.Controller
{
    /// <summary>
    /// Display Command - prints a maze
    /// </summary>
    class DisplayCommand : ACommand
    {
        /// <summary>
        /// DisplayCommand constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public DisplayCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// DoCommand - performs the display command
        /// </summary>
        /// <param name="parameters">maze name</param>
        public override void DoCommand(params string[] parameters)
        {
            if (parameters.Length != 1)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            int[,,] maze;
            try
            {
                maze = m_model.getMaze(parameters[0]);
            }
            catch (Exception)
            {
                m_view.errOutput("ERROR - Maze named: " + parameters[0] + " does not exist!\n");
                return;
            }
            m_view.printMaze(parameters[0], maze);
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "display <maze name>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "display";
        }
    }
}
