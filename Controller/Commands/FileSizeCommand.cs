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
    /// File Size Command - prints the size of a file in a given path
    /// </summary>
    class FileSizeCommand : ACommand
    {
        /// <summary>
        /// File Size Command Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public FileSizeCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// DoCommand - prints the size in bytes of a given file
        /// </summary>
        /// <param name="parameters">file path</param>
        public override void DoCommand(params string[] parameters)
        {
            if (1 == parameters.Length)
            {
                if (!File.Exists(parameters[0]))
                {
                    m_view.errOutput("ERROR - The given path is invalid!\n");
                    return;
                }
            }
            else
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
            }
            long size = m_model.fileSize(parameters[0]);
            m_view.Output("File (" + parameters[0] + ") size is: " + size.ToString() + " bytes\n");
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "fileSize <file path>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "filesize";
        }
    }
}