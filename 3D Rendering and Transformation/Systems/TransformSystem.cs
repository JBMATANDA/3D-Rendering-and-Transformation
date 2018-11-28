using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
                var transform = ComponentManager.Get.EntityComponent<TransformComponent>(modelComponent.Key);
                var camera = ComponentManager.Get.EntityComponent<CameraComponent>(modelComponent.Key);

                var rotationSpeed = 0.01f;
                var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                var angle = -elapsedGameTime * rotationSpeed;
                var speed = new Vector3(0.1f, 0.1f, 0.1f);

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

                
               transform.Position = new Vector3(0, 0, 0);
                
               
                // Rotate chooper
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                   transform.Axis = new Vector3(1f, 0, 0);                 
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                   transform.Axis = new Vector3(-1f, 0, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                   transform.Axis = new Vector3(0, -1f, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                   transform.Axis = new Vector3(0, 1f, 0);
                }

                Quaternion rotation = Quaternion.CreateFromAxisAngle(transform.Axis, angle);
                rotation.Normalize();
                transform.Rotation *= Matrix.CreateFromQuaternion(rotation);

                modelComp.Model.Bones[0].Transform *= Matrix.CreateTranslation(-modelComp.Model.Bones[0].Transform.Translation)
                    * Matrix.CreateFromQuaternion(rotation)
                    * Matrix.CreateTranslation(modelComp.Model.Bones[0].Transform.Translation);

                camera.World = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(transform.Position);
            }

        }
    }     
}

