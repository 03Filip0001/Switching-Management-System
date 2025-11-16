using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CommonLibrarySE
{
    [DataContract(Name = "Switch", Namespace = "")]
    public class Switch : NotifyPropertyChanged
    {
        private int _ID;
        [DataMember(Name = "Switch_ID")]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
                OnPropertyChanged();
            }
        }
        private bool _State;
        [DataMember(Name = "State")]
        public bool State { get { return _State; } set { _State = value; OnPropertyChanged(); } }
        public string StateToString => State ? "Open" : "Closed";
        public Switch() { ID = 0; State = false; }
    }
}
