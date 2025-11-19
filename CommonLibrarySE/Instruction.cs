using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace CommonLibrarySE
{
    [DataContract(Name = "Instruction", Namespace = "")]
    public class Instruction 
    {
        private int _Number;
        [DataMember(Name = "Number")]
        public virtual int Number { get { return _Number; } set { _Number = value; } }

        private ObservableCollection<Switch> _Switches;
        [DataMember(Name = "Switches")]
        public virtual ObservableCollection<Switch> Switches { get { return _Switches; } set { _Switches = value; } }
        public Instruction() {
            Switches = new ObservableCollection<Switch>();
            Number = 0;
        }
        public Instruction(int number)
        {
            Switches = new ObservableCollection<Switch>();
            Number = number;
        }
    }
}
