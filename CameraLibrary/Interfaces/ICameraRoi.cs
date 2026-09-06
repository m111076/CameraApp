using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary.Interfaces
{
    public interface ICameraRoi
    {
        void SetRoi(int width, int height, int offsetX = 0, int offsetY = 0);
    }
}
