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
        private LightSettingsComponent lightComp;
        private BoundingFrustum boundingFrustum;
        private Texture2D texture;
        private float scale = 1.5f;
        BoundingBox boundingBox;
        private Vector3 size = new Vector3(1, 1, 1);
        private float height;
        private Matrix modelProjection;

        public Matrix modelView;
        private GraphicsDevice graphicsDevice;

        public Models(Model treeModel, Vector3 pos, Texture2D texture, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            model = treeModel;
            //model.Bones[0].Transform = Matrix.CreateTranslation(pos);
            //worldMatrix = //Matrix.CreateScale(scale) * 
            //    Matrix.CreateTranslation(pos);
            this.pos = pos;

            Vector3 min = pos + Vector3.Up * height - size / 2f;
            Vector3 max = pos + Vector3.Up * height + size / 2f;
            boundingBox = new BoundingBox(min, max);

            var cameraComp = ComponentManager.Get.EntityComponent<CameraComponent>(0);
            lightComp = ComponentManager.Get.EntityComponent<LightSettingsComponent>(0);

            boundingFrustum = new BoundingFrustum(cameraComp.View * cameraComp.Projection);
            this.texture = texture;
        }
        public void Update(GameTime gameTime)
        {
            var rotationSpeed = 4.5f;
            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var angle = -elapsedGameTime * rotationSpeed;

            Quaternion rotation = Quaternion.CreateFromAxisAngle(axis, angle);
            rotation.Normalize();

            model.Bones[0].Transform *=

            Matrix.CreateScale(scale)
            * Matrix.CreateTranslation(-model.Bones[0].Transform.Translation)
            * Matrix.CreateFromQuaternion(rotation)
            * Matrix.CreateTranslation(model.Bones[0].Transform.Translation);

            worldMatrix = //Matrix.CreateRotationX(4.7f) * 
                Matrix.CreateScale(10f)
                * Matrix.CreateTranslation(pos);
        }
        public void Draw(Matrix view, Matrix projection)
        {
            if (boundingFrustum.Intersects(boundingBox))
            {
                //lightComp.UpdateLightViewProjection(boundingFrustum.GetCorners());
                //CreateShadowMap(lightComp);
                //DrawWithShadowMap(lightComp);
                
                //var cameraComp = ComponentManager.Get.EntityComponent<CameraComponent>(0);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = worldMatrix;
                        effect.View = view;
                        effect.Projection = projection;
                        effect.FogEnabled = true;
                        effect.FogStart = 200.0f;
                        effect.FogEnd = 10000.0f;
                        
                
                        effect.FogColor = new Vector3(100, 149, 237);
                
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

        void CreateShadowMap(LightSettingsComponent lightSettingsComponent)
        {
            graphicsDevice.SetRenderTarget(lightSettingsComponent.RenderTarget);
            graphicsDevice.Clear(Color.White);
            
            DrawModel("CreateShadowMap");
            //graphicsDevice.SetRenderTarget(null);
        }

        void DrawWithShadowMap(LightSettingsComponent lightSettingsComponent)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            graphicsDevice.SamplerStates[0] = new SamplerState
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp,
                AddressW = TextureAddressMode.Clamp,
                Filter = TextureFilter.Linear,
                ComparisonFunction = CompareFunction.LessEqual,
                FilterMode = TextureFilterMode.Comparison
            };

            DrawModel("DrawWithShadowMap");

        }

        void DrawModel(string techniqueName)
        {
            var effectSettingsComponent = ComponentManager.Get.EntityComponent<EffectSettingsComponent>(0);

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    //var world = transforms[mesh.ParentBone.Index] * modelComponent.ObjectWorld * worldMatrix;
                    effectSettingsComponent.Apply(meshPart.Effect, texture, worldMatrix, techniqueName);

                    graphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
                    graphicsDevice.Indices = meshPart.IndexBuffer;
                    graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
                }
            }
        }
    }
}