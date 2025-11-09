using Mini_Switching_Management_System_Client.Model;
using Mini_Switching_Management_System_Client.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

using Mini_Switching_Management_System_Client.View;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<WorkPlan> WorkPlans { get; set; }
        public RelayCommand Menu_AddWorkPlan => new RelayCommand(execute => AddWorkPlan(), canExecute => { return true; });
        public MainWindowViewModel() { 
            WorkPlans = new ObservableCollection<WorkPlan>();
        }

        private WorkPlan selectedWorkPlan;
        public  WorkPlan SelectedWorkPlan
        {
            get { return selectedWorkPlan; }
            set { 
                selectedWorkPlan = value;
                OnPropertyChanged();
            }
        }

        private void AddWorkPlan()
        {
            //MessageBox.Show("Ovo je poruka");
            //WorkPlans.Add(new WorkPlan
            //{
            //    ID=1,
            //    WorkPlanName="Test ime",
            //    WorkPlanState=WorkPlansStates.Draft,
            //    OperatorName="Filip",
            //    OperatorSurname="Test",
            //    StartDate="1.1.2025",
            //    EndDate="2.1.2025"
            //});
            AddWorkPlanWindow mw = new AddWorkPlanWindow();
            mw.ShowDialog();
        }
    }
}
