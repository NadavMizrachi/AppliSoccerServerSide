using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Timers;

namespace AppliSoccerEngine
{
    public class ScheduleTask
    {
        private BackgroundWorker _worker;

        public ScheduleTask(Action action, int seconds)
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += (object sender, DoWorkEventArgs e) => action();
            Timer timer = new Timer(1000 * seconds);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
        }

        //void worker_DoWork(object sender, DoWorkEventArgs e)
        //{
            
        //}


    }
}
