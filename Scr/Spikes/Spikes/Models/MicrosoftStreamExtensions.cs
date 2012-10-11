using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace Spikes.Models
{
    public static class MicrosoftStreamExtensions
    {
        public static IRandomAccessStream AsRandomAccessStream(this Stream stream)
        {
            return new RandomStream(stream);
        }

        public static async Task<IRandomAccessStream> ConvertToRandomAccessStream(this MemoryStream memoryStream)
        {
            var randomAccessStream = new InMemoryRandomAccessStream();

            var outputStream = randomAccessStream.GetOutputStreamAt(0);
            var dw = new DataWriter(outputStream);
            var task = new Task(() => dw.WriteBytes(memoryStream.ToArray()));
            task.Start();
            
            await task;
            await dw.StoreAsync();
            
            await outputStream.FlushAsync();
            
            return randomAccessStream;
        }
    }


    public class RandomStream : IRandomAccessStream
    {
        Stream internstream;
        public RandomStream(Stream underlyingstream)
        {
            internstream = underlyingstream;
        }
        public IInputStream GetInputStreamAt(ulong position)
        {
            //THANKS Microsoft! This is GREATLY appreciated!
            internstream.Position = (long)position;
            return internstream.AsInputStream();
        }

        public IOutputStream GetOutputStreamAt(ulong position)
        {
            internstream.Position = (long)position;
            return internstream.AsOutputStream();
        }

        public void Seek(ulong position)
        {
            throw new System.NotImplementedException();
        }

        public IRandomAccessStream CloneStream()
        {
            throw new System.NotImplementedException();
        }

        public bool CanRead { get; private set; }
        public bool CanWrite { get; private set; }
        public ulong Position { get; private set; }

        public ulong Size
        {
            get
            {
                return (ulong)internstream.Length;
            }
            set
            {
                internstream.SetLength((long)value);
            }
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperationWithProgress<uint, uint> WriteAsync(IBuffer buffer)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<bool> FlushAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}