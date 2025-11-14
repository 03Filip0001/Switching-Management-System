using Mini_Switching_Management_System_Client.Model;
using Mini_Switching_Management_System_Client.MVVM;
using Mini_Switching_Management_System_Client.View;
using Server;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class MainWindowViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<Server.WorkPlan> _WorkPlans;
        public ObservableCollection<Server.WorkPlan> WorkPlans {
            get { return _WorkPlans; }
            set {
                _WorkPlans = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand Menu_AddWorkPlan => new RelayCommand(execute => AddWorkPlan(), canExecute => { return true; });
        public RelayCommand Refresh_WorkPlans => new RelayCommand(execute => RefreshWorkPlans());
        public MainWindowViewModel() { 
            WorkPlans = new ObservableCollection<Server.WorkPlan>();
        }

        private Server.WorkPlan selectedWorkPlan;
        public  Server.WorkPlan SelectedWorkPlan
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
            Server.Service1Client serverClient = new Server.Service1Client();
            Server.WorkPlansCollection workPlans = serverClient.GetWorkPlans();

            WorkPlans = new ObservableCollection<Server.WorkPlan>(workPlans.WorkPlans);
            Debug.WriteLine(WorkPlans);
            Debug.WriteLine(WorkPlans.Count);
        }
    }
}
