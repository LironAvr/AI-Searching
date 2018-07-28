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
    /// Exits the program
    /// </summary>
    class ExitCommand : ACommand
    {
        /// <summary>
        /// Exit command constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public ExitCommand(IModel model, IView view) : base(model, view) { }

        /// <summary>
        /// DoCommand - make sure all the running threads end and exits the program
        /// </summary>
        /// <param name="parameters">no parameters should be given</param>
        public override void DoCommand(params string[] parameters)
        {
            m_model.exit();
            m_view.Output("Controller Terminated...");
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "exit";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "exit";
        }
    }
}