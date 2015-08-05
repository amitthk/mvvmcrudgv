using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MvvmCrudGv.ViewModels
{
     // Reuseable Stopwatch wrapper
    public class SimpleStopWatch : IDisposable
    {
        Stopwatch _watch;
        string _name;
        TimeSpan _totalTime;
        ILog _logger;
        DateTime _now;
        TimeSpan _totalElapsed { get { return (_totalTime + _watch.Elapsed); } }

        public string TotalElapsedTimeStampString
        {
            get { return (string.Format("{0}:{1}:{2}", _totalElapsed.Hours, _totalElapsed.Minutes, _totalElapsed.Seconds)); }
        }

        public string ElapsedTimeStampString
        {
            get { return (string.Format("{0}:{1}:{2}", _watch.Elapsed.Hours, _watch.Elapsed.Minutes, _watch.Elapsed.Seconds)); }
        }

        public string TotalTimeStampString
        {
            get { return string.Format("{0}:{1}:{2}", _totalTime.Hours, _totalTime.Minutes, _totalTime.Seconds); }
        }

        private string StartTimeStamp
        {
            get { return (string.Format("{0}:{1}:{2}", _now.Hour, _now.Minute, _now.Second)); }
        }

        public static SimpleStopWatch Start(string name, ILog logger)
        {
            return new SimpleStopWatch(name, logger);
        }

        private SimpleStopWatch(string name, ILog logger)
        {
            _name = name;
            _logger = logger;
            _watch = new Stopwatch();
            _now = DateTime.Now;
            _logger.InfoFormat("{0} - Starting Timer! {1}", _name, TimeElapsed());
            _watch.Start();
            _totalTime = TimeSpan.Zero;
        }

        public string TimeElapsed()
        {
            return string.Format("{0} - Elapsed time: {1}", _name, ElapsedTimeStampString);
        }



        public void Resume(string message = "")
        {
            _logger.InfoFormat("{0} - Restarting Timer! {1} ", string.IsNullOrWhiteSpace(message) ? _name : _name + " - " + message + " - ", TimeElapsed());
            _watch.Restart();
        }

        public void Pause()
        {
            _watch.Stop();
            _totalTime = _totalTime + _watch.Elapsed;
            _logger.InfoFormat("{0} - Resetting Timer! {1}", _name, TimeElapsed());
            _watch.Reset();
        }

        // Stops the stopwatch and prints time
        public void Stop()
        {
            _watch.Stop();
            _totalTime += _watch.Elapsed;
            _logger.Info(string.Format("{0} - Last Elapsed Time: {1},  Total Time Elapsed: {2}", _name, this.ElapsedTimeStampString, this.TotalTimeStampString));
        }


        public void Dispose()
        {
            this.Stop();
        }
    }
}
