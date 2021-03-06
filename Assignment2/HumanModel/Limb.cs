﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.HumanModel
{
    public class Limb : Humanoid
    {
        private Vector3 Rotation = Vector3.Zero;
        public Vector3 Position = Vector3.Zero;
        private Vector3 JointPos = new Vector3(0, 1.5f, 0);
        private bool isLeg = false;

        public Limb(GraphicsDevice graphics, Vector3 size, Vector3 position, Texture2D texture) : base(graphics, size, texture)
        {
            JointPos = position;
        }
        public Limb(GraphicsDevice graphics, Vector3 size, Vector3 position, Texture2D texture, bool isLeg) : base(graphics, size, texture)
        {
            JointPos = position;
            if (isLeg == true) this.isLeg = true;
        }

        public override void UpdateLimb(GameTime gameTime)
        {
            if (isLeg && (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D)))
                Rotation = new Vector3(Rotation.X, Rotation.Y - 0.2f, Rotation.Z);

            WorldMatrix = Matrix.Identity *
                Matrix.CreateTranslation(Position) *
                Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z)) *
                Matrix.CreateTranslation(JointPos);

        }

        public override void DrawLimb(BasicEffect effect, Matrix world)
        {
            effect.World = WorldMatrix * world;
            effect.CurrentTechnique.Passes[0].Apply();
            effect.Texture = Texture;
            GraphicsDevice.SetVertexBuffer(VertexBuffer);
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, VertexBuffer.VertexCount);
        }

    }
}
