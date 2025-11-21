using CommonLibrarySE;
using Mini_Switching_Management_System_Client.Model.Binding;
using Mini_Switching_Management_System_Client.Model.DTOMappers;
using Mini_Switching_Management_System_Client.Common;
using Mini_Switching_Management_System_Client.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class AddWorkPlanViewModel : NotifyPropertyChanged
    {
        public event Action RequestClose = null!;
        public RelayCommand Button_SaveWorkPlan => new RelayCommand(execute => SaveWorkPlan(), canExecute => { return true; });
        public RelayCommand Button_CancelWorkPlan => new RelayCommand(execute => CancelWorkPlan(), canExecute => { return true; });
        public RelayCommand Button_AddInstruction => new RelayCommand(execute => AddInstruction(), canExecute => { return true; });
        public RelayCommand Button_DeleteInstruction => new RelayCommand(execute => DeleteInstruction(), canExecute => { return SelectedInstruction != null; });
        public RelayCommand Button_AddSwitch => new RelayCommand(execute => AddSwitch(), canExecute => { return SelectedInstruction != null; });
        public RelayCommand Button_DeleteSwitch => new RelayCommand(execute => DeleteSwitch(), canExecute => DeleteSwitch_ButtonEnable());

        private int _ID = 0;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _WorkPlanName = "";
        public string WorkPlanName
        {
            get { return _WorkPlanName; }
            set { _WorkPlanName = value; }
        }

        private CommonLibrarySE.WorkPlanStates _WorkPlanState = CommonLibrarySE.WorkPlanStates.Draft;
        public CommonLibrarySE.WorkPlanStates WorkPlanState
        {
            get { return _WorkPlanState; }
            set { _WorkPlanState = value; }
        }

        private string _OperatorName = "";
        public string OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }

        private string _OperatorSurname = "";
        public string OperatorSurname
        {
            get { return _OperatorSurname; }
            set { _OperatorSurname = value; }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private ObservableCollection<InstructionDTO> _instructions = null!;
        public ObservableCollection<InstructionDTO> Instructions
        {
            get { return _instructions; }
            set { _instructions = value; OnPropertyChanged(); }
        }

        private InstructionDTO _selectedInstruction = null!;
        public InstructionDTO SelectedInstruction
        {
            get { return _selectedInstruction; }
            set { _selectedInstruction = value; }
        }

        public AddWorkPlanViewModel()
        {
            ServerReference.Service1Client client = new ServerReference.Service1Client();
            ID = client.GetNewWorkPlanUniqueID();

            Instructions = new ObservableCollection<InstructionDTO>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            WorkPlanName = string.Empty;
            OperatorName = string.Empty;
            OperatorSurname = string.Empty;
        }

        private void SaveWorkPlan()
        {
            ServerReference.WorkPlan wp = new ServerReference.WorkPlan()
            {
                ID = this.ID,
                Name = this.WorkPlanName,
                State = (ServerReference.WorkPlanStates) this.WorkPlanState,
                OperatorName = this.OperatorName,
                OperatorSurname = this.OperatorSurname,
                StartDate = this.StartDate.ToString("dd/MM/yyyy"),
                EndDate = this.EndDate.ToString("dd/MM/yyyy"),
                Instructions = DTOMapper.Instruction.ToServerInstructionCollection(Instructions)
            };

            try
            {
                ServerReference.Service1Client client = new ServerReference.Service1Client();
                bool status = client.SaveWorkPlan(wp);

                if (!status) throw new Exception();

                MessageBox.Show("Saved succesfully");
            }
            catch
            {
                MessageBox.Show("Error saving...");
            }
        }

        private void CancelWorkPlan()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit without saving ?", "Question", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                RequestClose?.Invoke();
            }
            else if (result == MessageBoxResult.No)
            {
                result = MessageBox.Show("Do you want to save work plan ?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    SaveWorkPlan();
                }
            }
        }

        private void AddInstruction()
        {
            Instructions.Add(new InstructionDTO { Number = Instructions.Count + 1});
        }

        private void DeleteInstruction()
        {
            Instructions.RemoveAt(SelectedInstruction.Number - 1);
            for (int i = 0; i < Instructions.Count; i++)
            {
                Instructions[i].Number = i + 1;
            }
        }

        private void AddSwitch()
        {
            Instructions[SelectedInstruction.Number - 1].Switches.Add(new SwitchDTO());
        }

        private void DeleteSwitch() 
        {
            var dialog = new DeleteSwitchDialog();
            var result = dialog.ShowDialog();


            var vm = (DeleteSwitchDialogViewModel) dialog.DataContext;
            bool delete = vm.Delete_Switch;
            int delete_id = vm.Delete_Switch_ID;

            if (delete)
            {
                SwitchDTO sw = null!;
                for (int i = 0; i < SelectedInstruction.Switches.Count; i++)
                {
                    if (SelectedInstruction.Switches[i].ID == delete_id)
                    {
                        sw = SelectedInstruction.Switches[i];
                        break;
                    }
                }

                if (sw != null) SelectedInstruction.Switches.Remove(sw);
            }
        }

        private bool DeleteSwitch_ButtonEnable()
        {
            if(SelectedInstruction == null ) return false;
            if(SelectedInstruction.Switches.Count == 0 ) return false;

            return true;
        }
    }
}
