using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// Базовый класс для управления камерой
    /// </summary>
    public abstract class BaseCameraControl : ICameraControl
    {
        public List<VideoDivice> GetDevices()
        {
            var devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            return devices.Select((x, i) => new VideoDivice { Index = i, ClassID = x.ClassID, Name = x.Name, DevicePath = x.DevicePath }).ToList();

        }

        public abstract void StartCamera(int? cameraIndex, string path = null);

        public abstract void StopCamera();

        public bool IsRunning { get; set; }

        public event EventHandler<BitmapImage> DataChanged;
        public void OnDataChanged(BitmapImage bitmapImage) => Dispatcher.CurrentDispatcher.Invoke(() => DataChanged?.Invoke(null, bitmapImage));
    }
}
