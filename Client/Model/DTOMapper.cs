
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model
{
    public static class DTOMapper
    {
        public static ServerReference.InstructionsList ToServerInstructionList(CommonLibrarySE.InstructionsList list)
        {
            ServerReference.InstructionsList server_list = new ServerReference.InstructionsList();
            server_list.Instructions = new ObservableCollection<ServerReference.Instruction>();

            foreach (var instr in list.Instructions)
            {
                server_list.Instructions.Add(new ServerReference.Instruction { Number = instr.Number, Switches = new ObservableCollection<ServerReference.Switch>()});
                foreach(var sw in instr.Switches)
                {
                    server_list.Instructions[server_list.Instructions.Count - 1].Switches.Add(new ServerReference.Switch { Switch_ID = sw.ID, State = sw.State }); 
                }
            }

            return server_list;
        }
    }
}
