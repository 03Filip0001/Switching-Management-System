using Mini_Switching_Management_System_Client.Model;
using Mini_Switching_Management_System_Client.MVVM;
using Mini_Switching_Management_System_Client.View;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class MainWindowViewModel : CommonLibrarySE.NotifyPropertyChanged
    {
        private ObservableCollection<ServerReference.WorkPlan> _WorkPlans;
        public ObservableCollection<ServerReference.WorkPlan> WorkPlans {
            get { return _WorkPlans; }
            set {
                _WorkPlans = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand Menu_AddWorkPlan => new RelayCommand(execute => AddWorkPlan(), canExecute => { return true; });
        public RelayCommand Refresh_WorkPlans => new RelayCommand(execute => RefreshWorkPlans());
        public MainWindowViewModel() { 
            WorkPlans = new ObservableCollection<ServerReference.WorkPlan>();
        }

        private ServerReference.WorkPlan selectedWorkPlan;
        public ServerReference.WorkPlan SelectedWorkPlan
        {
            get { return selectedWorkPlan; }
            set { 
                selectedWorkPlan = value;
                OnPropertyChanged();
            }
        }

        private void AddWorkPlan()
        {
            AddWorkPlanWindow mw = new AddWorkPlanWindow();
            mw.ShowDialog();
        }

        private void RefreshWorkPlans()
        {
            ServerReference.Service1Client serverClient = new ServerReference.Service1Client();
            ServerReference.WorkPlansList workPlans = serverClient.GetWorkPlans();

            if (workPlans != null)
            {
                WorkPlans = new ObservableCollection<ServerReference.WorkPlan>(workPlans.WorkPlans);
            }
        }
    }
}
