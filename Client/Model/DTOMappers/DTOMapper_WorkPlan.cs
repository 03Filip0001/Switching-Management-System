using Mini_Switching_Management_System_Client.Model.Binding;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class WorkPlan
        {
            public static ObservableCollection<WorkPlanDTO> ToClientWorkPlanCollection(ObservableCollection<ServerReference.WorkPlan> wp)
            {
                ObservableCollection<WorkPlanDTO> wpClient = new ObservableCollection<WorkPlanDTO>();

                foreach (var workPlan in wp)
                {
                    ObservableCollection<InstructionDTO> instructions = new ObservableCollection<InstructionDTO>();
                    foreach (var instr in workPlan.Instructions) {
                        ObservableCollection<SwitchDTO> switches = new ObservableCollection<SwitchDTO>();
                        foreach(var sw in instr.Switches)
                        {
                            switches.Add(new SwitchDTO { ID=sw.Switch_ID, State = sw.State });
                        }
                        instructions.Add(new InstructionDTO { Number = instr.Number, Switches=switches});
                    }
                    wpClient.Add(new WorkPlanDTO
                    {
                        ID = workPlan.ID,
                        Name = workPlan.Name,
                        State = (CommonLibrarySE.WorkPlanStates)workPlan.State,
                        OperatorName = workPlan.OperatorName,
                        OperatorSurname = workPlan.OperatorSurname,
                        StartDate = workPlan.StartDate,
                        EndDate = workPlan.EndDate,
                        Instructions = instructions
                    });
                }

                return wpClient;
            }
        }
    }
}
