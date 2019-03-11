using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using Assignment2.Interfaces;
using Assignment2.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Rendering_and_Transformation.Systems
{
    public class Models : AbstractModel, IModel
    {
        Vector3 axis = Vector3.Zero;
        private Vector3 pos;
        private Texture2D texture;
        private float scale = 1.5f;
        public Models(Model treeModel, Vector3 pos, Texture2D texture)
        {
            model = treeModel;
            //model.Bones[0].Transform = Matrix.CreateTranslation(pos);
            //worldMatrix = //Matrix.CreateScale(scale) * 
            //    Matrix.CreateTranslation(pos);
            this.pos = pos;

            this.texture = texture;
        }
        public void Update(GameTime gameTime)
        {
            var rotationSpeed = 0.01f;
            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var angle = -elapsedGameTime * rotationSpeed;

            Quaternion rotation = Quaternion.CreateFromAxisAngle(axis, angle);
            rotation.Normalize();

            model.Bones[0].Transform *=

            Matrix.CreateScale(scale)
            * Matrix.CreateTranslation(-model.Bones[0].Transform.Translation)
            * Matrix.CreateFromQuaternion(rotation)
            * Matrix.CreateTranslation(model.Bones[0].Transform.Translation);

            worldMatrix = //Matrix.CreateRotationY(angle) * 
                Matrix.CreateScale(0.3f)
                * Matrix.CreateTranslation(pos);
        }
        public void Draw(Matrix view, Matrix projection)
        {

            //var cameraComp = ComponentManager.Get.EntityComponent<CameraComponent>(0);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = worldMatrix;
                    effect.View = view;
                    effect.Projection = projection;

                    effect.LightingEnabled = true;
                    //effect.Texture = texture;
                    //effect.TextureEnabled = true;

                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        mesh.Draw();
                    }
                }

            }

        }
    }
}