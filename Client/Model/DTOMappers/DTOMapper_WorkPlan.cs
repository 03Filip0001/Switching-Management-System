using Mini_Switching_Management_System_Client.Model.Binding;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class WorkPlan
        {
            public static ObservableCollection<WorkPlanBinding> ToClientWorkPlanCollection(ObservableCollection<ServerReference.WorkPlan> wp)
            {
                ObservableCollection<WorkPlanBinding> wp_client = new ObservableCollection<WorkPlanBinding>();

                foreach (var workPlan in wp)
                {
                    wp_client.Add(new WorkPlanBinding
                    {
                        ID = workPlan.ID,
                        Name = workPlan.Name,
                        State = (CommonLibrarySE.WorkPlanStates)workPlan.State,
                        OperatorName = workPlan.OperatorName,
                        OperatorSurname = workPlan.OperatorSurname,
                        StartDate = workPlan.StartDate,
                        EndDate = workPlan.EndDate,
                        Instructions = new ObservableCollection<InstructionBinding>
                        {

                        }
                    });
                }

                return wp_client;
            }
        }
    }
}
