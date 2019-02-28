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
        
        public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
        {
            var cameraComponents = ComponentManager.Get.GetComponents<CameraComponent>();

            foreach (var cameraComponent in cameraComponents)
            {
                var camera = cameraComponent.Value as CameraComponent;
                var _model = ComponentManager.Get.EntityComponent<ModelComponent>(cameraComponent.Key);
                var transform = ComponentManager.Get.EntityComponent<TransformComponent>(cameraComponent.Key);
                var target = camera.CamTarget;

                if (transform.Position != null)
                {
                    target = transform.Position;
                }

                float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                camera.Far = 1000f;
                camera.Near = 0.1f;

                camera.World = Matrix.Identity;
                camera.View = Matrix.CreateLookAt(camera.CamPosition, target, Vector3.Up);
                camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, camera.Near, camera.Far);       
            }
        }

        
    }
}
