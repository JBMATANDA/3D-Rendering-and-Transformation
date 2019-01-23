using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Rendering_and_Transformation.Components
{
    public class HeightMapComponent :IComponent
    {
        public Texture2D HeightMap { get; set; }
        public Effect Effect { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }
        public VertexPositionTexture[] Verticies { get; set; }
        public float[,] heightData { get; set; }
        public Texture2D Texture { get; set; }
    }
}
