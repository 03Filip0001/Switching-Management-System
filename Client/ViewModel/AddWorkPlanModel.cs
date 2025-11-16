using Mini_Switching_Management_System_Client.MVVM;
using Mini_Switching_Management_System_Client.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class AddWorkPlanModel : CommonLibrarySE.NotifyPropertyChanged
    {
        public event Action RequestClose = null!;
        public RelayCommand Button_SaveWorkPlan => new RelayCommand(execute => SaveWorkPlan(), canExecute => { return true; });
        public RelayCommand Button_CancelWorkPlan => new RelayCommand(execute => CancelWorkPlan(), canExecute => { return true; });
        public RelayCommand Button_AddInstruction => new RelayCommand(execute => AddInstruction(), canExecute => { return true; });
        public RelayCommand Button_DeleteInstruction => new RelayCommand(execute => DeleteInstruction(), canExecute => { return SelectedInstruction != null; });
        public RelayCommand Button_AddSwitch => new RelayCommand(execute => AddSwitch(), canExecute => { return SelectedInstruction != null; });
        public RelayCommand Button_DeleteSwitch => new RelayCommand(execute => DeleteSwitch(), canExecute => DeleteSwitch_ButtonEnable());

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged();
            }
        }

        private string _WorkPlanName = "";
        public string WorkPlanName
        {
            get { return _WorkPlanName; }
            set
            {
                _WorkPlanName = value;
                OnPropertyChanged();
            }
        }

        private CommonLibrarySE.WorkPlansStates _WorkPlanState = CommonLibrarySE.WorkPlansStates.Draft;
        public CommonLibrarySE.WorkPlansStates WorkPlanState
        {
            get { return _WorkPlanState; }
            set
            {
                _WorkPlanState = value;
                OnPropertyChanged();
            }
        }

        private string _OperatorName = "";
        public string OperatorName
        {
            get { return _OperatorName; }
            set
            {
                _OperatorName = value;
                OnPropertyChanged();
            }
        }

        private string _OperatorSurname = "";
        public string OperatorSurname
        {
            get { return _OperatorSurname; }
            set
            {
                _OperatorSurname = value;
                OnPropertyChanged();
            }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                OnPropertyChanged();
            }
        }

        private CommonLibrarySE.InstructionsList _instructions = null!;
        public CommonLibrarySE.InstructionsList Instructions
        {
            get { return _instructions; }
            set
            {
                _instructions = value;
                OnPropertyChanged();
            }
        }

        private CommonLibrarySE.Instruction _selectedInstruction = null!;
        public CommonLibrarySE.Instruction SelectedInstruction
        {
            get { return _selectedInstruction; }
            set
            {
                _selectedInstruction = value;
                OnPropertyChanged();
            }
        }

        public AddWorkPlanModel()
        {
            ServerReference.Service1Client client = new ServerReference.Service1Client();
            ID = client.GetNewWorkPlanUniqueID();

            Instructions = new CommonLibrarySE.InstructionsList();
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
                State = (ServerReference.WorkPlansStates) this.WorkPlanState,
                OperatorName = this.OperatorName,
                OperatorSurname = this.OperatorSurname,
                StartDate = this.StartDate.ToString("dd/MM/yyyy"),
                EndDate = this.EndDate.ToString("dd/MM/yyyy"),
                //Instructions = this.Instructions
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
            Instructions.Instructions.Add(new CommonLibrarySE.Instruction { Number = Instructions.Instructions.Count + 1 });
        }

        private void DeleteInstruction()
        {
            Instructions.Instructions.Remove(SelectedInstruction);
            for (int i = 0; i < Instructions.Instructions.Count; i++)
            {
                Instructions.Instructions[i].Number = i + 1;
            }
        }

        private void AddSwitch()
        {
            CommonLibrarySE.Switch sw = new CommonLibrarySE.Switch();
            SelectedInstruction.Switches.Add(sw);
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
                CommonLibrarySE.Switch sw = null!;
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
