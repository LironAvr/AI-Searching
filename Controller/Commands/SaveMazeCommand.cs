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
    /// Save Maze Command - Compresses and Saves a maze into a file
    /// </summary>
    class SaveMazeCommand : ACommand
    {
        /// <summary>
        /// Save Maze Command Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public SaveMazeCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// DoCommand - Compresses the requested maze and saves it into a file in the given path
        /// </summary>
        /// <param name="parameters">[0] maze name [1]file path</param>
        public override void DoCommand(params string[] parameters)
        {
            if (2 != parameters.Length)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            else if (!pathExists(parameters[1]))
            {
                m_view.errOutput("ERROR - Invalid path, maze could not be saved.\n");
            }
            else if (m_model.saveMaze(parameters[0], parameters[1]))
            {
                m_view.Output("Maze " + parameters[0] + " was saved successfuly into: " + parameters[1] + "\n");
            }
            else m_view.errOutput("ERROR - Maze named " + parameters[0] + " does not exist! maze could not be saved.\n");
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "saveMaze <maze name> <file path>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "savemaze";
        }

        /// <summary>
        /// pathExists - check if a given path is valid and accessable
        /// </summary>
        /// <param name="path">path for saving the maze</param>
        /// <returns>True for existing path, false otherwise</returns>
        private bool pathExists(string path)
        {
            char[] c = new char[1];
            while (path.Length > 0 && !path.EndsWith("\\"))
                path = path.Remove(path.Length - 1);
            if (path.Length == 0) return true;
            else return Directory.Exists(path);
        }
    }
}