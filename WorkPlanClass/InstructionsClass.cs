using System;
using System.Collections.Generic;
using System.Text;

namespace WorkPlanClass
{
    internal class Switches
    {
        public int Switch_ID;
        public bool state;
        public Switches() { }
    }

    internal class Instruction
    {
        public int Number;
        List<Switches> Switches;
        public Instruction() {
            Switches = new List<Switches>();
        }
    }
    internal class InstructionsClass
    {
        List<Instruction> Instructions;
        public InstructionsClass() {
            Instructions = new List<Instruction>();
        }
    }
}
