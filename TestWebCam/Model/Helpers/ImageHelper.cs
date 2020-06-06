using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace TestWebCam.Model.Helpers
{
    /// <summary>
    /// Класс для работы с изображениями
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Путь до папки со снимками
        /// </summary>
        private static string _snapshotPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Snapshots");


        /// <summary>
        /// Конвертация Bitmap в BitmapImage
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static BitmapImage ConvertToBitmapImage(this Bitmap src)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                src.Save(memoryStream, ImageFormat.Png);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                memoryStream.Seek(0, SeekOrigin.Begin);
                image.StreamSource = memoryStream;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        /// <summary>
        /// Конвертация BitmapImage в Bitmap
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static Bitmap ConvertBitmapImageToBitmap(this BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

        /// <summary>
        /// Сохранение изображения в файл
        /// </summary>
        /// <param name="bitmapImage"></param>
        public static void SaveToFile(this BitmapImage bitmapImage)
        {
            var time = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var path = Path.Combine(_snapshotPath, $"Snapshot_{time}.png");
            if (!Directory.Exists(_snapshotPath))
            {
                Directory.CreateDirectory(_snapshotPath);
            }

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var memoryStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                    enc.Save(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
        }
    }
}
