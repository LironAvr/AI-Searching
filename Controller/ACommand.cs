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
    /// /Abstract class representing a command
    /// </summary>
    abstract class ACommand : ICommand
    {
        protected IModel m_model;
        protected IView m_view;

        /// <summary>
        /// ACommand Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public ACommand(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
        }

        /// <summary>
        /// DoCommand - asks the model to get the dir info and sends it to the view for print
        /// </summary>
        /// <param name="parameters">dir path</param>
        public abstract void DoCommand(params string[] parameters);

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public abstract string GetName();

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public abstract string getInfo();
    }
}
