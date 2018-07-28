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
    /// An Interface representing a controller
    /// </summary>
    interface IController
    {
        /// <summary>
        /// Set Model - sets a model for the controller
        /// </summary>
        /// <param name="model">model</param>
        void SetModel(IModel model);

        /// <summary>
        /// Set View - sets a view for the controller
        /// </summary>
        /// <param name="view">view</param>
        void SetView(IView view);

        /// <summary>
        /// Output - sending a given output to the view
        /// </summary>
        /// <param name="output">string output</param>
        void Output(string output);

        /// <summary>
        /// errOutput - sending a given error output to the view
        /// </summary>
        /// <param name="output">string output</param>
        void errOutput(string output);

        /// <summary>
        /// GetCommands
        /// </summary>
        /// <returns>a dictionary containing the commands avilable for the controller</returns>
        Dictionary<string, ICommand> GetCommands();
    }
}
