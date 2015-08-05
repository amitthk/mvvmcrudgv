using MvvmCrudGv.Common;
using MvvmCrudGv.Common.Messaging;
using MvvmCrudGv.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace MvvmCrudGv.ViewModels
{
    public class TodoPlayViewModel : BaseViewModel
    {
        IEventAggregator _eventAggregator;

        private readonly ICommand _GoBackCmd;
        public ICommand GoBackCmd { get { return (_GoBackCmd); } }
        private readonly ICommand _PauseCmd;
        public ICommand PauseCmd { get { return (_PauseCmd); } }
        private readonly ICommand _ResumeCmd;
        public ICommand ResumeCmd { get { return (_ResumeCmd); } }
        private readonly ICommand _StopCmd;
        public ICommand StopCmd { get { return (_StopCmd); } }
        private TodoViewModel _currentTodo;
        private SoundPlayer mediaPlayer;
        SimpleStopWatch _stopWatch;
        Timer _timer;
        Timer _soundTimer;
        bool breakPeriod = false;

        private Uri _TickerSoundFileLoc;

        public Uri TickerSoundFileLoc
        {
            get { return _TickerSoundFileLoc; }
            set
            {
                if (value != _TickerSoundFileLoc)
                {
                    _TickerSoundFileLoc = value;
                    OnPropertyChanged(() => TickerSoundFileLoc);
                }
            }
        }

        private bool _IsContinuePlay = false;

        public bool IsContinuePlay
        {
            get { return _IsContinuePlay; }
            set
            {
                _IsContinuePlay = value;
                OnPropertyChanged(() => IsContinuePlay);
                //if (IsContinuePlay)
                //{
                //    System.Windows.MessageBox.Show("Will Continue Playing");
                //}
                //else
                //{
                //    System.Windows.MessageBox.Show("Won't continue playing!");
                //}
            }
        }


        public TodoViewModel CurrentTodo
        {
            get { return _currentTodo; }
            set
            {
                if ((value != null) && (_currentTodo != value))
                {
                    _currentTodo = value;
                    OnPropertyChanged(() => CurrentTodo);
                }
            }
        }

        private string _CurrentElapsed;

        public string CurrentElapsed
        {
            get { return _CurrentElapsed; }
            set
            {
                if (value != _CurrentElapsed)
                {
                    _CurrentElapsed = value;
                    OnPropertyChanged(() => CurrentElapsed);
                }
            }
        }

        private bool _IsRunning = false;

        public bool IsRunning
        {
            get { return _IsRunning; }
            set
            {
                _IsRunning = value;
                OnPropertyChanged(() => IsRunning);
                if (IsRunning)
                {
                    _timer.Start();
                    _soundTimer.Start();
                    _stopWatch.Resume();
                    //_eventAggregator.Publish<Views.ObjMessage>(new Views.ObjMessage("TodoPlay", "Play"));
                }
                else
                {
                    _timer.Stop();
                    _soundTimer.Stop();
                    _stopWatch.Pause();
                    //_eventAggregator.Publish<Views.ObjMessage>(new Views.ObjMessage("TodoPlay", "Pause"));
                }
            }
        }


        public TodoPlayViewModel()
        {
            _eventAggregator = App.eventAggregator;
            _eventAggregator.Subscribe<MvvmCrudGv.Views.ObjMessage>(HandleCurrentTodo);
            
            _GoBackCmd = new RelayCommand(ExecGoBackCmd, CanGoBackCmd);
            _PauseCmd = new RelayCommand(ExecPauseCmd, CanPauseCmd);
            _ResumeCmd = new RelayCommand(ExecResumeCmd, CanResumeCmd);
            _StopCmd = new RelayCommand(ExecStopCmd, CanStopCmd);
            mediaPlayer = new SoundPlayer();
            _timer = new Timer(1000);//this timer ticks every 1 second
            _timer.Elapsed += TimerTickHandler;
            LoadTickerSound();
            _soundTimer = new Timer(1000);
            _soundTimer.Elapsed += SoundTimer_Tick;
            _stopWatch = SimpleStopWatch.Start("TodPlayView Timer", log4net.LogManager.GetLogger(typeof(TodoPlayViewModel)));
            IsRunning = true;

        }

        private bool CanStopCmd(object obj)
        {
            return ((IsRunning) && (!breakPeriod));
        }

        private void ExecStopCmd(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanPauseCmd(object obj)
        {
            return (IsRunning);
        }

        private void ExecPauseCmd(object obj)
        {
            IsRunning = false;
        }

        private bool CanResumeCmd(object obj)
        {
            return (!IsRunning);
        }

        private void ExecResumeCmd(object obj)
        {
            IsRunning = true;
        }

        private void TimerTickHandler(object sender, ElapsedEventArgs e)
        {
            CurrentElapsed = _stopWatch.TotalElapsedTimeStampString;
        }

        private void HandleCurrentTodo(MvvmCrudGv.Views.ObjMessage obj)
        {
            if (obj.Notification.Equals("TodoPlayView"))
            {
                var td = (TodoViewModel)obj.PayLoad;
                CurrentTodo = td;
            }
        }

        private void ExecGoBackCmd(object obj)
        {
            //Perfom Activity
            //Logic for determining next view, then ...
            IsRunning = false;
            _eventAggregator.Publish<Views.NavMessage>(new Views.NavMessage("Home"));
        }

        private bool CanGoBackCmd(object obj)
        {
            //Add Enable-disable logic here
            if (true)
            {
                return (true);
            }
        }

        private void SoundTimer_Tick(object sender, EventArgs e)
        {
            //if (Settings.Instance.PlayTickerSound && !breakPeriod)
                // SystemSounds.Beep.Play();
            if(!breakPeriod)
                mediaPlayer.PlaySync();
        }

        private void LoadTickerSound()
        {
            if (Utility.fileExists(Constants.TickerSoundFileLocation))
                mediaPlayer.SoundLocation = Constants.TickerSoundFileLocation;
            else
                mediaPlayer.Stream = Utility.ReadFileStream(Constants.TickerSoundFileLocation);
            mediaPlayer.Load();
        }

    }
}
