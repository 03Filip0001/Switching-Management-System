using Microsoft.Win32;
using Mini_Switching_Management_System_Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Mini_Switching_Management_System_Client.ViewModel
{
    class EditSwitchDialogViewModel : NotifyPropertyChanged
    {
        public event Action<bool?> RequestClose = null!;

        public RelayCommand Button_Save => new RelayCommand(execute => Save(), canExecute => { return true; });

        private bool _SwitchIsClosed;
        public bool SwitchIsClosed
        {
            get { return _SwitchIsClosed; }
            set { if (_SwitchIsClosed != value) { _SwitchIsClosed = value; OnPropertyChanged(); OnPropertyChanged(nameof(SwitchIsOpen)); } }
        }

        public bool SwitchIsOpen
        {
            get { return !_SwitchIsClosed; }
            set { if (_SwitchIsClosed == value) { _SwitchIsClosed = !value; OnPropertyChanged(); OnPropertyChanged(nameof(SwitchIsClosed)); } }
        }

        private int _PreviousID;
        public int PreviousID
        {
            get { return _PreviousID; }
            set { _PreviousID = value; OnPropertyChanged(); }
        }

        private bool _PreviousState;
        public bool PreviousState
        {
            get { return _PreviousState; }
            set { _PreviousState = value; OnPropertyChanged(); }
        }

        private int _NewID;
        public int NewID
        {
            get { return _NewID; }
            set { _NewID = value; OnPropertyChanged(); }
        }

        private void Save()
        {
            this.RequestClose?.Invoke(true);
        }
    }
}
