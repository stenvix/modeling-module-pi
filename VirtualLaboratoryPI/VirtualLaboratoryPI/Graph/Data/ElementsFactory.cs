using GraphX.Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphX;
using GraphX.Controls;
using System.Windows;
using VirtualLaboratoryPI.Graph.Data.Controls;
using VirtualLaboratoryPI.Graph.Data.Vertex;

namespace VirtualLaboratoryPI.Graph.Data
{
    public class ElementsFactory : IGraphControlFactory
    {

        public ElementsFactory(GraphAreaBase ga)
        {
            FactoryRootArea = ga;
        }

        public GraphAreaBase FactoryRootArea { get; private set; }

        public EdgeControl CreateEdgeControl(VertexControl source, VertexControl target, object edge, bool showLabels = false, bool showArrows = true, Visibility visibility = Visibility.Visible)
        {
            return new EdgeControl(source, target, edge, showLabels, showArrows);
        }

        public VertexControl CreateVertexControl(object vertexData)
        {
            if (vertexData.GetType() == typeof(PointVertex))
                return new PointControl(vertexData);

            if (vertexData.GetType() == typeof(RhombVertex))
                return new RhombControl(vertexData);

            if (vertexData.GetType() == typeof(BlockVertex))
                return new BlockControl(vertexData);

            return new VertexControl(vertexData);
        }
    }
}
