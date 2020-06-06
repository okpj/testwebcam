using MvvmLib.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TestWebCam.Model;
using TestWebCam.Model.CameraControls;
using TestWebCam.Model.Helpers;

namespace TestWebCam.ViewModel
{
    /// <summary>
    /// ViewModel для камеры
    /// </summary>
    public class CameraViewModel : BaseViewModel
    {
        private ICameraControl _cameraControl;
        
        public CameraViewModel()
        {
            _cameraControl = SelectCameraControl(SelectedLibrary);
            EnableLists = true;
            GetDevices();
            StartWebCamCommand = new DelegateCommand(StartWebCam);
            StopWebCamCommand = new DelegateCommand(StopWebCam);
            TakeSnapshotCommand = new DelegateCommand(TakeSnapshot);
            Events.Clossing += OnClossing;
        }


        #region Commands
        /// <summary>
        /// Комманда запуска камеры
        /// </summary>
        public ICommand StartWebCamCommand { get; set; }

        /// <summary>
        /// Команда остановки камеры
        /// </summary>
        public ICommand StopWebCamCommand { get; set; }

        /// <summary>
        /// Команда создания снимка
        /// </summary>
        public ICommand TakeSnapshotCommand { get; set; }

        #endregion

        #region Properties

        private BitmapImage _imageSource = null;
        /// <summary>
        /// Своство для привязки изображения с камеры
        /// </summary>
        public BitmapImage ImageSource
        {
            get => _imageSource;

            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        private IEnumerable<VideoDivice> _videoDivices;
        /// <summary>
        /// Список видео устройств
        /// </summary>
        public IEnumerable<VideoDivice> VideoDivices
        {
            get => _videoDivices;

            set
            {
                if (_videoDivices != value)
                {
                    _videoDivices = value;
                    OnPropertyChanged();
                }
            }
        }

        private VideoDivice _selectedVideoDivice;
        /// <summary>
        /// Выбранное устройство
        /// </summary>
        public VideoDivice SelectedVideoDivice
        {
            get => _selectedVideoDivice;

            set
            {
                if (_selectedVideoDivice != value)
                {
                    _selectedVideoDivice = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Список библиотек для работы с камерой
        /// </summary>
        public static IEnumerable<LibraryEnum> Libraries => Enum.GetValues(typeof(LibraryEnum)).Cast<LibraryEnum>();

        private LibraryEnum _selectedLibrary;

        /// <summary>
        /// Выбранная библиотека
        /// </summary>
        public LibraryEnum SelectedLibrary
        {
            get => _selectedLibrary;

            set
            {
                if (_selectedLibrary != value)
                {
                    _selectedLibrary = value;
                    OnPropertyChanged();
                    if (!_cameraControl.IsRunning)
                    {
                        _cameraControl = null;
                        _cameraControl = SelectCameraControl(_selectedLibrary);
                    }
                   
                }
            }
        }

        private bool _enableLists;

        /// <summary>
        /// Доступность выбора из комбобоксов
        /// </summary>
        public bool EnableLists
        {
            get => _enableLists;

            set
            {
                if (_enableLists != value)
                {
                    _enableLists = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Выбор контрола камеры
        /// </summary>
        /// <param name="libraryEnum"></param>
        /// <returns></returns>
        private BaseCameraControl SelectCameraControl(LibraryEnum libraryEnum)
        {
            switch (libraryEnum)
            {
                case LibraryEnum.EmguCV:
                    return new EmguCvCameraControl();
                case LibraryEnum.Aforge:
                    return new AforgeCameraControl();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Получить список камер
        /// </summary>
        private void GetDevices() => VideoDivices = _cameraControl.GetDevices();
       
        /// <summary>
        /// Запустить камеру
        /// </summary>
        private void StartWebCam()
        {
            EnableLists = false;
            _cameraControl.DataChanged += CameraControl_DataChanged;
            _cameraControl.StartCamera(SelectedVideoDivice?.Index ?? 0, SelectedVideoDivice?.DevicePath);
        }

        /// <summary>
        /// Реакция на событие изменения данных CameraControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CameraControl_DataChanged(object sender, BitmapImage e)
        {
            ImageSource = e;
        }

        /// <summary>
        /// Остановить камеру
        /// </summary>
        private void StopWebCam()
        {
            _cameraControl.DataChanged -= CameraControl_DataChanged;
            _cameraControl.StopCamera();
            ImageSource = null;
            EnableLists = true;


        }

        /// <summary>
        /// Сделать снимок
        /// </summary>
        private void TakeSnapshot()
        {
            if (ImageSource != null)
            {
                ImageSource.SaveToFile();
            }
        }

        /// <summary>
        /// Обработка события завершения работы
        /// </summary>
        private void OnClossing()
        {
            StopWebCam();
            Events.Clossing += OnClossing;
        }
        #endregion
    }
}
