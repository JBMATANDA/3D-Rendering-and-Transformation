using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Rendering_and_Transformation.Components
{
    public class CameraComponent: IComponent
    {
        public Vector3 camTarget { get; set; }
        public Vector3 camPosition { get; set; }
        public Matrix projectionMatrix { get; set; }
        public Matrix viewMatrix { get; set; }
        public Matrix worldMatrix { get; set; }
        public float near { get; set; }
        public float far { get; set; }
        public float degree { get; set; }
        public float aspectRatio { get; set;}
    }
}
