using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace WorldClock
{
    public class Clock : INotifyPropertyChanged
    {
        public string CST { get; private set; }
        public string EST { get; private set; }
        public string JST { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public Clock()
        {
            Run();
        }
        private async void Run()
        {
            DateTimeOffset lastTime;
            CultureInfo jaJP = new CultureInfo("ja-JP");
            //jaJP.DateTimeFormat.Calendar = new JapaneseCalendar();
            while (true)
            {
                await Task.Delay(10); 
                var nowTime = DateTimeOffset.Now;
                DateTime jstDateTime = DateTime.UtcNow + new TimeSpan(+9, 0, 0);
                DateTime estDateTime = DateTime.Now + new TimeSpan(+1, 0, 0);
                var estTime = new DateTimeOffset(estDateTime);
                var jstTime = new DateTimeOffset(jstDateTime);
                if (lastTime.Second != nowTime.Second)
                {                    
                    this.CST = nowTime.ToString("HH:mm:ss");
                    this.JST = jstTime.ToString("HH:mm:ss");
                    this.EST = estTime.ToString("HH:mm:ss");
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CST"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EST"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("JST"));
                    }
                        
                    lastTime = nowTime;
                }
            }
        }
    }
}

