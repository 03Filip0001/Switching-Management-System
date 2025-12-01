using Mini_Switching_Management_System_Client.Common;
using Mini_Switching_Management_System_Client.Model.Binding;
using Mini_Switching_Management_System_Client.Model.DTOMappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model.DTOs
{
    class SubstationDTO : NotifyPropertyChanged
    {
        public CommonLibrarySE.Substation Model { get; set; } = new CommonLibrarySE.Substation();

        public int ID
        {
            get {  return Model.ID; }
            set { Model.ID = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FeederDTO> _Feeders;
        public ObservableCollection<FeederDTO> Feeders
        {
            get { return _Feeders; }
            set 
            { 
                _Feeders = value;
                // Update Model.Switches from value:
                Model.Feeders = new ObservableCollection<CommonLibrarySE.Feeder>(
                    value.Select(b => b.Model));
                OnPropertyChanged(nameof(Feeders));
                // Re-subscribe to collection changed:
                _Feeders.CollectionChanged += Feeder_CollectionChanged;
            }
        }

        private void Feeder_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (FeederDTO b in e.NewItems)
                    Model.Feeders.Add(b.Model);
            if (e.OldItems != null)
                foreach (FeederDTO b in e.OldItems)
                    Model.Feeders.Remove(b.Model);
        }

        public SubstationDTO(CommonLibrarySE.Substation model = null!)
        {
            Model = model ?? new CommonLibrarySE.Substation();
            _Feeders = new ObservableCollection<FeederDTO>(Model.Feeders.Select(DTOMapper.Feeder.ToFeederBinding));
            _Feeders.CollectionChanged += Feeder_CollectionChanged;
        }
    }
}
