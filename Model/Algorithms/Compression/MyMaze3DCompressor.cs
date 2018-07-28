using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Compression
{
    class MyMaze3DCompressor : ICompressor
    {
        /// <summary>
        /// Compress the array - naive compression
        /// </summary>
        /// <param name="data">bytes array to compress</param>
        /// <returns>compressed bytes array</returns>
        public byte[] compress(byte[] data)
        {
            List<byte> compressed = new List<byte>();
            int i = 0;
            while (i < data.Length)
            {
                byte b = data[i]; // current data
                compressed.Add(b); // put it in
                i++;
                // how many times it appears continuously?
                byte count = 0;
                while (i < data.Length && data[i] == b && count < byte.MaxValue) { count++; i++; }
                // write that count;
                compressed.Add(count);
            }
            // return the compressed array
            return compressed.ToArray();
        }
        /// <summary>
        /// decompress the array - naive compression
        /// </summary>
        /// <param name="data">compressed bytes array</param>
        /// <returns>decompressed bytes array</returns>
        public byte[] decompress(byte[] data)
        {
            List<byte> decompressed = new List<byte>();
            int i = 0;
            while (i < data.Length)
            {
                byte b = data[i];
                decompressed.Add(b);
                i++;
                for (int j = 0; i < data.Length && j < data[i]; decompressed.Add(b), j++) ;
                i++;
            }
            return decompressed.ToArray();
        }
    }
}
