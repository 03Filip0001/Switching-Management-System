using Mini_Switching_Management_System_Client.Model.DTOMappers;
using Mini_Switching_Management_System_Client.Common;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.Binding
{
    public class InstructionDTO : NotifyPropertyChanged
    {
        public CommonLibrarySE.Instruction Model {  get; set; } = new CommonLibrarySE.Instruction();

        public int Number
        {
            get { return Model.Number; }
            set { Model.Number = value; OnPropertyChanged(); }
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

        public InstructionDTO(CommonLibrarySE.Instruction model = null!) {
            Model = model ?? new CommonLibrarySE.Instruction();
            _Switches = new ObservableCollection<SwitchDTO>(Model.Switches.Select(DTOMapper.Switch.ToSwitchBinding));
            _Switches.CollectionChanged += Switches_CollectionChanged;
        }
    }
}
