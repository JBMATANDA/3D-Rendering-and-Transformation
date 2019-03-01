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
    class LeftLeg : Humanoid
    {
        private Game game;
        private Matrix limbWorld;

        private Vector3 position;
        private Vector3 rotation = Vector3.Zero;
        private Vector3 jointPosition = new Vector3(0, 2, 0);

        public LeftLeg(Game game, Vector3 position) : base(game, "leftLeg")
        {
            Load();
            this.game = game;

            this.position = position;
            rotation.Y = 4;
        }

        public override void DrawLimb(GameTime gameTime, Matrix world)
        {
            limbWorld = Matrix.CreateScale(scale) * humWorld * Matrix.CreateTranslation(position);
            effect.World = limbWorld * world;


            game.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            game.GraphicsDevice.Indices = indexBuffer;


            //effect.View = Matrix.CreateLookAt(camPosition, jointPosition, Vector3.Up);
            //effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, game.GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
            foreach (EffectPass ep in effect.CurrentTechnique.Passes)
            {
                ep.Apply();
                game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount / 3);
            }
        }

        public override void UpdateLimb(GameTime gameTime)
        {
            float speed = 0;
            float maxRotation = 2.36f;
            float minRotation = 3.9f;
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                speed += 0.003f * (float)gameTime.ElapsedGameTime.Milliseconds;

                if (rotation.Y >= maxRotation)
                {
                    rotation.Y -= speed;
                }
                else
                {
                    rotation.Y = minRotation;
                }
            }

            humWorld = Matrix.Identity *
                Matrix.CreateTranslation(jointPosition) *
                Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z)) *
                Matrix.CreateTranslation(jointPosition);
        }
    }
}
