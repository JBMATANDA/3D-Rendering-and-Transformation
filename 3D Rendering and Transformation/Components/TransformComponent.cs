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
        public Matrix scaling { get; set; }
        public Matrix rotation { get; set; }
        public Matrix position { get; set; }
        public  float MAXROTATION { get ;set; }
         public float speed { get; set; }
    }
}
