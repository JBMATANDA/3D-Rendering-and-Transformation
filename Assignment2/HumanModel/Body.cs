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

namespace Assignment2.HumanModel
{
    public class Body : Humanoid
    {
        public List<IHumanoid> humanoid = new List<IHumanoid>();
        private Game game;
        public Vector3 parentPosition = Vector3.Zero;
        public Vector3 parentRotation = Vector3.Zero;
        private Vector3 forward;
        private Matrix modelRotation;
        private Vector3 direction;
        

        public Matrix limbWorld;

        public Body(Game game) : base(game, "body")
        {
            Load();
            this.game = game;
            
            parentPosition = humWorld.Translation;

            direction = Vector3.Forward;

            scale = new Vector3(3, 5, 3);
            humanoid.Add(new Head(game, new Vector3(0, 4.8f,0)));
            humanoid.Add(new RightArm(game, new Vector3(2.2f, 1.5f, 0)));
            humanoid.Add(new LeftArm(game, new Vector3(-2.2f, 1.5f, 0)));
            humanoid.Add(new RightLeg(game, new Vector3(0.8f, -3.8f, 0)));
            humanoid.Add(new LeftLeg(game, new Vector3(-0.8f, -3.8f, 0)));
        }

        public override void DrawLimb(GameTime gameTime, Matrix world)
        {

            foreach (CameraComponent cameraComp in ComponentManager.Get.GetComponents<CameraComponent>().Values)
            {
                limbWorld = Matrix.CreateScale(scale) * humWorld; 
                effect.World = limbWorld * world;

                game.GraphicsDevice.SetVertexBuffer(vertexBuffer);
                game.GraphicsDevice.Indices = indexBuffer;

                direction = cameraComp.View.Backward;
                direction.Normalize();

                cameraComp.CamTarget = parentPosition;
                effect.View = cameraComp.View;
                effect.Projection = cameraComp.Projection;

                foreach (EffectPass ep in effect.CurrentTechnique.Passes)
                {
                    ep.Apply();
                    game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount / 3);
                }

                foreach (IHumanoid child in humanoid)
                {
                    child.DrawLimb(gameTime, humWorld * world);
                }
            }
        }

        public override void UpdateLimb(GameTime gameTime)
        {
            var rotationMatrix = Matrix.CreateFromYawPitchRoll(parentRotation.X, parentRotation.Y, parentRotation.Z);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                parentRotation = new Vector3(parentRotation.X - 0.05f, parentRotation.Y, parentRotation.Z);
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                parentPosition  += Vector3.Transform(Vector3.Forward, rotationMatrix);

            }

            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                parentPosition += Vector3.Transform(Vector3.Backward, rotationMatrix);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                parentPosition += Vector3.Transform(Vector3.Up, rotationMatrix);
            }
            
            
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                parentPosition += Vector3.Transform(Vector3.Down, rotationMatrix);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                parentRotation = new Vector3(parentRotation.X + 0.05f, parentRotation.Y, parentRotation.Z);
            }

            humWorld = Matrix.Identity * 
                rotationMatrix
                * Matrix.CreateTranslation(parentPosition);

           
            foreach (IHumanoid child in humanoid)
            {
                child.UpdateLimb(gameTime);
            }


        }
    }
}
