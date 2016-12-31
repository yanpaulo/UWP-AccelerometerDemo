using AccelerometerDemo.Viewmodels;
using System.Collections.Generic;

namespace AccelerometerDemo
{
    public static class PinListExtensions
    {
        public static void SwitchAll(this List<PinViewModel> list, bool value)
        {
            foreach (var item in list)
            {
                item.IsOn = value;
            }
        }
    }
}