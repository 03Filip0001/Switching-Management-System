using Mini_Switching_Management_System_Client.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    class DeleteSwitchDialogViewModel : CommonLibrarySE.NotifyPropertyChanged
    {
        public event Action RequestClose = null!;

        public RelayCommand Button_Delete => new RelayCommand(execute => Delete(), canExecute => { return true; });
        public RelayCommand Button_Cancel => new RelayCommand(execute => Cancel(), canExecute => { return true; });


        private int _Delete_Switch_ID;
        public int Delete_Switch_ID
        {
            get { return _Delete_Switch_ID; }
            set
            {
                _Delete_Switch_ID = value;
                OnPropertyChanged();
            }
        }

        public bool Delete_Switch = false;

        private string _Delete_Switch_ID_InputBox;
        public string Delete_Switch_ID_InputBox
        {
            get { return _Delete_Switch_ID_InputBox; }
            set
            {
                _Delete_Switch_ID_InputBox = value;
                OnPropertyChanged();
            }
        }

        public void Delete()
        {
            if (int.TryParse(Delete_Switch_ID_InputBox, out int val))
            {
                Delete_Switch_ID = val;
                Delete_Switch = true;
                RequestClose?.Invoke();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        public void Cancel()
        {
            Delete_Switch = false;
            RequestClose?.Invoke();
        }
    }
}
