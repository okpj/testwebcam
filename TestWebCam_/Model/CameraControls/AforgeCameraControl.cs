using AForge.Video.DirectShow;
using Serilog;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using TestWebCam.Model.Helpers;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// Управление камерой средствами Aforge
    /// </summary>
    public class AforgeCameraControl : BaseCameraControl
    {

        private VideoCaptureDevice _videoCapture;
        public override void StartCamera(int? cameraIndex, string path)
        {
            try
            {
                if (_videoCapture == null)
                {
                    IsRunning = true;
                    Log.Information("Start video");
                    _videoCapture = new VideoCaptureDevice(path);
                    _videoCapture.NewFrame += VideoSource_NewFrame;

                    var _videoResolution = _videoCapture.VideoCapabilities?.OrderByDescending(x => x.FrameSize.Width * x.FrameSize.Height)?.FirstOrDefault();
                    if (_videoResolution != null)
                    {
                        _videoCapture.VideoResolution = _videoResolution;
                    }
                    _videoCapture.Start();
                }
            }
            catch (Exception ex)
            {
                IsRunning = false;
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        private void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {

            Bitmap bitmap = null;
            try
            {
                eventArgs.Frame.RotateFlip(RotateFlipType.Rotate180FlipY);
                bitmap = (Bitmap)eventArgs.Frame.Clone();
                OnDataChanged(bitmap.ConvertToBitmapImage());
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                bitmap?.Dispose();
            }
        }

        public override void StopCamera()
        {
            try
            {
                if (_videoCapture != null)
                {
                    IsRunning = false;
                    Log.Information("Stop video");
                    _videoCapture.NewFrame -= VideoSource_NewFrame;
                    _videoCapture.SignalToStop();
                    _videoCapture = null;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
