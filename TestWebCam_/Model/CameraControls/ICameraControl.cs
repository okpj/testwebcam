using System;
using System.Windows.Media.Imaging;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// Управление камерой
    /// </summary>
    public interface ICameraControl
    {
        /// <summary>
        /// Запуск камеры
        /// </summary>
        /// <param name="cameraIndex">Индекс устройства</param>
        /// <param name="path">Путь до устройства</param>
        void StartCamera(int? cameraIndex, string path = null);

        /// <summary>
        /// Остановка камеры
        /// </summary>
        void StopCamera();

        /// <summary>
        /// Признак того, что камера запущена
        /// </summary>
        bool IsRunning { get; set; }

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
