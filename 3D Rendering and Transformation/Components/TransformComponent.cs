using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Rendering_and_Transformation.Components
{
    public class TransformComponent : IComponent
    {
        public Vector3 Scale { get; set; }
        public Matrix Rotation { get; set; }
        public Vector3 Position;
     
        public TransformComponent()
        {
            
        }
    }
}
