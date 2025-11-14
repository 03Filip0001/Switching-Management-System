using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WorkPlanClass
{
    public class Switch
    {
        public int Switch_ID { get; set; }
        public bool State {get; set; }
        public string StateToString => State ? "Open" : "Closed";
        public Switch() { }
    }

    public class Instruction
    {
        public int Number {get; set; }
        public ObservableCollection<Switch> Switches {get; set;}
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
    public class InstructionsClass
    {
        public ObservableCollection<Instruction> Instructions { get; set; }
        public InstructionsClass() {
            Instructions = new ObservableCollection<Instruction>();
        }
    }
}
