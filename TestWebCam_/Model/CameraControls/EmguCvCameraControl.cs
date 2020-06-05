using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Serilog;
using System;
using System.Drawing;
using System.Reflection;
using TestWebCam.Model.Helpers;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// Управление камерой средствами EmguCV
    /// </summary>
    public class EmguCvCameraControl : BaseCameraControl
    {

        /// <summary>
        /// Изобраение с камеры
        /// </summary>
        private VideoCapture _videoCapture;

        public override void StartCamera(int? cameraIndex, string path)
        {
            if (_videoCapture == null)
            {
               
                Log.Information("Start video");
                try
                {
                    IsRunning = true;
                    _videoCapture = new VideoCapture(cameraIndex.Value, VideoCapture.API.Any);
                    _videoCapture.ImageGrabbed += ProcessFrame;
                    _videoCapture.Start();

                }
                catch (Exception ex)
                {
                    IsRunning = false;
                    Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                }
            }
        }


        private void ProcessFrame(object sender, EventArgs e)
        {
            Mat mat = null;
            Bitmap image = null;
            try
            {
                mat = new Mat();
                _videoCapture.Retrieve(mat);
                image = mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap();
                var bitmapImage = image.ConvertToBitmapImage();
                OnDataChanged(bitmapImage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                image?.Dispose();
                mat?.Dispose();
            }
        }

        public override void StopCamera()
        {
            if (_videoCapture != null)
            {
                Log.Information("Stop video");
                try
                {
                    IsRunning = false;
                    _videoCapture.ImageGrabbed -= ProcessFrame;
                    _videoCapture.Stop();
                    _videoCapture.Dispose();
                    _videoCapture = null;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                }
            }
        }
    }
}
