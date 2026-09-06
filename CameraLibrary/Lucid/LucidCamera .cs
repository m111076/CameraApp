using ArenaNET;
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

namespace CameraLibrary.Lucid
{
    public class LucidCamera : CameraBase
    {
        private ISystem _system;
        private IDevice _device;

        public override bool Connect()
        {
            try
            {
                // 開 System（你的 SDK 正確用法）
                _system = ArenaNET.Arena.OpenSystem();

                // 更新設備
                _system.UpdateDevices(5000);

                if (_system.Devices.Count == 0)
                {
                    return false;
                }

                // 先拿第一顆相機（你可改成用 SerialNumber）
                IDeviceInfo info = _system.Devices[0];
                _device = _system.CreateDevice(info);

                // Stream 設定（依照 Cs_Save_Bitmap 範例）
                var autoPacket = (IBoolean)_device.TLStreamNodeMap.GetNode("StreamAutoNegotiatePacketSize");
                autoPacket.Value = true;

                var resend = (IBoolean)_device.TLStreamNodeMap.GetNode("StreamPacketResendEnable");
                resend.Value = true;

                // PixelFormat = Mono8
                var pixelFormat = _device.NodeMap.GetNode("PixelFormat") as IEnumeration;
                pixelFormat?.FromString("Mono8");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lucid Connect Error: " + ex.Message);
                return false;
            }
        }

        public override void Disconnect()
        {
            try
            {
                StopGrabbing();

                foreach (var img in _buffer)
                    img?.Dispose();

                _buffer = new BlockingCollection<Bitmap>(boundedCapacity: 200);

                if (_device != null)
                {
                    _system.DestroyDevice(_device);
                    _device = null;
                }

                if (_system != null)
                {
                    ArenaNET.Arena.CloseSystem(_system);
                    _system = null;
                }
            }
            catch { }
        }

        public override bool StartGrabbing()
        {
            try
            {
                if (_device == null)
                    return false;

                _device.StartStream();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lucid StartGrabbing Error: " + ex.Message);
                return false;
            }
        }

        public override void StopGrabbing()
        {
            try
            {
                if (_device != null)
                    _device.StopStream();
            }
            catch { }
        }

        public override Bitmap Capture()
        {
            if (_device == null)
                return null;

            try
            {
                ArenaNET.IImage img = _device.GetImage(2000);

                Bitmap bmp = img.Bitmap;

                _device.RequeueBuffer(img);

                return bmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lucid Capture Error: " + ex.Message);
                return null;
            }
        }

        public void SetRoi(int width, int height, int offsetX = 0, int offsetY = 0)
        {
            if (_device == null)
                return;

            try
            {
                // ROI 必須在停止串流時設定（官方範例習慣）
                _device.StopStream();

                ArenaNET.INodeMap nodeMap = _device.NodeMap;

                // 取得四個 ROI Node
                var nOffsetX = nodeMap.GetNode("OffsetX") as ArenaNET.IInteger;
                var nOffsetY = nodeMap.GetNode("OffsetY") as ArenaNET.IInteger;
                var nWidth = nodeMap.GetNode("Width") as ArenaNET.IInteger;
                var nHeight = nodeMap.GetNode("Height") as ArenaNET.IInteger;

                // ====== 檢查 Node 是否可用（依照 Explore_Nodes） ======
                if (!nOffsetX.IsAvailable || !nOffsetX.IsWritable) return;
                if (!nOffsetY.IsAvailable || !nOffsetY.IsWritable) return;
                if (!nWidth.IsAvailable || !nWidth.IsWritable) return;
                if (!nHeight.IsAvailable || !nHeight.IsWritable) return;

                // ====== 合法性檢查（依照 Explore_NodeTypes） ======
                // OffsetX
                if (offsetX < nOffsetX.Min) offsetX = (int)nOffsetX.Min;
                if (offsetX > nOffsetX.Max) offsetX = (int)nOffsetX.Max;

                // OffsetY
                if (offsetY < nOffsetY.Min) offsetY = (int)nOffsetY.Min;
                if (offsetY > nOffsetY.Max) offsetY = (int)nOffsetY.Max;

                // Width
                if (width < nWidth.Min) width = (int)nWidth.Min;
                if (width > nWidth.Max) width = (int)nWidth.Max;

                // Height
                if (height < nHeight.Min) height = (int)nHeight.Min;
                if (height > nHeight.Max) height = (int)nHeight.Max;

                // ====== 設定 ROI（依照 NodeTypes 的 Integer.Value setter） ======
                nOffsetX.Value = offsetX;
                nOffsetY.Value = offsetY;
                nWidth.Value = width;
                nHeight.Value = height;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lucid SetRoi Error: " + ex.Message);
            }
        }
    }
}
