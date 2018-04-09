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
    public class RotatorSystem
    {
        //public void Update(GameTime gameTime)
        //{
        //    var modelComponents = ComponentManager.Get.GetComponents<ModelComponent>();
        
        
        //    var modelComp = modelComponents.Value as ModelComponent;
        
        //    float rotationSpeed = 0.01f;
        //    float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        //    float angle = -elapsedGameTime * rotationSpeed;
        
        //    //Propeller
        //    var axis = Vector3.Up;
        //    Quaternion rot = Quaternion.CreateFromAxisAngle(axis, angle);
        //    rot.Normalize();
        //    model.Bones[1].Transform *= Matrix.CreateFromQuaternion(rot);
        
        //    //Rotor
        //    var axis2 = Vector3.Left;
        //    Quaternion rot2 = Quaternion.CreateFromAxisAngle(axis2, angle);
        //    rot2.Normalize();
        //    model.Bones[3].Transform *= Matrix.CreateTranslation(-model.Bones[3].Transform.Translation)
        //        * Matrix.CreateFromQuaternion(rot2)
        //        * Matrix.CreateTranslation(model.Bones[3].Transform.Translation);
        
        //    //Note: A bone is a mesh within a mesh like the rotor and the propeller of the chopper
        //    //Your need something like this to rotate all the bones at the same time (not implemented)
        //    //objectWorld = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(position);
        
        //    RotateStuff(gameTime);
        
        //}
        
        //public void RotateStuff(GameTime gameTime)
        //{
        
        //    foreach (var bone in model.Bones)
        //    {
        //        foreach (var mesh in bone.Meshes)
        //        {
        //            // "Effect" refers to a shader. Each mesh may have multiple shaders applied to it for more advanced visuals. 
        //            foreach (BasicEffect effect in mesh.Effects)
        //            {
        //                // We could set up custom lights, but this is the quickest way to get somethign on screen:
        //                effect.EnableDefaultLighting();
        
        //                // This makes lighting look more realistic on round surfaces, but at a slight performance cost:
        //                effect.PreferPerPixelLighting = true;
        
        //                // Move the camera 30 units away from the origin:
        //                var cameraPosition = new Vector3(0, 30, 0);
        
        //                // Tell the camera to look at the origin:
        //                var cameraLookAtVector = Vector3.Zero;
        
        //                // Tell the camera that positive Z is up
        //                var cameraUpVector = Vector3.UnitZ;
        
        //                // We want the aspect ratio of our display to match the entire screen's aspect ratio:
        //                float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
        
        //                // Field of view measures how wide of a view our camera has.
        //                // Increasing this value means it has a wider view, making everything
        //                // on screen smaller. This is conceptually the same as "zooming out".
        //                // It also 
        //                float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
        
        //                // Anything closer than this will not be drawn (will be clipped)
        //                float nearClipPlane = 1;
        
        //                // Anything further than this will not be drawn (will be clipped)
        //                float farClipPlane = 100;
        
        //                // The world matrix can be used to position, rotate or resize (scale) the model. Identity means that
        //                // the model is unrotated, drawn at the origin, and its size is unchanged from the loaded content file.
        //                effect.World = bone.Transform * objectWorld;
        //                effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);
        //                effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
        
        //                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
        //                {
        //                    pass.Apply();
        //                    mesh.Draw();
        //                }
        //            }
        //        }
        //    }
        //}
        
        //protected override void Draw(GameTime gameTime)
        //{
        //    base.Draw(gameTime);
        //}
    }
}

