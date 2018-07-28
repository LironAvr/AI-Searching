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
    /// The Controller
    /// </summary>
    class MyController : IController
    {
        //Fields
        private Dictionary<string, ICommand> m_commands;
        protected IModel m_model;
        protected IView m_view;

        //Functions

        /// <summary>
        /// MyController Constructor
        /// </summary>
        public MyController()
        {
            m_model = null;
            m_view = null;
            m_commands = new Dictionary<string, ICommand>();
        }

        /// <summary>
        /// Set Model - sets a model for the controller
        /// </summary>
        /// <param name="model">model</param>
        public void SetModel(IModel model)
        {
            m_model = model;
        }

        /// <summary>
        /// Set View - sets a view for the controller
        /// </summary>
        /// <param name="view">view</param>
        public void SetView(IView view)
        {
            m_view = view;
            if (null != m_model) createCommandDictionary();
            m_view.setCommands(m_commands);
        }

        /// <summary>
        /// createCommandDictionary - initiates the commands dictionary
        /// </summary>
        private void createCommandDictionary()
        {
            m_commands.Add("dir", new DirCommand(m_model, m_view));
            m_commands.Add("generate3dmaze", new Generate3DMazeCommand(m_model, m_view));
            m_commands.Add("display", new DisplayCommand(m_model, m_view));
            m_commands.Add("savemaze", new SaveMazeCommand(m_model, m_view));
            m_commands.Add("loadmaze", new LoadMazeCommand(m_model, m_view));
            m_commands.Add("mazesize", new MazeSizeCommand(m_model, m_view));
            m_commands.Add("filesize", new FileSizeCommand(m_model, m_view));
            m_commands.Add("solvemaze", new SolveMazeCommand(m_model, m_view));
            m_commands.Add("displaysolution", new DisplaySolutionCommand(m_model, m_view));
            m_commands.Add("exit", new ExitCommand(m_model, m_view));
        }

        /// <summary>
        /// GetCommands
        /// </summary>
        /// <returns>a dictionary containing the commands avilable for the controller</returns>
        public Dictionary<string, ICommand> GetCommands()
        {
            return m_commands;
        }

        /// <summary>
        /// Output - sending a given output to the view
        /// </summary>
        /// <param name="output">string output</param>
        public void Output(string output)
        {
            m_view.Output(output);
        }

        /// <summary>
        /// errOutput - sending a given error output to the view
        /// </summary>
        /// <param name="output">string output</param>
        public void errOutput(string output)
        {
            m_view.errOutput(output);
        }
    }
}
