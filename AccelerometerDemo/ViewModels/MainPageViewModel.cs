using AccelerometerDemo.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Devices.Sensors;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;

namespace AccelerometerDemo.ViewModels
{
    public class MainPageViewModel
    {
        private List<PinViewModel>
            xPins = new List<PinViewModel>(),
            yPins = new List<PinViewModel>();

        public PinViewModel[] XPins => xPins.ToArray();
        public PinViewModel[] YPins => yPins.ToArray();

        public MainPageViewModel()
        {
            var isGpioAvailable = ApiInformation.IsApiContractPresent("Windows.Devices.DevicesLowLevelContract", 2);
            var gpioController = isGpioAvailable ? GpioController.GetDefault() : null;
            
            for (int i = 2; i <= 8; i++)
            {
                var pin = new PinViewModel();

                if (gpioController != null)
                {
                    var gPin = gpioController.OpenPin(i);
                    gPin.SetDriveMode(GpioPinDriveMode.Output);
                    gPin.Write(GpioPinValue.Low);

                    pin.GpioPin = gPin;
                }
                xPins.Add(pin);
            }
            for (int i = 9; i <= 12; i++)
            {
                var pin = new PinViewModel();

                if (gpioController != null)
                {
                    var gPin = gpioController.OpenPin(i);
                    gPin.SetDriveMode(GpioPinDriveMode.Output);
                    gPin.Write(GpioPinValue.Low);

                    pin.GpioPin = gPin;
                }
                yPins.Add(pin);
            }
        }

        public void ReadAccel(AccelerometerReading reading)
        {
            var xSign = Math.Sign(reading.AccelerationX);
            var xCenterPinIndex = xPins.Count / 2;
            var xAxisPinsCount = xCenterPinIndex + (xPins.Count % 2);

            //Number of pins on X axis that should light up.
            var xCount = (int)((xAxisPinsCount + 1) * Math.Abs(reading.AccelerationX)) + 1;

            //Number of pins on Y axis that should light up.
            var yCount = (int)((yPins.Count + 1) * (1.0d - Math.Abs(reading.AccelerationY)));

            xPins.SwitchAll(false);
            for (int i = 0; i < xCount && i < xAxisPinsCount; i++)
            {
                var pinIndex = xCenterPinIndex + xSign * i;
                xPins[pinIndex].IsOn = true;
            }

            yPins.SwitchAll(false);
            for (int i = 0; i < yCount && i < yPins.Count; i++)
            {
                yPins[i].IsOn = true;
            }

        }

    }
}
