using Mini_Switching_Management_System_Client.Model.Binding;
using Mini_Switching_Management_System_Client.Model.DTOMappers;
using Mini_Switching_Management_System_Client.MVVM;
using Mini_Switching_Management_System_Client.View;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class MainWindowViewModel : CommonLibrarySE.NotifyPropertyChanged
    {
        public RelayCommand Button_Menu_AddWorkPlan => new RelayCommand(execute => AddWorkPlan(), canExecute => { return true; });
        public RelayCommand Button_Refresh => new RelayCommand(execute => RefreshWorkPlans());

        private ObservableCollection<WorkPlanBinding> _WorkPlans = null!;
        public ObservableCollection<WorkPlanBinding> WorkPlans {
            get { return _WorkPlans; }
            set { _WorkPlans = value; OnPropertyChanged(); }
        }

        private WorkPlanBinding _SelectedWorkPlan = null!;
        public WorkPlanBinding SelectedWorkPlan
        {
            get { return _SelectedWorkPlan; }
            set { _SelectedWorkPlan = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel() { 
            WorkPlans = new ObservableCollection<WorkPlanBinding>();
        }

        private void AddWorkPlan()
        {
            AddWorkPlanWindow mw = new AddWorkPlanWindow();
            mw.ShowDialog();
        }

        private void RefreshWorkPlans()
        {
            ServerReference.Service1Client serverClient = new ServerReference.Service1Client();
            ObservableCollection<ServerReference.WorkPlan> workPlans = serverClient.GetWorkPlans();

            if (workPlans != null)
            {
                WorkPlans = DTOMapper.WorkPlan.ToClientWorkPlanCollection(workPlans);
            }
        }
    }
}
