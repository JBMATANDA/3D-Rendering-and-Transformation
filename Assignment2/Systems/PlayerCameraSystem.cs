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
        private Matrix humanWorld;
        public PlayerCameraSystem(Matrix humanWorld)
        {
            this.humanWorld = humanWorld;
        }

        public void Update(GameTime gameTime)
        {
            var cameraComponents = ComponentManager.Get.GetComponents<CameraComponent>();
            foreach (CameraComponent cameraComp in cameraComponents.Values)
            {
                Vector3 cameraPosition = humanWorld.Translation + humanWorld.Backward * 40f;
                Vector3 cameraLookAt = humanWorld.Translation;

                cameraComp.View = Matrix.CreateLookAt(cameraPosition, cameraLookAt, Vector3.Up);
            }
        }
    }
}
