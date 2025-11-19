using Mini_Switching_Management_System_Client.Model.DTOMappers;
using System.Collections.ObjectModel;

namespace Mini_Switching_Management_System_Client.Model.Binding
{
    public class InstructionBinding : CommonLibrarySE.NotifyPropertyChanged
    {
        public CommonLibrarySE.Instruction Model {  get; set; } = new CommonLibrarySE.Instruction();

        public int Number
        {
            get { return Model.Number; }
            set { Model.Number = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SwitchBinding> _Switches;
        public ObservableCollection<SwitchBinding> Switches
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

        private void Switches_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (SwitchBinding b in e.NewItems)
                    Model.Switches.Add(b.Model);
            if (e.OldItems != null)
                foreach (SwitchBinding b in e.OldItems)
                    Model.Switches.Remove(b.Model);
        }

        public InstructionBinding(CommonLibrarySE.Instruction model = null) {
            Model = model ?? new CommonLibrarySE.Instruction();
            _Switches = new ObservableCollection<SwitchBinding>(Model.Switches.Select(DTOMapper.Switch.ToSwitchBinding));
            _Switches.CollectionChanged += Switches_CollectionChanged;
        }
    }
}
