using GraphX.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLaboratoryPI.Graph.Data.Controls
{
    public class PointControl : VertexControl
    {
        public PointControl(object vertexData, bool tracePositionChange = true, bool bindToDataObject = true) : base(vertexData, tracePositionChange, bindToDataObject)
        {

        }
    }

}
