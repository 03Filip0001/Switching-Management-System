using Mini_Switching_Management_System_Client.ViewModel;
using System.Globalization;
using System.Windows;

namespace Mini_Switching_Management_System_Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
        }
    }
}