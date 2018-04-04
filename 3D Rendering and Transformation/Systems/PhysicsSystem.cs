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
    public class PhysicsSystem
    {
        public void CreateModel(GraphicsDevice graphics, Texture2D texture,Model model, BasicEffect effect) {
            var modelComponents = ComponentManager.Get().GetComponents<ModelComponent>();
            
            foreach(var modelComponent in modelComponents)
            {
                var theModel = modelComponent.Value as ModelComponent;
                var transformation = ComponentManager.Get().EntityComponent<TransformComponent>(modelComponent.Key);

                theModel.transformMatrices = new Matrix[theModel.model.Bones.Count];

                transformation.scaling = Matrix.CreateScale(1f);
                transformation.rotation = Quaternion.CreateFromAxisAngle()

            }


    }
    }
}
