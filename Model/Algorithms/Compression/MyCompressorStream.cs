using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algorithms.Compression
{
    /// <summary>
    /// My Compressor Stream
    /// </summary>
    class MyCompressorStream : Stream
    {
        private Stream m_io;
        private const int m_BufferSize = 100;
        private Queue<byte> m_queue;
        private MyMaze3DCompressor m_naiveCompressor;
        private byte[] m_bytesReadFromStream;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="io">Stream - to be use for read and write</param>
        public MyCompressorStream(Stream io) : base()
        {
            m_io = io;
            m_queue = new Queue<byte>();
            m_naiveCompressor = new MyMaze3DCompressor();
            m_bytesReadFromStream = new byte[m_BufferSize];
        }

        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanSeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Flush the stream
        /// </summary>
        public override void Flush()
        {
            m_io.Flush();
        }

        /// <summary>
        /// Read compressed data from a file, decompress it and writes it into the buffer
        /// </summary>
        /// <param name="buffer">buffer for decompressed data</param>
        /// <param name="offset">start to insert data into the 'buffer' from this position</param>
        /// <param name="count">number of bytes to read</param>
        /// <returns>number of bytes that actually readed </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int r = 0;
            while (m_queue.Count < count && (r = m_io.Read(m_bytesReadFromStream, 0, m_BufferSize)) != 0)
            {
                // our source actually contain R bytes and if R<bufferSize then the rest of bytes are leftovers... 
                // let's cut them
                byte[] data = new byte[r];
                for (int i = 0; i < r; data[i] = m_bytesReadFromStream[i], i++) ;

                byte[] decompressed = m_naiveCompressor.decompress(data);
                // now, we'll put the decomprssed data in the queue; it is used as a buffer
                foreach (byte b in decompressed)
                {
                    m_queue.Enqueue(b);
                }
            }
            int bytesCount = Math.Min(m_queue.Count, count);

            for (int i = 0; i < bytesCount; i++)
            {
                buffer[i + offset] = m_queue.Dequeue();
            }
            return bytesCount;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// compress the data from the 'buffer' and write it to thr current stream.
        /// </summary>
        /// <param name="buffer">array of bytes to read data from</param>
        /// <param name="offset">start to read from the 'buffer' in this position</param>
        /// <param name="count">number of bytes to read from the buffer</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = new byte[count];
            for (int i = 0; i < count; data[i] = buffer[i + offset], i++) ;
            byte[] compressed = m_naiveCompressor.compress(data);
            m_io.Write(compressed, 0, compressed.Length);
        }
    }
}
