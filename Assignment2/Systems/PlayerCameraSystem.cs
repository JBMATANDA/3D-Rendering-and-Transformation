using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Assignment2.HumanModel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Systems
{
    public class PlayerCameraSystem
    {
        private Body human;
        public PlayerCameraSystem(Body human)
        {
            this.human = human;
        }

        public void Update(GameTime gameTime)
        {
            var cameraComponents = ComponentManager.Get.GetComponents<CameraComponent>();
            foreach (CameraComponent cameraComp in cameraComponents.Values)
            {
                Vector3 cameraPosition = human.humWorld.Translation + human.humWorld.Backward * 10f;
                Vector3 cameraLookAt = human.humWorld.Translation;

                cameraComp.View = Matrix.CreateLookAt(cameraPosition, cameraLookAt, Vector3.Up);
                cameraComp.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, cameraComp.AspectRatio, cameraComp.Near, cameraComp.Far);

            }
        }
        
        
    }
}
