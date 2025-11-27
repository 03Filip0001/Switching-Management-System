using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CommonLibrarySE
{
    class Substation
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private ObservableCollection<Feeder> _Feeders;
        public ObservableCollection<Feeder> Feeders
        {
            get { return _Feeders; }
            set { _Feeders = value; }
        }
    }
}
