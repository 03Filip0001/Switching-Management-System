using Mini_Switching_Management_System_Client.Model.Binding;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class Instruction
        {
            public static ObservableCollection<ServerReference.Instruction> ToServerInstructionCollection(ObservableCollection<InstructionBinding> list)
            {
                ObservableCollection<ServerReference.Instruction> server_list = new ObservableCollection<ServerReference.Instruction>();

                foreach (var instr in list)
                {
                    server_list.Add(new ServerReference.Instruction { Number = instr.Number, Switches = new ObservableCollection<ServerReference.Switch>() });
                    foreach (var sw in instr.Switches)
                    {
                        server_list[server_list.Count - 1].Switches.Add(new ServerReference.Switch { Switch_ID = sw.ID, State = sw.State });
                    }
                }

                return server_list;
            }
        }
    }
}
