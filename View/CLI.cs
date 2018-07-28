using ATP2016Project.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATP2016Project.View
{

    /// <summary>
    /// Command Line Interface
    /// </summary>
    class CLI : IView
    {
        private Stream m_input;
        private Stream m_output;
        private StreamReader m_reader;
        private IController m_controller;
        Dictionary<string, ICommand> m_commands;
        private string m_cursor = ">>";
        Mutex mutex;

        /// <summary>
        /// CLI Constructor
        /// </summary>
        /// <param name="controller">The controller to be used to connect with the view</param>
        public CLI(IController controller)
        {
            m_controller = controller;
            m_input = Console.OpenStandardInput();
            m_output = Console.OpenStandardOutput();
            m_reader = new StreamReader(m_input);
            mutex = new Mutex();
        }

        /// <summary>
        /// Starting the CLI
        /// </summary>
        public void Start()
        {
            printInfo();
            Thread ioThread = new Thread(run);
            ioThread.Start();
        }

        /// <summary>
        /// Prints initial info about the program
        /// </summary>
        private void printInfo()
        {
            string commands = "Command Line Interface Started\n\n";
            foreach (ICommand command in m_commands.Values)
            {
                commands += command.getInfo() + "\n\n";
            }
            Output(commands.Remove(commands.Length - 2), false);
            errOutput("Important Information:\nThe program is NOT case sensitive\nMaze names and paths cannot include backspaces\nMaze dimensions limitation: 5 < Rows\\Columns < 256\n");     
        }

        /// <summary>
        /// CLI running function - takes care of input -> commands
        /// </summary>
        private void run()
        {
            List<string> input;
            string command;
            while (true)
            {
                m_reader.DiscardBufferedData();
                input = Input().Trim().Split(' ').ToList<string>();
                command = input[0].ToLower();
                if (!m_commands.ContainsKey(command))
                    errOutput("ERROR - '" + command + "' is an Unrecognized Command!\n");
                else if ("exit" == command) break;
                else
                {
                    input.RemoveAt(0);
                    m_commands[command].DoCommand(input.ToArray());
                }
            }
            m_commands["exit"].DoCommand();
            Output("View Terminated...\nGood Bye!\nPress Enter to Exit");
            Console.Read();
            m_reader.Close();
            Environment.Exit(0);
        }

        /// <summary>
        /// Error Output function - prints red
        /// </summary>
        /// <param name="output">string output</param>
        public void errOutput(string output)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            StreamWriter streamWriter = new StreamWriter(m_output);
            streamWriter.AutoFlush = true;
            Console.SetCursorPosition(0, Console.CursorTop);
            streamWriter.WriteLine("");
            streamWriter.Write(output);
            streamWriter.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            streamWriter.Write(m_cursor);
            Console.ResetColor();
        }

        /// <summary>
        /// Input function
        /// </summary>
        /// <returns>string input</returns>
        private string Input()
        {
            string input;
            if (null != m_reader)
                m_reader.DiscardBufferedData();
            
            input = m_reader.ReadLine();
            return input;
        }

        /// <summary>
        /// Output function
        /// </summary>
        /// <param name="output">string output</param>
        /// <param name="cursor">bool cursor (print cursor or not - default true)</param>
        public void Output(string output, bool cursor = true)
        {
            mutex.WaitOne();
            Console.ForegroundColor = ConsoleColor.Cyan;
            StreamWriter streamWriter = new StreamWriter(m_output);
            streamWriter.AutoFlush = true;
            Console.SetCursorPosition(0, Console.CursorTop);
            streamWriter.WriteLine("");
            streamWriter.Write(output);
            streamWriter.WriteLine("");
            if (cursor)
                streamWriter.Write(m_cursor);
            Console.ResetColor();
            mutex.ReleaseMutex();
        }

        /// <summary>
        /// Setter for m_commands
        /// </summary>
        /// <param name="commands">Dictionary of commands</param>
        public void setCommands(Dictionary<string, ICommand> commands)
        {
            m_commands = commands;
        }

        /// <summary>
        /// PrintMaze
        /// </summary>
        /// <param name="mazeName">string maze name</param>
        /// <param name="maze">int[,,] maze to print</param>
        public void printMaze(string mazeName, int[,,] maze)
        {
            mutex.WaitOne();
            int columns = maze.GetLength(2);
            int rows = maze.GetLength(1);
            int floors = maze.GetLength(0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n********** < Displaying maze (" + mazeName + ") > **********\n");
            Console.ResetColor();
            for (int level = 0; level < floors; level++)
            {
                Console.WriteLine("Floor number " + level + "\n");
                Console.Write("    ");
                for (int row = 0; row < columns; row++)
                {
                    if (row < 10)
                        Console.Write(" 0" + row + " ");
                    else Console.Write(" " + row + " ");
                }

                Console.WriteLine();
                for (int row = 0; row < rows; row++)
                {
                    if (row < 10)
                        Console.Write(" 0" + row + " ");
                    else Console.Write(" " + row + " ");
                    for (int col = 0; col < columns; col++)
                    {
                        if (maze[level, row, col] == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("****");
                            Console.ResetColor();
                        }
                        else if (maze[level, row, col] == 0)
                        {
                            Console.Write("    ");
                        }
                        else if (maze[level, row, col] == 5)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" ST ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" EN ");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("********** < End of Maze > **********");
            Console.ResetColor();
            Output("");
            mutex.ReleaseMutex();
        }
    }
}
