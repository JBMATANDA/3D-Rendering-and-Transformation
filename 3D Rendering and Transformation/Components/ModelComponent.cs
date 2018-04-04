using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Rendering_and_Transformation.Components
{
    public class ModelComponent : IComponent
    {
        public Texture2D texture { get; set; }
        public Model model { get; set; }
        public Matrix mainRotator { get; set; }
        public Matrix backRotator { get; set; }
        public Matrix[] transformMatrices { get; set; }

    }
}
