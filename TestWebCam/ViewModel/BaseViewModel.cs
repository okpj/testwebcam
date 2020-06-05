using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TestWebCam.ViewModel
{
    /// <summary>
    /// Базовый ViewModel
    /// </summary>
    public class BaseViewModel : DependencyObject, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Обработчик изменения свойств
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
