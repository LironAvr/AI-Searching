using System;
using System.Collections.Generic;
using System.Linq;
using ATP2016Project.Model.Algorithms.MazeGenerators;
using ATP2016Project.Model.Algorithms.Search;
using ATP2016Project.Controller;
using System.IO;
using System.Threading;
using ATP2016Project.Model.Algorithms.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;

namespace ATP2016Project.Model
{
    /// <summary>
    /// MyModel
    /// </summary>
    class MyModel : IModel
    {
        private Dictionary<string, Maze3d> m_mazesDictionary;
        private Dictionary<string, Solution> m_solutionsDictionary;
        private Dictionary<string, Mutex> m_mutexDictionary;
        private Mutex m_mutex;
        private IController m_controller;
        private List<Thread> m_threads;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="c">Icontroller</param>
        public MyModel(IController c)
        {
            m_controller = c;
            m_mazesDictionary = new Dictionary<string, Maze3d>();
            m_solutionsDictionary = new Dictionary<string, Solution>();
            m_threads = new List<Thread>();
            m_mutexDictionary = new Dictionary<string, Mutex>();
            m_mutex = new Mutex();
        }

        #region Solution         

        /// <summary>
        /// Returns the solution for maze 'mazeName'
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>null - if the solution doesnt exists, else returns the solution </returns>
        public Solution displaySolution(string mazeName)
        {
            Solution sol;
            try
            {
                m_mutexDictionary[mazeName].WaitOne();
                sol = m_solutionsDictionary[mazeName];
                m_mutexDictionary[mazeName].ReleaseMutex();
            }
            catch (Exception)
            {
                return null;
            }
            return sol;
        }

        /// <summary>
        /// Generates a thread that solves the maze 'mazeName'
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="algorithm">algorithm to solve with</param>
        public void solveMaze(string mazeName, string algorithm)
        {
            ISearchingAlgorithm searching = null;
            if ("BFS" == algorithm)
            {
                searching = new BFS();
            }

            else if ("DFS" == algorithm)
            {
                searching = new DFS();
            }

            else
            {
                m_controller.errOutput("wrong argument inserted for <algorithm>, Algorithm options are BFS/DFS\n");
                return;
            }
            m_controller.Output("Solving maze...\n");
            Thread thread = new Thread(() => solve(searching, mazeName));
            m_threads.Add(thread);
            thread.Start();
        }

        /// <summary>
        /// Solve the maze 'mazeName' with BFS/DFS algorithm and saves the solution in the dictionary
        /// </summary>
        /// <param name="searching">Isearching algorithm to solve the maze with</param>
        /// <param name="mazeName">maze naze</param>
        private void solve(ISearchingAlgorithm searching, string mazeName)
        {
            ISearchable maze;
            try
            {
                m_mutexDictionary[mazeName].WaitOne();
                maze = new SearchableMaze3D(m_mazesDictionary[mazeName]);
            }
            catch (Exception)
            {
                m_controller.errOutput("The maze named: " + mazeName + "does not exist!\n");
                return;
            }
            Solution solution = searching.Solve(maze);
            m_solutionsDictionary[mazeName] = solution;
            m_mutexDictionary[mazeName].ReleaseMutex();
            m_controller.Output("Solution for " + mazeName + " is ready!\n");
        }

        #endregion

        #region Maze

        /// <summary>
        /// Add maze 'mazeName' to the dictionary
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="maze">Maze3d to save</param>
        private void addMaze(string mazeName, Maze3d maze)
        {
            m_mutex.WaitOne();
            if (!m_mutexDictionary.ContainsKey(mazeName))
                m_mutexDictionary[mazeName] = new Mutex();
            m_mutexDictionary[mazeName].WaitOne();
            m_mutex.ReleaseMutex();
            try
            {
                m_solutionsDictionary.Remove(mazeName);
            }
            catch (Exception) { }
            m_mazesDictionary[mazeName] = maze;
            m_mutexDictionary[mazeName].ReleaseMutex();
        }

        /// <summary>
        /// Returns a Maze3d with name 'mazeName' from the dictionary
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>Maze3d with name 'mazeName' if exists, else, returns null</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private Maze3d retrieveMaze(string mazeName)
        {
            if (!mazeExists(mazeName))
                return null;
            return m_mazesDictionary[mazeName];
        }

        /// <summary>
        /// Generates a thread that generates a 3d maze
        /// </summary>
        /// <param name="mazeName">maze name - ID</param>
        /// <param name="columns">num of columns in the maze</param>
        /// <param name="rows">num of rows in the maze</param>
        /// <param name="levels">num of levels in the maze</param>
        public void generate3dMaze(string mazeName, int columns, int rows, int levels)
        {
            int[] size = { levels, rows, columns };
            m_controller.Output("Generating Maze...\n");
            Thread thread = new Thread(() => threadGenerateMaze(size, mazeName));
            m_threads.Add(thread);
            thread.Start();
        }

        /// <summary>
        /// Generates a 3d maze and adds it to the dictionary.
        /// </summary>
        /// <param name="size">array[3] : [0]columns [1] rows [2] levels - as maze dimensions </param>
        /// <param name="mazeName">the maze name - maze ID</param>
        private void threadGenerateMaze(int[] size, string mazeName)
        {
            IMazeGenerator mazeGenerator3d = new MyMaze3dGenerator();
            Maze3d maze = (Maze3d)mazeGenerator3d.generate(size);
            addMaze(mazeName, maze);
            m_controller.Output("maze " + mazeName + " is ready!\n");
        }

        /// <summary>
        /// Returns the maze 'mazeName' 
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>3D array - maze board</returns>
        public int[,,] getMaze(string mazeName)
        {
            return retrieveMaze(mazeName).getMaze();
        }

        /// <summary>
        /// Loads a compressed maze from the given path ,decompress it and adds it to the dictionary
        /// also - if theres a previous existing solution for a maze with the same name - delete it
        /// </summary>
        /// <param name="path">path of the file to load</param>
        /// <param name="mazeName">maze name</param>
        public void loadMaze(string path, string mazeName)
        {
            int numOfBytes;
            byte[] arrayCmp = new byte[100];
            List<byte> totalComp = new List<byte>();
            using (FileStream fileInStream = new FileStream(path, FileMode.Open))
            {
                using (Stream compressor = new MyCompressorStream(fileInStream))
                {
                    while ((numOfBytes = compressor.Read(arrayCmp, 0, 100)) != 0)
                    {
                        for (int i = 0; i < numOfBytes; i++)
                            totalComp.Add(arrayCmp[i]);
                    }
                }
            }
            addMaze(mazeName, new Maze3d(totalComp.ToArray()));
        }

        /// <summary>
        /// Calculates the maze size in the memory and returns it by bytes
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>maze size in the memory in bytes</returns>
        public int mazeSize(string mazeName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] Array;
            Maze3d maze = retrieveMaze(mazeName);
            if (null == maze) return -1;
            bf.Serialize(ms, maze);
            Array = ms.ToArray();
            return Array.Length;
        }

        /// <summary>
        /// Compress the maze 'mazeName' and save it into a file with a given path
        /// </summary>
        /// <param name="mazeName">the name of the maze to save</param>
        /// <param name="path">file path</param>
        /// <returns>true - for success, false for failure</returns>
        public bool saveMaze(string mazeName, string path)
        {
            Maze3d maze = retrieveMaze(mazeName);
            if (null == maze) return false;
            using (FileStream fileOutStream = new FileStream(path, FileMode.Create))
            {
                using (Stream outStream = new MyCompressorStream(fileOutStream))
                {
                    outStream.Write(maze.toByteArray(), 0, maze.toByteArray().Length);
                    outStream.Flush();
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the maze 'mazeName' exists in the system
        /// </summary>
        /// <param name="mazeName"> maze name</param>
        /// <returns>true if the maze exists, false otherwise</returns>
        public bool mazeExists(string mazeName)
        {
            return m_mazesDictionary.ContainsKey(mazeName);
        }

        #endregion

        /// <summary>
        /// Returns all folders and files inside the folder given by 'path'
        /// </summary>
        /// <param name="path">the folder's path</param>
        /// <returns>null - in case of a problem (invalid/inaccessible path). array of strings - each string represents a path for a folder or file under 'path'</returns>
        public string[] getDir(string path)
        {
            return Directory.GetFileSystemEntries(path);
        }

        /// <summary>
        /// Wait for all threads to finish and exit 
        /// </summary>
        public void exit()
        {
            m_controller.Output("Initiating exit process, please wait for background process to exit properly.");
            foreach (Thread thread in m_threads)
                thread.Join();
            m_controller.Output("Model Terminated...");
        }

        /// <summary>
        /// Returns the file size in bytes
        /// </summary>
        /// <param name="path">the file path </param>
        /// <returns>file size in bytes</returns>
        public long fileSize(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Length;
        }
    }
}
