﻿using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.HumanModel
{
    class Head : Humanoid
    {
        private Game game;
        public Matrix limbWorld;
        private Vector3 position;
        private Vector3 jointPosition = new Vector3(0, 1, 0);
        private Vector3 rotation = Vector3.Zero;
        
        public Head(Game game, Vector3 position) : base(game, "head")
        {
            Load();
            this.game = game;

            this.position = position;
        }

        public override void DrawLimb(GameTime gameTime, Matrix world)
        {

            foreach (CameraComponent cameraComp in ComponentManager.Get.GetComponents<CameraComponent>().Values)
            {
                limbWorld = Matrix.CreateScale(scale) * humWorld * Matrix.CreateTranslation(position);
                effect.World = limbWorld * world;


                game.GraphicsDevice.SetVertexBuffer(vertexBuffer);
                game.GraphicsDevice.Indices = indexBuffer;

                cameraComp.CamTarget = jointPosition;

                effect.View = cameraComp.View;
                effect.Projection = cameraComp.Projection;

                foreach (EffectPass ep in effect.CurrentTechnique.Passes)
                {
                    ep.Apply();
                    game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount / 3);
                }
            }
        }

        public override void UpdateLimb(GameTime gameTime)
        {
            base.UpdateLimb(gameTime);
        }
    }
}
