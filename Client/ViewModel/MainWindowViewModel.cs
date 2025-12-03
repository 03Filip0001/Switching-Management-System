using Mini_Switching_Management_System_Client.Model.Binding;
using Mini_Switching_Management_System_Client.Model.DTOMappers;
using Mini_Switching_Management_System_Client.Common;
using Mini_Switching_Management_System_Client.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class MainWindowViewModel : NotifyPropertyChanged
    {
        public event Action? RefreshGraphRequested;
        public RelayCommand Button_Menu_AddWorkPlan => new RelayCommand(execute => AddWorkPlan(), canExecute => { return true; });
        public RelayCommand Button_Refresh => new RelayCommand(execute => RefreshWorkPlans());

        public RelayCommand Button_EditWorkPlan => new RelayCommand(execute: wp => EditWorkPlan(wp), canExecute: wp => { return true; });

        public RelayCommand Button_CheckWorkPlan => new RelayCommand(execute: wp => CheckWorkPlan(wp), canExecute:wp => { 
            var plan = wp as WorkPlanDTO;
            if (plan == null) return false;
            return plan.State == CommonLibrarySE.WorkPlanStates.Draft;
        });
        public RelayCommand Button_ExecuteWorkPlan => new RelayCommand(execute: wp => ExecuteWorkPlan(wp), canExecute:wp => {
            var plan = wp as WorkPlanDTO;
            if (plan == null) return false;
            return plan.State == CommonLibrarySE.WorkPlanStates.Approved;
        });
        public RelayCommand Button_DeleteWorkPlan => new RelayCommand(execute: wp => DeleteWorkPlan(wp), canExecute:wp => { return true; });

        private ObservableCollection<WorkPlanDTO> _WorkPlans = null!;
        public ObservableCollection<WorkPlanDTO> WorkPlans {
            get { return _WorkPlans; }
            set { _WorkPlans = value; OnPropertyChanged(); }
        }

        private WorkPlanDTO _SelectedWorkPlan = null!;
        public WorkPlanDTO SelectedWorkPlan
        {
            get { return _SelectedWorkPlan; }
            set { _SelectedWorkPlan = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel() { 
            WorkPlans = new ObservableCollection<WorkPlanDTO>();
        }

        private void AddWorkPlan()
        {
            AddWorkPlanWindow mw = new AddWorkPlanWindow();
            mw.ShowDialog();
        }

        private void RefreshWorkPlans()
        {
            RefreshGraphRequested?.Invoke();
            ServerReference.Service1Client serverClient = new ServerReference.Service1Client();
            ObservableCollection<ServerReference.WorkPlan> workPlans = serverClient.GetWorkPlans();

            if (workPlans != null)
            {
                WorkPlans = DTOMapper.WorkPlan.ToClientWorkPlanCollection(workPlans);
            }
        }

        private void CheckWorkPlan(object param)
        {
            var workPlan = param as WorkPlanDTO;
            
            ServerReference.Service1Client client = new ServerReference.Service1Client();
            bool res = client.WorkPlanAction(workPlan.ID, ServerReference.WorkPlanActions.Execute);
            client.Close();

            if (res)
            {
                MessageBox.Show("Checked succesfully");
            }
            else
            {
                MessageBox.Show("Error checking...");
            }

            RefreshWorkPlans();
        }

        private void ExecuteWorkPlan(object param)
        {
            var workPlan = param as WorkPlanDTO;

            ServerReference.Service1Client client = new ServerReference.Service1Client();
            bool res = client.WorkPlanAction(workPlan.ID, ServerReference.WorkPlanActions.Execute);
            client.Close();

            if (res)
            {
                MessageBox.Show("Executed succesfully");
            }
            else
            {
                MessageBox.Show("Error executing...");
            }

            RefreshWorkPlans();
        }

        private void DeleteWorkPlan(object param)
        {
            var workPlan = param as WorkPlanDTO;

            ServerReference.Service1Client client = new ServerReference.Service1Client();
            bool res = client.WorkPlanAction(workPlan.ID, ServerReference.WorkPlanActions.Delete);
            client.Close();

            if (res)
            {
                MessageBox.Show("Deleted succesfully");
            }
            else
            {
                MessageBox.Show("Error deleting...");
            }

            RefreshWorkPlans();
        }

        public void EditWorkPlan(object param)
        {
            var workPlan = param as WorkPlanDTO;
            AddWorkPlanWindow mw = new AddWorkPlanWindow(workPlan);
            mw.ShowDialog();
        }
    }
}
