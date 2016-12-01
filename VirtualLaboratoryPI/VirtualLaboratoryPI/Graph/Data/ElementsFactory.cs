using GraphX.Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphX;
using GraphX.Controls;
using System.Windows;

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
            var type = vertexData.GetType();
            return new PointControl(vertexData);
        }
    }
}
