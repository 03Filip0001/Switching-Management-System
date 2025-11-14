using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WorkPlanClass
{
    public class Switches
    {
        public int Switch_ID { get; set; }
        public bool state {get; set; }
        public Switches() { }
    }

    public class Instruction
    {
        public int Number {get; set; }
        public ObservableCollection<Switches> Switches {get; set;}
        public Instruction() {
            Switches = new ObservableCollection<Switches>();
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
