﻿using _3D_Rendering_and_Transformation.Components;
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
    public class HeightMapSystem
    {
        VertexPositionNormalTexture[] vertices;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        ComponentManager cm = ComponentManager.Get;
        GraphicsDevice device;
        Game game;

        int[] indices;

        private float[,] heightData;

        public void SetUpHeightMap(Game game, Texture2D heightMap)
        {
            BasicEffect effect = new BasicEffect(game.GraphicsDevice);
            effect.TextureEnabled = true;
            LoadHeightData(heightMap);
            SetUpVertices();
            SetupVertexBuffer(game);
            SetUpIndices();
            SetupIndexBuffer(game);
            GetHeightMapData();
        }

        public void SetupVertexBuffer(Game game)
        {
            vertexBuffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.None);
            vertexBuffer.SetData(vertices);
        }

        public void SetUpVertices()
        {
            Vector2 texturePosition;
            var heightMapDictionary = cm.GetComponents<HeightMapComponent>();

            foreach (var heightMapComp in heightMapDictionary)
            {
                var heightMapComponent = heightMapComp.Value as HeightMapComponent;

                vertices = new VertexPositionNormalTexture[heightMapComponent.Width * heightMapComponent.Height];

                for (int x = 0; x < heightMapComponent.Width; x++)
                {
                    for (int y = 0; y < heightMapComponent.Height; y++)
                    {
                        texturePosition = new Vector2((float)x / 25.5f, (float)y / 25.5f);
                        vertices[x + y * heightMapComponent.Width] =
                            new VertexPositionNormalTexture(new Vector3(x, heightData[x, y], -y), Vector3.Zero, texturePosition);
                    }

                }
            }
        }
        public void SetupIndexBuffer(Game game)
        {
            indexBuffer = new IndexBuffer(game.GraphicsDevice, typeof(short), indices.Length, BufferUsage.None);
            indexBuffer.SetData(indices);
        }
        public void SetUpIndices()
        {
            var heightMapDictionary = cm.GetComponents<HeightMapComponent>();
            foreach (var heightMapComp in heightMapDictionary)
            {
                var heightMapComponent = heightMapComp.Value as HeightMapComponent;

                indices = new int[(heightMapComponent.Width - 1) * (heightMapComponent.Height - 1) * 6];
                int counter = 0;
                for (int y = 0; y < heightMapComponent.Height - 1; y++)
                {
                    for (int x = 0; x < heightMapComponent.Width - 1; x++)
                    {
                        int lowerLeft = x + y * heightMapComponent.Width;
                        int lowerRight = (x + 1) + y * heightMapComponent.Width;
                        int topLeft = x + (y + 1) * heightMapComponent.Width;
                        int topRight = (x + 1) + (y + 1) * heightMapComponent.Width;

                        indices[counter++] = topLeft;
                        indices[counter++] = lowerRight;
                        indices[counter++] = lowerLeft;

                        indices[counter++] = topLeft;
                        indices[counter++] = topRight;
                        indices[counter++] = lowerRight;
                    }
                }
            }
        }
        public void GetHeightMapData()
        {
            foreach (HeightMapComponent heightMap in ComponentManager.Get.GetComponents<HeightMapComponent>().Values)
            {
                heightMap.heightData = heightData;

            }
        }
        public void LoadHeightData(Texture2D heightMap)
        {
            float minimumHeight = float.MaxValue;
            float maximumHeight = float.MinValue;

            var terrainWidth = heightMap.Width;
            var terrainHeight = heightMap.Height;

            Color[] heightMapColors = new Color[terrainWidth * terrainHeight];
            heightMap.GetData(heightMapColors);

            heightData = new float[terrainWidth, terrainHeight];
            for (int x = 0; x < terrainWidth; x++)
                for (int y = 0; y < terrainHeight; y++)
                {
                    heightData[x, y] = heightMapColors[x + y * terrainWidth].R;
                    if (heightData[x, y] < minimumHeight)
                        minimumHeight = heightData[x, y];

                    if (heightData[x, y] > maximumHeight)
                        maximumHeight = heightData[x, y];
                }
            for (int x = 0; x < terrainWidth; x++)
                for (int y = 0; y < terrainHeight; y++)
                    heightData[x, y] = (heightData[x, y] - minimumHeight) / (maximumHeight - minimumHeight) * 30.0f;
        }

        public void Draw(GameTime gameTime, GraphicsDevice device)
        {
            var heightMapDictionary = cm.GetComponents<HeightMapComponent>();
            var cameraDictionary = cm.GetComponents<CameraComponent>();


            var lightDictionary = cm.GetComponents<LightSettingsComponent>();

            var heightMapComponent = (HeightMapComponent)heightMapDictionary[0];
            var camera = (CameraComponent)cameraDictionary[0];
            var light = (LightSettingsComponent)lightDictionary[0];

            camera.UpdateFrustum();
            light.UpdateLightViewProjection(camera.BoundingFrustum.GetCorners());

            CreateShadowMap(light, device);
            DrawWithShadowMap(light, device);

            //RasterizerState rs = new RasterizerState();
            //rs.CullMode = CullMode.None;
            //rs.FillMode = FillMode.WireFrame;
            //device.RasterizerState = rs;


            //heightMapComponent.Effect.CurrentTechnique = heightMapComponent.Effect.Techniques["DrawWithShadowMap"];
            //heightMapComponent.Effect.Parameters["View"].SetValue(camera.View);
            //heightMapComponent.Effect.Parameters["Projection"].SetValue(camera.Projection);
            //heightMapComponent.Effect.Parameters["World"].SetValue(worldMatrix);
            //heightMapComponent.Effect.Parameters["Texture"].SetValue(heightMapComponent.Texture);
            //heightMapComponent.Effect.Parameters["LightDirection"].SetValue(light.LightDirection);
            //heightMapComponent.Effect.Parameters["FogEnabled"].SetValue(true);
            //heightMapComponent.Effect.Parameters["FogStart"].SetValue(100f);
            //heightMapComponent.Effect.Parameters["FogEnd"].SetValue(1000f);
            //
            ////heightMapComponent.Effect.Parameters["FogColor"].SetValue(new Vector3(50, 0, 50));
            ////heightMapComponent.Effect.Parameters["Ambient"].SetValue(1f);
            //
            //device.SetVertexBuffer(vertexBuffer);
            //device.Indices = indexBuffer;
            //
            //foreach (EffectPass pass in heightMapComponent.Effect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //
            //    device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionNormalTexture.VertexDeclaration);
            //}


        }

        void CreateShadowMap(LightSettingsComponent lightSettingsComponent, GraphicsDevice device)
        {
            device.SetRenderTarget(lightSettingsComponent.RenderTarget);
            device.Clear(Color.White);

            DrawModel("CreateShadowMap", device);
            device.SetRenderTarget(null);
        }

        void DrawWithShadowMap(LightSettingsComponent lightSettingsComponent, GraphicsDevice device)
        {
            device.Clear(Color.CornflowerBlue);
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.RasterizerState = RasterizerState.CullCounterClockwise;
            device.SamplerStates[0] = new SamplerState
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp,
                AddressW = TextureAddressMode.Clamp,
                Filter = TextureFilter.Linear,
                ComparisonFunction = CompareFunction.LessEqual,
                FilterMode = TextureFilterMode.Comparison
            };

            DrawModel("DrawWithShadowMap", device);

        }

        void DrawModel(string techniqueName, GraphicsDevice device)
        {
            var effectSettingsComponent = ComponentManager.Get.EntityComponent<EffectSettingsComponent>(0);
            var heightMapComponent = ComponentManager.Get.EntityComponent<HeightMapComponent>(0);

            Matrix worldMatrix = Matrix.CreateTranslation(-heightMapComponent.Width / 2.0f, -0.5f * heightMapComponent.Height / 2.0f, 0f);

            effectSettingsComponent.Apply(heightMapComponent.Effect, heightMapComponent.Texture, worldMatrix, techniqueName);
            device.SetVertexBuffer(vertexBuffer);
            device.Indices = indexBuffer;

            foreach (EffectPass pass in heightMapComponent.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionNormalTexture.VertexDeclaration);
            }
        }
    }

}


