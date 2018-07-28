using ATP2016Project.Controller;
using ATP2016Project.Model;
using ATP2016Project.Model.Algorithms.Compression;
using ATP2016Project.Model.Algorithms.MazeGenerators;
using ATP2016Project.Model.Algorithms.Search;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("************  testMaze2dGenerator  ************");
            testMaze2dGenerator(new SimpleMaze2dGenerator());
            Console.WriteLine("\nPlease enter any key to continue \n");
            Console.ReadKey();
            Console.WriteLine("************  testMaze3dGenerator  ************");
            testMaze3dGenerator(new MyMaze3dGenerator());
            Console.WriteLine("\nPlease enter any key to continue \n");
            Console.ReadKey();*/
            //Console.WriteLine("*******  testSearchAlgorithms  *******\n");
            //testSearchAlgorithms();
            //testCompressor();
            //TestNaiveCompressorStream();
            //testMyCompressorStream();
            testMVC();
        }

        private static void testMVC()
        {
            Console.WriteLine("********** test MVC **********");
            IController controller = new MyController();
            IModel model = new MyModel(controller);
            IView view = new CLI(controller);
            controller.SetModel(model);
            controller.SetView(view);
            view.Start();
        }

        private static void testCompressor()
        {
            int[] size3D = { 3, 6, 6 }; // (z,y,x)
            IMazeGenerator mazeGenerator3d = new MyMaze3dGenerator();
            Maze3d maze =(Maze3d) mazeGenerator3d.generate(size3D);
            maze.print();
            Console.ReadKey();
            byte[] decomp = maze.toByteArray();
            for(int i = 0; i < decomp.Length; i ++)
            {
                Console.Write(decomp[i] + " ");
            }
            ICompressor compressor = new MyMaze3DCompressor();
            Console.WriteLine("\ncompression : ");
            byte[] comp = compressor.compress(decomp);
            for (int i = 0; i < comp.Length; i++)
            {
                Console.Write(comp[i] + " ");
            }
            decomp = compressor.decompress(comp);
            Console.WriteLine("\ndecompression : ");
            for (int i = 0; i < decomp.Length; i++)
            {
                Console.Write(decomp[i] + " ");
            }
        }

        private static void testMyCompressorStream()
        {
            Console.WriteLine("*******  testMyCompressorStream  *******\n");
            int[] size3D = { 3, 6, 7 }; // (z,y,x)
            IMazeGenerator mazeGenerator3d = new MyMaze3dGenerator();
            Maze3d maze = (Maze3d)mazeGenerator3d.generate(size3D);
            // save the maze to a file – compressed
            using (FileStream fileOutStream = new FileStream(@"D:\1.maze.txt", FileMode.Create))
            {
                using (Stream outStream = new MyCompressorStream(fileOutStream))
                {
                    outStream.Write(maze.toByteArray(), 0, maze.toByteArray().Length);
                    outStream.Flush();
                }
            }
            byte[] mazeBytes;
            using (FileStream fileInStream = new FileStream(@"D:\1.maze.txt", FileMode.Open))
            {
                using (Stream inStream = new MyCompressorStream(fileInStream))
                {
                    mazeBytes = new byte[maze.toByteArray().Length];
                    inStream.Read(mazeBytes, 0, mazeBytes.Length);
                }
            }
            Maze3d loadedMaze = new Maze3d(mazeBytes);
            Console.WriteLine("The original maze : ");
            maze.print();
            maze.getStartPosition().print();
            maze.getGoalPosition().print();

            Console.WriteLine("The decompress maze : ");
            loadedMaze.print();
            loadedMaze.getStartPosition().print();
            loadedMaze.getGoalPosition().print();
            Console.WriteLine(loadedMaze.Equals(maze));

        }

        private static void CompressFile(string originalFilePath, string compressedFilePath)
        {
            //Compress bart.txt => bart_compressed.txt
            using (FileStream originalFileStream = new FileStream(originalFilePath, FileMode.Open))
            {
                using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Create))
                {
                    using (MyCompressorStream myCompressorStream = new MyCompressorStream(compressedFileStream))
                    {
                        //Read original bart
                        byte[] buffer = new byte[50];
                        int r = 0;
                        while ((r = originalFileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            myCompressorStream.Write(buffer, 0, r);
                        }
                    }
                }
            }
        }

        private static void DecompressFile(string compressedFilePath, string decompressedFilePath)
        {
            // decompress bart_compressed.txt => bart_decompressed.txt
            using (FileStream compressesFileStream = new FileStream(compressedFilePath, FileMode.Open))
            {
                using (FileStream decompressedFileStream = new FileStream(decompressedFilePath, FileMode.Create))
                {
                    using (MyCompressorStream myCompressorStream = new MyCompressorStream(compressesFileStream))
                    {
                        byte[] data = new byte[100];
                        int r = 0;
                        while ((r = myCompressorStream.Read(data, 0, 100)) != 0)
                        {
                            decompressedFileStream.Write(data, 0, data.Length);
                        }
                    }
                }
            }
        }

        private static void testMaze2dGenerator(IMazeGenerator generator)
        {
            int[] size2D = { 27, 13 }; // (y,x)
            Console.WriteLine(generator.geasureAlgorithmTime(size2D));
            AMaze maze = generator.generate(size2D);
            APosition start = maze.getStartPosition();
            start.print();
            maze.getGoalPosition().print();
            maze.print();
        }

        private static void testMaze3dGenerator(IMazeGenerator generator)
        {
            int[] size3D = { 3, 13, 17 }; // (z,y,x)
            Console.WriteLine(generator.geasureAlgorithmTime(size3D));
            AMaze maze = generator.generate(size3D);
            APosition start = maze.getStartPosition();
            start.print();
            maze.getGoalPosition().print();
            maze.print();
        }
        /// <summary>
        /// generate maze and calls print for both BFS and DFS searching algorithms
        /// </summary>
        private static void testSearchAlgorithms()
        {
            int[] size3D = { 4, 6, 6 }; // (z,y,x)
            IMazeGenerator mazeGenerator3d = new MyMaze3dGenerator();
            ISearchable maze = new SearchableMaze3D(mazeGenerator3d.generate(size3D));

            print("BFS", new BFS(), maze);
            Console.WriteLine("\nPlease enter any key to continue\n");
            Console.ReadKey();
            print("DFS", new DFS(), maze);
        }
        /// <summary>
        /// Prints the test results for either BFS or DFS searching algorithms (defined by args)
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="searching"></param>
        /// <param name="maze"></param>
        private static void print(string algo, ISearchingAlgorithm searching, ISearchable maze)
        {
            Solution solution = searching.Solve(maze);
            Console.WriteLine("Problem to solve using {0} Algorithm :", algo);
            Console.WriteLine("**************************************");
            maze.print();
            if (solution.isSolutionExists())
            {
                Console.WriteLine("Solution Using " + algo + " algorithm :");
                Console.WriteLine("**************************************");
                Console.WriteLine("(x,y,z)");
                solution.printSolution();
                Console.WriteLine("\nNodes generated: " + searching.getNumberOfNodesEvaluated());
                Console.WriteLine("\nSolving time (miliseconds): " + searching.GetSolvingTimeMiliseconds());
                //Console.WriteLine("\nSolution length (number of nodes) : " + solution.getSolutionSteps());
            }
            else
            {
                Console.WriteLine("No Solution found!");
            }

        }
    }
}
