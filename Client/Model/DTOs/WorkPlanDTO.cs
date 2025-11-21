using Mini_Switching_Management_System_Client.Model.DTOMappers;
using Mini_Switching_Management_System_Client.Common;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.Binding
{
    public class WorkPlanDTO : NotifyPropertyChanged
    {
        public CommonLibrarySE.WorkPlan Model { get; set; } = new CommonLibrarySE.WorkPlan();

        public int ID
        {
            get {  return Model.ID; }
            set { Model.ID = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; OnPropertyChanged(); }
        }

        public CommonLibrarySE.WorkPlanStates State
        {
            get { return Model.State; }
            set { Model.State = value; OnPropertyChanged(); }
        }

        public string OperatorName
        {
            get { return Model.OperatorName; }
            set { Model.OperatorName = value; OnPropertyChanged(); }
        }

        public string OperatorSurname
        {
            get { return Model.OperatorSurname; }
            set { Model.OperatorSurname = value; OnPropertyChanged(); }
        }

        public string StartDate
        {
            get { return Model.StartDate; }
            set { Model.StartDate = value; OnPropertyChanged(); }
        }

        public string EndDate
        {
            get { return Model.EndDate; }
            set { Model.EndDate = value; OnPropertyChanged(); }
        }

        public ObservableCollection<InstructionDTO> Instructions
        {
            get
            {
                var collection = new ObservableCollection<InstructionDTO>();
                foreach (var instr in Model.Instructions)
                {
                    var sw_list = new ObservableCollection<SwitchDTO>();
                    foreach(var sw in instr.Switches)
                    {
                        sw_list.Add(DTOMapper.Switch.ToSwitchBinding(sw));
                    }
                    collection.Add(new InstructionDTO { Number = instr.Number, Switches = sw_list });
                }
                return collection;
            }
            set
            {
                var collection = new ObservableCollection<CommonLibrarySE.Instruction>();
                foreach (var instrBinding in value)
                {
                    collection.Add(instrBinding.Model);
                }
                Model.Instructions = collection;
                OnPropertyChanged();
            }
        }
    }
}
