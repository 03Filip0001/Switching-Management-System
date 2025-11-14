using System;
using System.Collections.Generic;
using System.Text;

namespace WorkPlanClass
{
    public class Switches
    {
        public int Switch_ID;
        public bool state;
        public Switches() { }
    }

    public class Instruction
    {
        public int Number;
        List<Switches> Switches;
        public Instruction() {
            Switches = new List<Switches>();
        }
    }
    public class InstructionsClass
    {
        public List<Instruction> Instructions;
        public InstructionsClass() {
            Instructions = new List<Instruction>();
        }
    }
}
