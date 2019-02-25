using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.HumanModel
{
    public class Body : Humanoid
    {
        List<Humanoid> humanoid = new List<Humanoid>();
        private Game game;
        private Vector3 parentPosition;

        private Matrix limbWorld;

        public Body(Game game) : base(game, "body")
        {
            Load();
            this.game = game;

            
            scale = new Vector3(3, 5, 3);
            parentPosition = new Vector3(0, 1, 0);

            humanoid.Add(new Head(game, new Vector3(0, 4.8f, 0)));
            humanoid.Add(new RightArm(game, new Vector3(2.2f, 1.5f, 0)));
            humanoid.Add(new LeftArm(game, new Vector3(-2.2f, 1.5f, 0)));
            humanoid.Add(new RightLeg(game, new Vector3(0.8f, -3.8f, 0)));
            humanoid.Add(new LeftLeg(game, new Vector3(-0.8f, -3.8f, 0)));
        }

        public override void DrawLimb(GameTime gameTime, Matrix world)
        {
            limbWorld = Matrix.CreateScale(scale) * humWorld * Matrix.CreateTranslation(parentPosition);
            effect.World = limbWorld * world;

            game.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            game.GraphicsDevice.Indices = indexBuffer;

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
}
