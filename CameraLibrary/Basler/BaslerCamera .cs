using Basler.Pylon;
using CameraLibrary.Base;
using CameraLibrary.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary.Basler
{
    public class BaslerCamera : CameraBase, ICameraRoi
    {
        private Camera _camera;
        private PixelDataConverter _converter = new PixelDataConverter();

        public override bool Connect()
        {
            try
            {
                _camera = new Camera();
                _camera.Open();

                _camera.Parameters[PLCamera.PixelFormat].SetValue(PLCamera.PixelFormat.Mono8);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Basler Connect Error: " + ex.Message);
                return false;
            }
        }

        public override void Disconnect()
        {
            try
            {
                StopGrabbing();

                foreach (var img in _buffer)
                {
                    img?.Dispose();
                }

                _buffer = new BlockingCollection<Bitmap>(boundedCapacity: 200);

                if (_camera != null)
                {
                    if (_camera.IsOpen)
                        _camera.Close();

                    _camera.Dispose();
                    _camera = null;
                }
            }
            catch { }
        }

        public void SetRoi(int width, int height, int offsetX = 0, int offsetY = 0)
        {
            var p = _camera.Parameters;

            _camera.StreamGrabber.Stop();

            p[PLCamera.OffsetX].SetValue(offsetX);
            p[PLCamera.OffsetY].SetValue(offsetY);
            p[PLCamera.Width].SetValue(width);
            p[PLCamera.Height].SetValue(height);
        }


        public override bool StartGrabbing()
        {
            try
            {
                if (_camera == null || !_camera.IsOpen)
                    return false;

                _camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Basler StartGrabbing Error: " + ex.Message);
                return false;
            }
        }

        public override void StopGrabbing()
        {
            try
            {
                if (_camera != null && _camera.StreamGrabber.IsGrabbing)
                    _camera.StreamGrabber.Stop();
            }
            catch { }
        }

        public override Bitmap Capture()
        {
            if (_camera == null || !_camera.IsOpen)
                return null;

            try
            {
                IGrabResult grab = _camera.StreamGrabber.GrabOne(50);

                if (!grab.GrabSucceeded)
                    return null;

                var w = grab.Width;
                var h = grab.Height;

                var buffer = new byte[w * h];
                _converter.OutputPixelFormat = PixelType.Mono8;
                _converter.Convert(buffer, grab);

                return CreateMonoBitmap(buffer, w, h);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Basler Capture Error: " + ex.Message);
                return null;
            }
        }

        private Bitmap CreateMonoBitmap(byte[] buffer, int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(i, i, i);
            bmp.Palette = pal;

            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format8bppIndexed);

            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            bmp.UnlockBits(data);

            return bmp;
        }
    }
}
