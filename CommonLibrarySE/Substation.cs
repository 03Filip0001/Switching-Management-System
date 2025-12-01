using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace CommonLibrarySE
{
    [DataContract(Name ="Substation", Namespace ="")]
    public class Substation
    {
        private int _ID;
        [DataMember(Name="ID")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        [DataMember(Name="Name")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private ObservableCollection<Feeder> _Feeders;
        [DataMember(Name="Feeders")]
        public ObservableCollection<Feeder> Feeders
        {
            get { return _Feeders; }
            set { _Feeders = value; }
        }
    }
}
