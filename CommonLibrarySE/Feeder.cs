using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace CommonLibrarySE
{
    [DataContract(Name = "Feeder", Namespace = "")]
    public class Feeder
    {
        private int _ID;
        [DataMember(Name = "ID")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        [DataMember(Name = "Name")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private ObservableCollection<Switch> _Switches;
        [DataMember(Name = "Switches")]
        public ObservableCollection<Switch> Switches
        {
            get { return _Switches; }
            set { _Switches = value; }
        }
    }
}
