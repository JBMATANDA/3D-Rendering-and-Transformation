using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Systems
{
    public class PlayerRenderSystem
    {
        private BasicEffect effect;

        public PlayerRenderSystem(BasicEffect effect)
        {
            this.effect = effect;
        }

        public void Update(GameTime gameTime)
        {
            var cameraComponents = ComponentManager.Get.GetComponents<CameraComponent>();
            foreach (CameraComponent cameraComp in cameraComponents.Values)
            {
                effect.EnableDefaultLighting();
                effect.View = cameraComp.View;
                effect.Projection = cameraComp.Projection;
                //effect.World = cameraComp.World;
            }
        }
    }
}
