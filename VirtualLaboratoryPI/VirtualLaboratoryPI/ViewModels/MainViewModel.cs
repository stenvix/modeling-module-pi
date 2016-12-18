using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GraphX.Controls;
using GraphX.Controls.Models;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Common.Models;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VirtualLaboratoryPI.Graph.Area;
using VirtualLaboratoryPI.Graph.Data;
using VirtualLaboratoryPI.Graph.Data.Controls;
using VirtualLaboratoryPI.Graph.Data.Enums;
using VirtualLaboratoryPI.Graph.Data.Vertex;
using VirtualLaboratoryPI.Graph.Logic;

namespace VirtualLaboratoryPI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private EditorObjectManager _editorManager;

        private EditorOperationMode _opMode = EditorOperationMode.Select;

        private VertexControl _ecFrom;

        private GraphExample Graph = new GraphExample();
        private Logic LogicCore { get; set; }
        private GraphAreaExample Area { get; set; }
        private ZoomControl ZoomCtrl { get; set; }
        public string BlockText { get; set; }

        public RelayCommand AddPointCommand
        {
            get
            {
                return new RelayCommand(AddPoint);
            }
        }


        internal void ButtonChanged(string name)
        {
            switch (name)
            {
                case "moveButton":
                    {
                        _opMode = EditorOperationMode.Move;
                        break;
                    }
                case "selectButton":
                    {
                        _opMode = EditorOperationMode.Select;
                        break;
                    }
                case "deleteButton":
                    {
                        _opMode = EditorOperationMode.Delete;
                        break;
                    }
                case "blockButton":
                    {
                        _opMode = EditorOperationMode.Block;
                        break;
                    }
                case "pointButton":
                    {
                        _opMode = EditorOperationMode.Point;
                        break;
                    }
                case "rhombButton":
                    {
                        _opMode = EditorOperationMode.Rhomb;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void AddVertex(Point position)
        {
            switch (_opMode)
            {
                case EditorOperationMode.Block:
                    {
                        if (string.IsNullOrWhiteSpace(BlockText)) return;
                        var blockVertex = new BlockVertex(string.Format("{0}", BlockText));
                        Graph.AddVertex(blockVertex);
                        var blockControl = new BlockControl(blockVertex);
                        blockControl.SetPosition(position);
                        Area.AddVertexAndData(blockVertex, blockControl, true);
                        BlockText = string.Empty;
                        RaisePropertyChanged("BlockText");
                        break;
                    }
                case EditorOperationMode.Point:
                    {
                        var pointVertex = new PointVertex();
                        Graph.AddVertex(pointVertex);
                        var pointControl = new PointControl(pointVertex);
                        pointControl.SetPosition(position);
                        Area.AddVertexAndData(pointVertex, pointControl, true);
                        break;
                    }
                case EditorOperationMode.Rhomb:
                    {
                        var rhombVertex = new RhombVertex();
                        Graph.AddVertex(rhombVertex);
                        var rhombControl = new RhombControl(rhombVertex);
                        rhombControl.SetPosition(position);
                        Area.AddVertexAndData(rhombVertex, rhombControl, true);
                        break;
                    }
            }
        }

        private void AddPoint()
        {
            var pointVertex = new PointVertex();
            Graph.AddVertex(pointVertex);
            var pointControl = new PointControl(pointVertex);
            Area.AddVertex(pointVertex, pointControl);
            Area.RelayoutGraph(true);
        }

        private void additems()
        {
            //Lets generate configured graph using pre-created data graph assigned to LogicCore object.
            //Optionaly we set first method param to True (True by default) so this method will automatically generate edges
            //  If you want to increase performance in cases where edges don't need to be drawn at first you can set it to False.
            //  You can also handle edge generation by calling manually Area.GenerateAllEdges() method.
            //Optionaly we set second param to True (True by default) so this method will automaticaly checks and assigns missing unique data ids
            //for edges and vertices in _dataGraph.
            //Note! Area.Graph property will be replaced by supplied _dataGraph object (if any).
            Area.GenerateGraph(true, true);

            /* 
             * After graph generation is finished you can apply some additional settings for newly created visual vertex and edge controls
             * (VertexControl and EdgeControl classes).
             * 
             */

            //This method sets the dash style for edges. It is applied to all edges in Area.EdgesList. You can also set dash property for
            //each edge individually using EdgeControl.DashStyle property.
            //For ex.: Area.EdgesList[0].DashStyle = GraphX.EdgeDashStyle.Dash;
            Area.SetEdgesDashStyle(EdgeDashStyle.Dash);

            //This method sets edges arrows visibility. It is also applied to all edges in Area.EdgesList. You can also set property for
            //each edge individually using property, for ex: Area.EdgesList[0].ShowArrows = true;
            Area.ShowAllEdgesArrows(true);

            //This method sets edges labels visibility. It is also applied to all edges in Area.EdgesList. You can also set property for
            //each edge individually using property, for ex: Area.EdgesList[0].ShowLabel = true;
            Area.ShowAllEdgesLabels(true);

            ZoomCtrl.ZoomToFill();
        }

        public void GraphAreaExampleSetup(GraphAreaExample area, ZoomControl zoomctrl)
        {
            this.Area = area;
            this.ZoomCtrl = zoomctrl;

            _editorManager = new EditorObjectManager(Area, ZoomCtrl);

            ZoomCtrl.MouseDown += zoomCtrl_MouseDown;

            Area.VertexSelected += area_VertexSelected;
            Area.EdgeSelected += area_EdgeSelected;

            //Lets create logic core and filled data graph with edges and vertices
            LogicCore = new Logic() { Graph = Graph };

            Area.LogicCore = LogicCore;

            LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Custom;
            LogicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.None;
            LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            LogicCore.EdgeCurvingEnabled = true;

            ////This property sets layout algorithm that will be used to calculate vertices positions
            ////Different algorithms uses different values and some of them uses edge Weight property.
            //LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
            ////Now we can set parameters for selected algorithm using AlgorithmFactory property. This property provides methods for
            ////creating all available algorithms and algo parameters.
            //LogicCore.DefaultLayoutAlgorithmParams = LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            ////Unfortunately to change algo parameters you need to specify params type which is different for every algorithm.
            //((KKLayoutParameters)LogicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            ////This property sets vertex overlap removal algorithm.
            ////Such algorithms help to arrange vertices in the layout so no one overlaps each other.
            //LogicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            ////Default parameters are created automaticaly when new default algorithm is set and previous params were NULL
            //LogicCore.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 50;
            //LogicCore.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 50;

            ////This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
            ////For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
            ////Bundling algorithm will try to tie different edges that follows same direction to a single channel making complex graphs more appealing.
            //LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            ////This property sets async algorithms computation so methods like: Area.RelayoutGraph() and Area.GenerateGraph()
            ////will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
            ////Area.RelayoutFinished and Area.GenerateGraphFinished.
            //LogicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to GraphArea object


            //Area.SetVerticesDrag(true, true);
        }
        void area_EdgeSelected(object sender, EdgeSelectedEventArgs args)
        {
            if (args.MouseArgs.LeftButton == MouseButtonState.Pressed && _opMode == EditorOperationMode.Delete)
                Area.RemoveEdge(args.EdgeControl.Edge as DataEdge, true);
        }
        void area_VertexSelected(object sender, VertexSelectedEventArgs args)
        {
            if (args.MouseArgs.LeftButton == MouseButtonState.Pressed)
            {
                switch (_opMode)
                {
                    case EditorOperationMode.Select:
                        {
                            CreateEdgeControl(args.VertexControl);
                            break;
                        }
                    case EditorOperationMode.Delete:
                        SafeRemoveVertex(args.VertexControl);
                        break;
                    default:
                        if (_opMode != EditorOperationMode.Select && args.Modifiers == ModifierKeys.Control)
                            SelectVertex(args.VertexControl);
                        break;
                }
            }
        }

        void zoomCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //create vertices and edges only in Edit mode
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = ZoomCtrl.TranslatePoint(e.GetPosition(ZoomCtrl), Area);
                pos.Offset(-22.5, -22.5);
                if (_opMode == EditorOperationMode.Block || _opMode == EditorOperationMode.Point || _opMode == EditorOperationMode.Rhomb)
                {
                    AddVertex(pos);
                }
                else
                {
                    _ecFrom = null;
                    _editorManager.DestroyVirtualEdge();
                }

                //var vc = CreateVertexControl(pos);

                //if (_ecFrom != null)
                //    CreateEdgeControl(vc);
                //}
                //else if (_opMode == EditorOperationMode.Select)
                //{
                //    //ClearSelectMode(true);
                //}
            }
        }

        private void CreateEdgeControl(VertexControl vc)
        {
            if (_ecFrom == null)
            {
                _editorManager.CreateVirtualEdge(vc, vc.GetPosition());
                _ecFrom = vc;
                HighlightBehaviour.SetHighlighted(_ecFrom, true);
                return;
            }
            if (_ecFrom == vc) return;

            var data = new DataEdge((VertexBase)_ecFrom.Vertex, (VertexBase)vc.Vertex);
            var ec = new EdgeControl(_ecFrom, vc, data);
            Area.InsertEdgeAndData(data, ec, 0, true);

            HighlightBehaviour.SetHighlighted(_ecFrom, false);
            _ecFrom = null;
            _editorManager.DestroyVirtualEdge();
        }

        private static void SelectVertex(DependencyObject vc)
        {
            if (DragBehaviour.GetIsTagged(vc))
            {
                HighlightBehaviour.SetHighlighted(vc, false);
                DragBehaviour.SetIsTagged(vc, false);
            }
            else
            {
                HighlightBehaviour.SetHighlighted(vc, true);
                DragBehaviour.SetIsTagged(vc, true);
            }
        }

        private void SafeRemoveVertex(VertexControl vc)
        {
            //remove vertex and all adjacent edges from layout and data graph
            Area.RemoveVertexAndEdges(vc.Vertex as VertexBase);
        }
      
    }
}
