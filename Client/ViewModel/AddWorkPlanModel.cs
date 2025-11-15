using Mini_Switching_Management_System_Client.MVVM;
using Mini_Switching_Management_System_Client.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class AddWorkPlanModel : NotifyPropertyChanged
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

        private Server.WorkPlansStates _WorkPlanState = Server.WorkPlansStates.Draft;
        public Server.WorkPlansStates WorkPlanState
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

        private WorkPlanClass.InstructionsClass _instructions = null!;
        public WorkPlanClass.InstructionsClass Instructions
        {
            get { return _instructions; }
            set
            {
                _instructions = value;
                OnPropertyChanged();
            }
        }

        private WorkPlanClass.Instruction _selectedInstruction = null!;
        public WorkPlanClass.Instruction SelectedInstruction
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
            Server.Service1Client client = new Server.Service1Client();
            ID = client.GetNewWorkPlanUniqueID();

            Instructions = new WorkPlanClass.InstructionsClass();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            WorkPlanName = string.Empty;
            OperatorName = string.Empty;
            OperatorSurname = string.Empty;
        }

        private void SaveWorkPlan()
        {
            Server.WorkPlan wp = new Server.WorkPlan()
            {
                ID = this.ID,
                WorkPlanName = this.WorkPlanName,
                WorkPlanState = this.WorkPlanState,
                OperatorName = this.OperatorName,
                OperatorSurname = this.OperatorSurname,
                StartDate = this.StartDate.ToString("dd/MM/yyyy"),
                EndDate = this.EndDate.ToString("dd/MM/yyyy")
            };

            try
            {
                Server.Service1Client client = new Server.Service1Client();
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
            Instructions.Instructions.Add(new WorkPlanClass.Instruction(Instructions.Instructions.Count + 1));
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
            WorkPlanClass.Switch sw = new WorkPlanClass.Switch();
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
                WorkPlanClass.Switch sw = null;
                for (int i = 0; i < SelectedInstruction.Switches.Count; i++)
                {
                    if (SelectedInstruction.Switches[i].Switch_ID == delete_id)
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
