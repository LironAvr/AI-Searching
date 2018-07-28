using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    /// <summary>
    /// Load Maze Command - Loads a compressed maze from a file
    /// </summary>
    class LoadMazeCommand : ACommand
    {
        /// <summary>
        /// Load Maze Command Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public LoadMazeCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// Load Maze Command - loads a compressed maze from a file
        /// </summary>
        /// <param name="parameters">[0] file path [1] maze name</param>
        public override void DoCommand(params string[] parameters)
        {
            if (2 != parameters.Length)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            if (File.Exists(parameters[0]))
            {
                m_model.loadMaze(parameters[0].ToLower(), parameters[1]);
                m_view.Output("Maze was loaded successfuly!\n");
            }
            else
                m_view.errOutput("ERROR - The given path is invalid! maze loading terminated.\n");
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "loadMaze <file path> <maze name>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "loadmaze";
        }
    }
}