using Mini_Switching_Management_System_Client.Model.Binding;
using Mini_Switching_Management_System_Client.Model.DTOs;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class Feeder
        {
            public static ObservableCollection<ServerReference.Feeder> ToServerFeederCollection(ObservableCollection<FeederDTO> list)
            {
                ObservableCollection<ServerReference.Feeder> serveList = new ObservableCollection<ServerReference.Feeder>();

                foreach (var instr in list)
                {
                    serveList.Add(new ServerReference.Feeder { ID = instr.ID, Name = instr.Name, Switches = new ObservableCollection<ServerReference.Switch>() });
                    foreach (var sw in instr.Switches)
                    {
                        serveList[serveList.Count - 1].Switches.Add(new ServerReference.Switch { Switch_ID = sw.ID, State = sw.State });
                    }
                }

                return serveList;
            }

            public static ObservableCollection<FeederDTO> ToClientFeederCollection(ObservableCollection<ServerReference.Feeder> list)
            {
                ObservableCollection<FeederDTO> clientList = new ObservableCollection<FeederDTO>();

                foreach (var instr in list)
                {
                    clientList.Add(new FeederDTO { ID = instr.ID, Name = instr.Name, Switches = new ObservableCollection<SwitchDTO>() });
                    foreach (var sw in instr.Switches)
                    {
                        clientList[clientList.Count - 1].Switches.Add(new SwitchDTO { ID = sw.Switch_ID, State = sw.State });
                    }
                }

                return clientList;
            }

            public static FeederDTO ToFeederBinding(CommonLibrarySE.Feeder feeder)
            {
                FeederDTO dto =  new FeederDTO { ID = feeder.ID, Name = feeder.Name, Switches = new ObservableCollection<SwitchDTO>() };

                foreach (var sw in feeder.Switches)
                {
                    dto.Switches.Add(new SwitchDTO { ID = sw.ID, State = sw.State });
                }

                return dto;
            }
        }
    }
}
