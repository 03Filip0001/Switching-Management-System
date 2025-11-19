using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CommonLibrarySE
{
    [DataContract(Name = "Switch", Namespace = "")]
    public class Switch
    {
        private int _ID;
        [DataMember(Name = "Switch_ID")]
        public virtual int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        private bool _State;
        [DataMember(Name = "State")]
        public virtual bool State { get { return _State; } set { _State = value; } }
        public virtual string StateToString => State ? "Open" : "Closed";
        public Switch() { ID = 0; State = false; }
    }
}
