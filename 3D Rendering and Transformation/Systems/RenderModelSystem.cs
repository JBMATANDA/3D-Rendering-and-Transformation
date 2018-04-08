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
    public class RenderModelSystem
    {
    
        public void Draw(GameTime gameTime) {

            var modelComponents = ComponentManager.Get().GetComponents<ModelComponent>();
            
            foreach(var modelComponent in modelComponents)
            {
                var modelComp = modelComponent.Value as ModelComponent;
                var transformation = ComponentManager.Get().EntityComponent<TransformComponent>(modelComponent.Key);
                var camera = ComponentManager.Get().EntityComponent<CameraComponent>(modelComponent.Key);

               
                foreach(ModelMesh mesh in modelComp.Model.Meshes)
                {
                    foreach (BasicEffect eff in mesh.Effects)
                    {

                        eff.View = camera.View;
                        eff.World = mesh.ParentBone.Transform * modelComp.ObjectWorld * camera.World;
                        eff.Projection = camera.Projection;

                        eff.EnableDefaultLighting();
                        eff.LightingEnabled = true;

                        foreach (EffectPass pass in eff.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            mesh.Draw();
                        }
                    }
                }
            }
        }
   }
}
