using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CommonLibrarySE
{
    public class Feeder
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

        private ObservableCollection<Switch> _Switches;
        public ObservableCollection<Switch> Switches
        {
            get { return _Switches; }
            set { _Switches = value; }
        }
    }
}
