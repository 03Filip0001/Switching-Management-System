using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.Service1Client client = new Server.Service1Client();
            ObservableCollection<Server.Instruction> instr = new ObservableCollection<Server.Instruction>
            {
                new Server.Instruction
                {
                    Number = 1,
                    Switches = new ObservableCollection<Server.Switch>()
                },
                new Server.Instruction
                {
                    Number = 2,
                    Switches = new ObservableCollection<Server.Switch>()
                }
            };

            Server.WorkPlan plan = new Server.WorkPlan
            {
                ID = 1,
                StartDate = "123",
                EndDate = "321",
                Name = "se",
                OperatorName = "filip",
                OperatorSurname = "gold",
                Instructions = instr,
                State = Server.WorkPlanStates.Draft,
            };

            if (client.SaveWorkPlan(plan))
            {
                Console.WriteLine("saved");
            }
            else
            {
                Console.WriteLine("NOT SAVED");
            }
        }
    }
}
