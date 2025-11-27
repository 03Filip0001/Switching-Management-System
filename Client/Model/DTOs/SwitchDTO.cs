using Mini_Switching_Management_System_Client.Common;

namespace Mini_Switching_Management_System_Client.Model.Binding
{
    public class SwitchDTO : NotifyPropertyChanged
    {
        public CommonLibrarySE.Switch Model { get; set; } = new CommonLibrarySE.Switch();

        public int ID
        {
            get { return Model.ID; } 
            set { Model.ID = value; OnPropertyChanged(); }
        }

        public bool State
        {
            get { return Model.State; }
            set { Model.State = value; OnPropertyChanged(); OnPropertyChanged(nameof(StateToString)); }
        }

        public string StateToString { get { return Model.StateToString; } }
    }
}
