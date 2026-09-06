using CameraLibrary.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CameraLibrary.Base
{
    public abstract class CameraBase : ICamera
    {
        private Thread _liveThread;
        private bool _running;
        public abstract bool Connect();
        public abstract void Disconnect();
        public abstract Bitmap Capture();
        public abstract bool StartGrabbing();
        public abstract void StopGrabbing();
        public event Action<Bitmap> ImageCaptured;
        protected BlockingCollection<Bitmap> _buffer = new BlockingCollection<Bitmap>(boundedCapacity: 200);
        public int BufferCount => _buffer.Count;

        public void StartLive()
        {
            if (_running)
                return;
            _running = true;
            _liveThread = new Thread(LiveLoop);
            _liveThread.Start();
        }

        public void StopLive()
        {
            if (!_running)
                return;
            _running = false;
            _liveThread.Join();
        }

        private void LiveLoop()
        {
            while (_running)
            {
                var image = Capture();

                if (image != null)
                {
                    ImageCaptured?.Invoke(image);

                    if (!_buffer.TryAdd(image))
                    {
                        _buffer?.TryTake(out var _);
                        _buffer?.TryAdd(image);
                    }
                }
                Thread.Sleep(50);
            }
        }

        public Bitmap GetBufferedImage(int iIdx)
        {
            if (_running)
                return null;

            if (iIdx < 0 || iIdx >= _buffer.Count)
                return null;

            var images = _buffer.ToArray();
            return images[iIdx];
        }
    }
}
