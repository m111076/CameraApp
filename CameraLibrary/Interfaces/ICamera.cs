using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CameraLibrary.Interfaces
{
    public interface ICamera
    {
        bool Connect();
        void Disconnect();
        Bitmap Capture();
        bool StartGrabbing();
        void StopGrabbing();
        event Action<Bitmap> ImageCaptured;
    }
}
