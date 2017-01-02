using AccelerometerDemo.Viewmodels;
using AccelerometerDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AccelerometerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _timer;
        private Accelerometer _accel;
        private MainPageViewModel _viewModel;
        
        public MainPage()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(512) };
            _timer.Tick += Timer_Tick;

            _accel = Accelerometer.GetDefault();

            this.DataContext = _viewModel = new ViewModels.MainPageViewModel();

            this.InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            var reading = _accel?.GetCurrentReading();
            if (reading != null)
            {
                _viewModel.ReadAccel(reading);
            }
        }

    }
}
