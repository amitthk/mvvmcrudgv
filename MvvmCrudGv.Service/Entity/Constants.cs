using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrudGv.Service.Entity
{
    public static class Constants
    {
        private static string _TickerSoundFileLocation = Utility.getAbsolutePath("Resources\\Sound", "clock-tick1.wav");
        public static string TickerSoundFileLocation
        {
            get { return _TickerSoundFileLocation; }
            set { _TickerSoundFileLocation = value; }
        }
    }
}
