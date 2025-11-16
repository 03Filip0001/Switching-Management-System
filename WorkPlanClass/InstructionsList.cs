using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace CommonLibrarySE
{
    [DataContract(Name = "Instruction", Namespace = "")]
    public class Instruction : NotifyPropertyChanged 
    {
        private int _Number;
        [DataMember(Name = "Number")]
        public int Number { get { return _Number; } set { _Number = value; OnPropertyChanged(); } }

        private ObservableCollection<Switch> _Switches;
        [DataMember(Name = "Switches")]
        public ObservableCollection<Switch> Switches { get { return _Switches; } set { _Switches = value; OnPropertyChanged(); } }
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

    [DataContract(Name = "InstructionsList", Namespace = "")]
    public class InstructionsList : NotifyPropertyChanged
    {
        private ObservableCollection<Instruction> _Instructions;
        [DataMember(Name = "Instructions")]
        public ObservableCollection<Instruction> Instructions { get { return _Instructions; } set { _Instructions = value; OnPropertyChanged(); } }
        public InstructionsList() {
            this.Instructions = new ObservableCollection<Instruction>();
        }
    }
}
