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
        public Vector3 CamTarget;
        public Vector3 CamPosition;
        public Matrix Projection { get; set; }
        public Matrix View { get; set; }
        public Matrix World { get; set; }
        public float Near { get; set; }
        public float Far { get; set; }
    }
}
