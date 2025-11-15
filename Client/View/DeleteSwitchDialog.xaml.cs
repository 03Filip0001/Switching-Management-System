using Mini_Switching_Management_System_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mini_Switching_Management_System_Client.View
{
    /// <summary>
    /// Interaction logic for DeleteSwitchDialog.xaml
    /// </summary>
    public partial class DeleteSwitchDialog : Window
    {
        public DeleteSwitchDialog()
        {
            InitializeComponent();
            DeleteSwitchDialogViewModel vm = new DeleteSwitchDialogViewModel();
            vm.RequestClose += () => this.Close();
            DataContext = vm;
        }
    }
}
