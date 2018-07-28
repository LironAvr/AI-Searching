using ATP2016Project.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.View
{
    /// <summary>
    /// View Interface - defines the main functions of the view
    /// </summary>
    interface IView
    {
        /// <summary>
        /// Output function
        /// </summary>
        /// <param name="output">string output</param>
        /// <param name="cursor">bool cursor (print cursor or not - default true)</param>
        void Output(string output, bool cursor = true);

        /// <summary>
        /// Error Output function - prints red
        /// </summary>
        /// <param name="output">string output</param>
        void errOutput(string output);

        /// <summary>
        /// PrintMaze
        /// </summary>
        /// <param name="mazeName">string maze name</param>
        /// <param name="maze">int[,,] maze to print</param>
        void printMaze(string mazeName, int[,,] maze);

        /// <summary>
        /// Starting the CLI
        /// </summary>
        void Start();

        /// <summary>
        /// Setter for m_commands
        /// </summary>
        /// <param name="commands">Dictionary of commands</param>
        void setCommands(Dictionary<string, ICommand> commands);
    }
}
