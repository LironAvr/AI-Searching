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
    /// Generates a 3 Dimensional maze
    /// </summary>
    class Generate3DMazeCommand : ACommand
    {
        /// <summary>
        /// Generate3DMaze Command Constructor
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public Generate3DMazeCommand(IModel model, IView view) : base(model, view){ }

        /// <summary>
        /// DoCommand - Generates a maze with given dimensions
        /// </summary>
        /// <param name="parameters">[0] maze name [1] columns [2] rows [3] floors</param>
        public override void DoCommand(params string[] parameters)
        {
            int columns, rows, floors;
            if (parameters.Length != 4 || !(int.TryParse(parameters[1], out columns) &
                  int.TryParse(parameters[2], out rows) &
                  int.TryParse(parameters[3], out floors)))
                m_view.errOutput("ERROR - Wrong number of parameters inserted, function should be used as: " + getInfo() + "\n");
            else if (rows < 6 | columns < 6 | rows > 255 | columns > 255 | floors > 255)
                m_view.errOutput("ERROR - Invalid maze dimensions, Rows and Columns should be between 6 and 255, floors should be upto 255");
            else
                m_model.generate3dMaze(parameters[0], columns, rows, floors);
        }

        /// <summary>
        /// Returns important info about the command
        /// </summary>
        /// <returns>string info</returns>
        public override string getInfo()
        {
            return "generate3DMaze <maze name> <columns (X)> <rows (Y)> <floors (Z)>\n(Rows and Columns should be between 6 and 255, floors should be upto 255)";
        }

        /// <summary>
        /// GetName
        /// </summary>
        /// <returns>string name of command</returns>
        public override string GetName()
        {
            return "generate3dmaze";
        }
    }
}
