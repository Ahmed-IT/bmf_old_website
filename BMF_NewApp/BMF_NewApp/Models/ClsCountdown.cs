using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BMF_NewApp.Models
{
    public class ClsCountdown : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ID { get; set; }
        public string Round_Number { get; set; }
        public DateTime DateTime_Start { get; set; }
        public DateTime DateTime_End { get; set; }
        public ClsEventCategory EventCategory { get; set; }

        public ClsCountdown()
        {
            EventCategory = new ClsEventCategory();
        }

        public string EventFullName
        {
            get
            {
                return EventCategory.Title + " " + Round_Number;
            }
        }

        public string EventDate
        {
            get
            {
                string eventDate = DateTime_Start.ToString("dd MMM yyyy");

                return eventDate;
            }
            set
            {

            }
        }

        public string _sec;
        public string Sec
        {
            get { return _sec; }
            set { if (value != _sec) { _sec = value; NotifyPropertyChanged(); } }
        }

        public string _min;
        public string Min
        {
            get { return _min; }
            set { if (value != _min) { _min = value; NotifyPropertyChanged(); } }
        }

        public string _hour;
        public string Hour
        {
            get { return _hour; }
            set { if (value != _hour) { _hour = value; NotifyPropertyChanged(); } }
        }

        public string _day;
        public string Day
        {
            get { return _day; }
            set { if (value != _day) { _day = value; NotifyPropertyChanged(); } }
        }

        public bool _hasEvent;
        public bool HasEvent
        {
            get { return _hasEvent; }
            set { if (value != _hasEvent) { _hasEvent = value; NotifyPropertyChanged(); } }
        }

        public string _noEvent;
        public string NoEvent
        {
            get { return _noEvent; }
            set { if (value != _noEvent) { _noEvent = value; NotifyPropertyChanged(); } }
        }
    }
}
