using Serilog;
using System.IO;
using System.Windows;
using TestWebCam.Model;

namespace TestWebCam
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        void StartApp(object sender, StartupEventArgs se)
        {
            Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .MinimumLevel.Debug()
                 .WriteTo.File(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "logs", "log.log"), rollingInterval: RollingInterval.Day)
                 .CreateLogger();

            var mainWindow = new MainWindow();
            mainWindow.ShowDialog();

            Events.OnClossing();
        }
    }
}
