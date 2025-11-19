using Mini_Switching_Management_System_Client.Model.DTOMappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model.Binding
{
    public class WorkPlanBinding : CommonLibrarySE.NotifyPropertyChanged
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

        public ObservableCollection<InstructionBinding> Instructions
        {
            get
            {
                var collection = new ObservableCollection<InstructionBinding>();
                foreach (var instr in Model.Instructions)
                {
                    var sw_list = new ObservableCollection<SwitchBinding>();
                    foreach(var sw in instr.Switches)
                    {
                        sw_list.Add(DTOMapper.Switch.ToSwitchBinding(sw));
                    }
                    collection.Add(new InstructionBinding { Number = instr.Number, Switches = sw_list });
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
