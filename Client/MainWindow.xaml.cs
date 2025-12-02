using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Controls.Models;
using Mini_Switching_Management_System_Client.Model;
using Mini_Switching_Management_System_Client.ViewModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mini_Switching_Management_System_Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;

            //ServerReference.Service1Client client = new ServerReference.Service1Client();
            //var res = client.GetSubstations();

            var logic = new SubstationLogicCore();
            tg_Area.LogicCore = logic;
            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
            logic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logic.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 50;
            logic.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 50;
            logic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
            logic.EdgeCurvingEnabled = true;
            logic.AsyncAlgorithmCompute = true;
            tg_Area.SetVerticesDrag(true);

            tg_Area.VertexSelected += tg_Area_VertexSelected;
            tg_Area.GenerateGraphFinished += tg_Area_GenerateGraphFinished;
            tg_Area.RelayoutFinished += tg_Area_RelayoutFinished;

            vm.RefreshGraphRequested += () => RefreshGraph();
        }

        void tg_Area_VertexSelected(object sender, VertexSelectedEventArgs args)
        {
            //if (args.MouseArgs.LeftButton == MouseButtonState.Pressed && tg_edgeMode.SelectedIndex == 1)
            //{
            //    tg_Area.GenerateEdgesForVertex(args.VertexControl, (EdgesType)tg_edgeType.SelectedItem);
            //}
            if (args.MouseArgs.RightButton != MouseButtonState.Pressed) return;
            args.VertexControl.ContextMenu = new ContextMenu();
            var menuitem = new MenuItem { Header = "Delete item", Tag = args.VertexControl };
            //menuitem.Click += tg_deleteitem_Click;
            args.VertexControl.ContextMenu.Items.Add(menuitem);
            args.VertexControl.ContextMenu.IsOpen = true;
        }

        void tg_Area_RelayoutFinished(object sender, EventArgs e)
        {
            //if (tg_Area.LogicCore.AsyncAlgorithmCompute)
            //    tg_loader.Visibility = Visibility.Collapsed;
            //if (tg_Area.MoveAnimation == null)
            //    tg_zoomctrl.ZoomToFill();
        }

        void tg_Area_GenerateGraphFinished(object sender, EventArgs e)
        {
            //if (tg_Area.LogicCore.AsyncAlgorithmCompute)
            //    tg_loader.Visibility = Visibility.Collapsed;

            //tg_highlightType_SelectionChanged(null, null);
            //tg_highlightEnabled_Checked(null, null);
            //tg_highlightEdgeType_SelectionChanged(null, null);
            //tg_dragMoveEdges_Checked(null, null);
            //tg_dragEnabled_Checked(null, null);

            tg_Area.SetEdgesDashStyle(EdgeDashStyle.Dash);
            //tg_zoomctrl.ZoomToFill();// ZoomToFill(); //manually update zoom control to fill the area
        }

        void RefreshGraph()
        {
            ServerReference.Service1Client client = new ServerReference.Service1Client();
            var res = client.GetSubstations();

            var graph = new Graph();
            graph.AddVertex(new SubstationVertex { ID = res[0].ID, Name = res[0].Name });

            tg_Area.GenerateGraph(graph);
        }
    }
}