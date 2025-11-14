using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WorkPlanClass
{
    public class Switch : NotifyPropertyChanged
    {
        private int _Switch_ID;
        public int Switch_ID {
            get
            {
                return _Switch_ID;
            }
            set
            {
                _Switch_ID = value;
                OnPropertyChanged();
            }
        }
        private bool _State;
        public bool State { get { return _State; } set { _State = value; OnPropertyChanged(); } }
        public string StateToString => State ? "Open" : "Closed";
        public Switch() { }
    }

    public class Instruction : NotifyPropertyChanged 
    {
        private int _Number;
        public int Number { get { return _Number; } set { _Number = value; OnPropertyChanged(); } }

        private ObservableCollection<Switch> _Switches;
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
    public class InstructionsClass : NotifyPropertyChanged
    {
        private ObservableCollection<Instruction> _Instructions;
        public ObservableCollection<Instruction> Instructions { get { return _Instructions; } set { _Instructions = value; OnPropertyChanged(); } }
        public InstructionsClass() {
            Instructions = new ObservableCollection<Instruction>();
        }
    }
}
