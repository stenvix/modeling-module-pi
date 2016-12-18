using GraphX.Controls;

namespace VirtualLaboratoryPI.Graph.Data.Controls
{
    public class BlockControl : VertexControl
    {
        public BlockControl(object vertexData, bool tracePositionChange = true, bool bindToDataObject = true) : base(vertexData, tracePositionChange, bindToDataObject)
        {
        }
    }
}
