using Mini_Switching_Management_System_Client.Common;
using Mini_Switching_Management_System_Client.Model.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model.DTOs
{
    public class FeederDTO : NotifyPropertyChanged
    {
        public CommonLibrarySE.Feeder Model { get; set; } = new CommonLibrarySE.Feeder();

        public int ID
        {
            get { return Model.ID; }
            set { Model.ID = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SwitchDTO> _Switches;
        public ObservableCollection<SwitchDTO> Switches
        {
            get => _Switches;
            set
            {
                _Switches = value;
                // Update Model.Switches from value:
                Model.Switches = new ObservableCollection<CommonLibrarySE.Switch>(
                    value.Select(b => b.Model));
                OnPropertyChanged(nameof(Switches));
                // Re-subscribe to collection changed:
                _Switches.CollectionChanged += Switches_CollectionChanged;
            }
        }

        private void Switches_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (SwitchDTO b in e.NewItems)
                    Model.Switches.Add(b.Model);
            if (e.OldItems != null)
                foreach (SwitchDTO b in e.OldItems)
                    Model.Switches.Remove(b.Model);
        }
    }
}
