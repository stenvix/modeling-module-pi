using GraphX.PCL.Common.Models;
using GraphX.PCL.Logic.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLaboratoryPI.Graph.Data;

namespace VirtualLaboratoryPI.Graph.Logic
{
    public class Logic : GXLogicCore<VertexBase, EdgeBase<VertexBase>, BidirectionalGraph<VertexBase, EdgeBase<VertexBase>>> { }
}
