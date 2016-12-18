using GraphX.Controls;
using GraphX.PCL.Common.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLaboratoryPI.Graph.Data;

namespace VirtualLaboratoryPI.Graph.Area
{
    public class GraphAreaExample : GraphArea<VertexBase, EdgeBase<VertexBase>, BidirectionalGraph<VertexBase, EdgeBase<VertexBase>>> {
        public GraphAreaExample() {
            ControlFactory = new ElementsFactory(this);
         }
    }
}
