using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model
{
    public class SubstationEdge :EdgeBase<DataVertex>, INotifyPropertyChanged
    {
        public double Angle { get; set; }
        private string _text;
        public string Text { get { return _text; } set { _text = value; OnPropertyChanged("Text"); } }
        public string ToolTipText { get; set; }

        public SubstationEdge(DataVertex source, DataVertex target, double weight = 1)
                    : base(source, target, weight)
        {
            Angle = 90;
        }

        public SubstationEdge()
            : base(null, null, 1)
        {
            Angle = 90;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
