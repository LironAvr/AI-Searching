using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    /// <summary>
    /// An Interfact representing a command
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// DoCommand - asks the model to get the dir info and sends it to the view for print
        /// </summary>
        /// <param name="parameters">dir path</param>
        void DoCommand(params string[] parameters);

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        string GetName();

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        string getInfo();
    }
}
