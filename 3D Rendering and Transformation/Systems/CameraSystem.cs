using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Rendering_and_Transformation.Systems
{
    public class CameraSystem
    {
        GraphicsDevice graphicsDevice;
        public void InitializeCamera()
        {
            var cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();

            foreach (var cameraComponent in cameraComponents)
            {
                var camera = cameraComponent.Value as CameraComponent;
                var _model = ComponentManager.Get().EntityComponent<ModelComponent>(cameraComponent.Key);

                camera.CamPosition = new Vector3(0, 0, 20);
                camera.CamTarget = new Vector3(0, 0, 0);

                camera.AspectRatio = graphicsDevice.Viewport.AspectRatio;
                camera.Far = 1000f;
                camera.Near = 0.1f;

                camera.World = Matrix.Identity;
                camera.View = Matrix.CreateLookAt(camera.CamPosition, camera.CamTarget, Vector3.Up);
                camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, camera.AspectRatio,camera.Near,camera.Far);       
            }
        }
        
    }
}
