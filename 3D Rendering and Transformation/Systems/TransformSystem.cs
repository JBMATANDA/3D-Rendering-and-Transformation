using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Rendering_and_Transformation.Systems
{
    public class TransformSystem
    {
        public void Update(GameTime gameTime)
        {
            var modelComponents = ComponentManager.Get.GetComponents<ModelComponent>();

            foreach (var modelComponent in modelComponents)
            {

                var modelComp = modelComponent.Value as ModelComponent;

                float rotationSpeed = 0.01f;
                float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                float angle = -elapsedGameTime * rotationSpeed;

                //Propeller
                var axis = Vector3.Up;
                Quaternion rot = Quaternion.CreateFromAxisAngle(axis, angle);
                rot.Normalize();
                modelComp.Model.Bones[1].Transform *= Matrix.CreateFromQuaternion(rot);

                //Rotor
                var axis2 = Vector3.Left;
                Quaternion rot2 = Quaternion.CreateFromAxisAngle(axis2, angle);
                rot2.Normalize();
                modelComp.Model.Bones[3].Transform *= Matrix.CreateTranslation(-modelComp.Model.Bones[3].Transform.Translation)
                    * Matrix.CreateFromQuaternion(rot2)
                    * Matrix.CreateTranslation(modelComp.Model.Bones[3].Transform.Translation);

            }

        }
    }     
}

