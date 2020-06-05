using System;

namespace TestWebCam.Model.CameraControls
{
    /// <summary>
    /// Видео устройства
    /// </summary>
    public class VideoDivice
    {
        /// <summary>
        /// Индекс
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid ClassID { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь до устройства
        /// </summary>
        public string DevicePath { get; set; }
    }
}
