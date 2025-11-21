using Mini_Switching_Management_System_Client.Model.Binding;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class Instruction
        {
            public static ObservableCollection<ServerReference.Instruction> ToServerInstructionCollection(ObservableCollection<InstructionDTO> list)
            {
                ObservableCollection<ServerReference.Instruction> serveList = new ObservableCollection<ServerReference.Instruction>();

                foreach (var instr in list)
                {
                    serveList.Add(new ServerReference.Instruction { Number = instr.Number, Switches = new ObservableCollection<ServerReference.Switch>() });
                    foreach (var sw in instr.Switches)
                    {
                        serveList[serveList.Count - 1].Switches.Add(new ServerReference.Switch { Switch_ID = sw.ID, State = sw.State });
                    }
                }

                return serveList;
            }
        }
    }
}
