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

                camera.camPosition = new Vector3(0f, 0f, 0f);
                camera.camTarget = new Vector3(0f, 0f, -100f);
            
                camera.worldMatrix = Matrix.CreateWorld(camera.camTarget, Vector3.Forward, Vector3.Up);
                camera.viewMatrix = Matrix.CreateLookAt(camera.camPosition, camera.camTarget, Vector3.Up);
                camera.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(camera.degree),graphicsDevice.DisplayMode.AspectRatio,camera.near,camera.far);
            }
        }
    }
}
