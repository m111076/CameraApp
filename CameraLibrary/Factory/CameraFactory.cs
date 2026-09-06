using CameraLibrary.Basler;
using CameraLibrary.Interfaces;
using CameraLibrary.Lucid;
using CameraLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraLibrary.Factory
{
    public static class CameraFactory
    {
        public static ICamera Create(CameraType type)
        {
            switch (type)
            {
                case CameraType.Basler:
                    return new BaslerCamera();

                case CameraType.Lucid:
                    return new LucidCamera();

                //case CameraType.Simulation:
                //return new SimCamera();

                default:
                    throw new ArgumentException("Unknown camera type");
            }
        }
    }
}
