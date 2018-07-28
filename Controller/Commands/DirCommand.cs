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
    /// Dir Commands - returns the names of the files and folders in a given path
    /// </summary>
    class DirCommand : ACommand
    {
        /// <summary>
        /// Dir Command Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public DirCommand(IModel model, IView view) : base(model, view){}

        /// <summary>
        /// DoCommand - asks the model to get the dir info and sends it to the view for print
        /// </summary>
        /// <param name="parameters">dir path</param>
        public override void DoCommand(params string[] parameters)
        {
            if (1 != parameters.Length)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
                return;
            }
            else try
            {
                if (Directory.Exists(parameters[0].ToLower()))
                {
                    string[] data = m_model.getDir(parameters[0].ToLower());
                    foreach (string line in data)
                    {
                        m_view.Output(line + " ");
                    }
                    m_view.Output("\n");
                }
                else m_view.errOutput("ERROR - The give path (" + parameters[0] + ") is invalid!\n");
            }
            catch (Exception)
            {
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
            }
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "dir <path>";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "dir";
        }
    }
}
