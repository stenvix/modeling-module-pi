﻿using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLaboratoryPI.Graph.Data.Vertex
{

    public class BlockVertex: VertexBase
    {
        public BlockVertex(string text="")
        {
            this.Text = text;
        }
        public string Text { get; set; }
    }
}
