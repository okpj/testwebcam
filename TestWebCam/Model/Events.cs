using System;

namespace TestWebCam.Model
{
    /// <summary>
    /// События
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Событие закрытия
        /// </summary>
        public static Action Clossing;

        /// <summary>
        /// Вызов события закрытия
        /// </summary>
        public static void OnClossing() => Clossing.Invoke();
    }
}
