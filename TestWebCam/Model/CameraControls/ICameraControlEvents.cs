using System;
using System.Windows.Media.Imaging;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// События контрола
    /// </summary>
    public interface ICameraControlEvents
    {
        /// <summary>
        /// Событие изменение данных
        /// </summary>
        event EventHandler<BitmapImage> DataChanged;

        /// <summary>
        /// Вызвать событие изменения данныз
        /// </summary>
        /// <param name="bitmapImage"></param>
        void OnDataChanged(BitmapImage bitmapImage);
    }
}
