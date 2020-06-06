using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// Управление камерой
    /// </summary>
    public interface ICameraControl : ICameraControlEvents
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
        /// Получить видео устройства
        /// </summary>
        /// <returns></returns>
        List<VideoDivice> GetDevices();

        /// <summary>
        /// Признак того, что камера запущена
        /// </summary>
        bool IsRunning { get; }

    }
}
