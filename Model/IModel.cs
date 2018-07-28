using ATP2016Project.Model.Algorithms.MazeGenerators;
using ATP2016Project.Model.Algorithms.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model
{
    /// <summary>
    /// An Interface represents a model
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Returns all folders and files inside the folder given by 'path'
        /// </summary>
        /// <param name="path">the folder's path</param>
        /// <returns>null - in case of a problem (invalid/inaccessible path). array of strings - each string represents a path for a folder or file under 'path'</returns>
        string[] getDir(string path);
        
        /// <summary>
        /// Generates a thread that generates a 3d maze
        /// </summary>
        /// <param name="mazeName">maze name - ID</param>
        /// <param name="columns">num of columns in the maze</param>
        /// <param name="rows">num of rows in the maze</param>
        /// <param name="levels">num of levels in the maze</param>
        void generate3dMaze(string mazeName, int columns, int rows, int levels);
        
        /// <summary>
        /// Returns the maze 'mazeName' 
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>3D array - maze board</returns>
        int[,,] getMaze(string mazeName);
        
        /// <summary>
        /// Compress the maze 'mazeName' and save it into a file with a given path
        /// </summary>
        /// <param name="mazeName">the name of the maze to save</param>
        /// <param name="path">file path</param>
        /// <returns>true - for success, false for failure</returns>
        bool saveMaze(string mazeName, string path);
        
        /// <summary>
        /// Loads a compressed maze from the given path ,decompress it and adds it to the dictionary
        /// also - if theres a previous existing solution for a maze with the same name - delete it
        /// </summary>
        /// <param name="path">path of the file to load</param>
        /// <param name="mazeName">maze name</param>
        void loadMaze(string path, string mazeName);
        
        /// <summary>
        /// Calculates the maze size in the memory and returns it by bytes
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>maze size in the memory in bytes</returns>
        int mazeSize(string mazeName);
        
        /// <summary>
        /// Returns the file size in bytes
        /// </summary>
        /// <param name="path">the file path </param>
        /// <returns>file size in bytes</returns>
        long fileSize(string path);
        
        /// <summary>
        /// Generates a thread that solves the maze 'mazeName'
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="algorithm">algorithm to solve with</param>
        void solveMaze(string mazeName, string algorithm);
        
        /// <summary>
        /// Returns the solution for maze 'mazeName'
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns>null - if the solution doesnt exists, else returns the solution </returns>
        Solution displaySolution(string mazeName);
        
        /// <summary>
        /// Checks if the maze 'mazeName' exists in the system
        /// </summary>
        /// <param name="mazeName"> maze name</param>
        /// <returns>true if the maze exists, false otherwise</returns>
        bool mazeExists(string mazeName);
        
        /// <summary>
        /// Wait for all threads to finish and exit 
        /// </summary>
        void exit();
    }
}
