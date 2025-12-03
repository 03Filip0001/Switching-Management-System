using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Controls.Models;
using Mini_Switching_Management_System_Client.Model;
using Mini_Switching_Management_System_Client.Model.DTOMappers;
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

            var logic = new DataLogicCore();
            tg_Area.LogicCore = logic;
            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Sugiyama;
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

            List<CommonLibrarySE.Substation> sub = DTOMapper.Substation.ToSubstation(res);

            var graph = new Graph();
            

            foreach (var subitem in sub)
            {
                var subVertex = new DataVertex { ID = subitem.ID, Name = subitem.Name, VertexType = VertexTypes.SUBSTATION };
                graph.AddVertex(subVertex);
                foreach (var feeder in subitem.Feeders)
                {
                    var feedVertex = new DataVertex { ID = feeder.ID, Name = feeder.Name, VertexType = VertexTypes.FEEDER };
                    graph.AddVertex(feedVertex);
                    graph.AddEdge(new DataEdge(subVertex, feedVertex, 10) { Text = "", ToolTipText=""});
                    foreach (var sw in feeder.Switches)
                    {
                        var switchVertex = new DataVertex { ID = sw.ID, State = sw.State, VertexType = VertexTypes.SWITCH };
                        graph.AddVertex(switchVertex);
                        graph.AddEdge(new DataEdge(feedVertex, switchVertex, 10) { Text = "", ToolTipText=""});
                    }
                }
            }

            tg_Area.ShowAllEdgesLabels(false);
            tg_Area.EdgeLabelFactory = null;
            tg_Area.GenerateGraph(graph);

            foreach (var kvp in tg_Area.VertexList)
            {
                var vertex = kvp.Key;          // DataVertex
                var vc = kvp.Value;        // VertexControl

                switch (vertex.VertexType)
                {
                    case VertexTypes.SUBSTATION:
                        vc.Style = (Style)FindResource("SubstationVertexStyle");
                        break;

                    case VertexTypes.FEEDER:
                        vc.Style = (Style)FindResource("FeederVertexStyle");
                        break;

                    case VertexTypes.SWITCH:
                        vc.Style = (Style)FindResource("SwitchVertexStyle");
                        break;
                }
            }
        }
    }
}