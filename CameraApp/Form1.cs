using CameraLibrary.Base;
using CameraLibrary.Factory;
using CameraLibrary.Interfaces;
using CameraLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraApp
{
    public partial class Form1 : Form
    {
        private ICamera _camera;
        private bool _isConnected;
        private bool _syncing;
        private Timer _bufferTimer;

        public Form1()
        {
            InitializeComponent();
            cb_CameraType.DataSource = Enum.GetValues(typeof(CameraType));
            SetButtonsEnable(false);

            _bufferTimer = new Timer();
            _bufferTimer.Interval = 200;
            _bufferTimer.Tick += BufferTimer_Tick;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (!_isConnected)
            {
                var type = (CameraType)cb_CameraType.SelectedItem;
                _camera = CameraFactory.Create(type);

                if (_camera == null)
                {
                    WriteLog("相機類型未實作");
                    return;
                }

                if (_camera.Connect())
                {
                    _isConnected = true;
                    btn_Connect.Text = "Disconnect";
                    SetButtonsEnable(true);
                    WriteLog($"已連線到 {type} 相機");
                }
                else
                {
                    WriteLog($"連線到 {type} 相機失敗");
                }
            }
            else
            {
                try
                {
                    if (_camera is CameraBase cameraBase)
                        cameraBase.StopLive();

                    _camera.ImageCaptured -= OnImageCaptured;
                    _camera.Disconnect();
                }
                catch (Exception ex)
                {
                    WriteLog($"斷線時發生錯誤: {ex.Message}");
                }

                _isConnected = false;
                btn_Connect.Text = "Connect";
                SetButtonsEnable(false);
                WriteLog("已斷線");
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                _camera.ImageCaptured -= OnImageCaptured;
                _camera.ImageCaptured += OnImageCaptured;

                if (_camera is CameraBase cameraBase)
                {
                    cameraBase.StartLive();
                    _bufferTimer.Start();
                    WriteLog("已開始即時影像");
                }
            }
            catch (Exception ex)
            {
                WriteLog($"開始即時影像時發生錯誤: {ex.Message}");
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_camera is CameraBase cameraBase)
                    cameraBase.StopLive();
                _camera.ImageCaptured -= OnImageCaptured;
                _bufferTimer.Stop();
                WriteLog("已停止即時影像");
            }
            catch (Exception ex)
            {
                WriteLog($"停止即時影像時發生錯誤: {ex.Message}");
            }
        }

        private void OnImageCaptured(Bitmap bmp)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Bitmap>(OnImageCaptured), bmp);
            }
            else
            {
                pb_GrabImage.Image?.Dispose();
                pb_GrabImage.Image = (Bitmap)bmp.Clone();
            }
        }

        private void btn_GrabOne_Click(object sender, EventArgs e)
        {
            try
            {
                var bmp = _camera.Capture();

                if (bmp != null)
                {
                    pb_GrabImage.Image?.Dispose();
                    pb_GrabImage.Image = (Bitmap)bmp.Clone();
                    WriteLog("已擷取單張影像");
                }
                else
                {
                    WriteLog("未能擷取影像");
                }
            }
            catch (Exception ex)
            {
                WriteLog($"擷取影像時發生錯誤: {ex.Message}");
            }
        }

        private void SetButtonsEnable(bool isEnabled)
        {
            btn_Start.Enabled = isEnabled;
            btn_Stop.Enabled = isEnabled;
            btn_GrabOne.Enabled = isEnabled;
        }

        private void WriteLog(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(WriteLog), message);
            }
            else
            {
                tb_Log.AppendText($"[{DateTime.Now}] {message}{Environment.NewLine}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isConnected)
            {
                try
                {
                    if (_camera is CameraBase cameraBase)
                        cameraBase.StopLive();
                    _camera.ImageCaptured -= OnImageCaptured;
                    _camera.Disconnect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("關閉程式時發生錯誤: " + ex.Message);
                }
            }
        }

        private void trackBar_TargetFrame_Scroll(object sender, EventArgs e)
        {
            if (_syncing) return;
            _syncing = true;

            nud_TargetFrame.Value = trackBar_TargetFrame.Value;

            _syncing = false;
        }

        private void nud_TargetFrame_ValueChanged(object sender, EventArgs e)
        {
            if (_syncing) return;
            _syncing = true;

            trackBar_TargetFrame.Value = (int)nud_TargetFrame.Value;

            _syncing = false;
        }

        private void UpdateBufferCount()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateBufferCount));
            }
            else
            {
                var cameraBase = _camera as CameraBase;
                if (cameraBase == null)
                {
                    lb_BufferCount.Text = "Buffer: N/A";
                    return;
                }

                lb_BufferCount.Text = $"Buffer: {cameraBase.BufferCount} 張";
            }
        }

        private void btn_ShowTargetFrame_Click(object sender, EventArgs e)
        {
            if (!_isConnected || _camera == null)
            {
                WriteLog("尚未連線，無法顯示指定張數影像");
                return;
            }

            var cameraBase = _camera as CameraBase;
            if (cameraBase == null)
            {
                WriteLog("此相機不支援影像緩衝區功能");
                return;
            }

            var idx = (int)nud_TargetFrame.Value - 1;

            var bmp = cameraBase.GetBufferedImage(idx);

            if (bmp == null)
            {
                WriteLog($"指定的張數 {idx + 1} 無影像可顯示");
                return;
            }

            pb_GrabImage.Image?.Dispose();
            pb_GrabImage.Image = (Bitmap)bmp.Clone();

            WriteLog($"已顯示第 {idx + 1} 張影像");
        }

        private void BufferTimer_Tick(object sender, EventArgs e)
        {
            UpdateBufferCount();
        }
    }
}
