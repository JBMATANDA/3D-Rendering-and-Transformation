using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Shader
{
    class Shader
    {
        private float ambientPower;
        private Vector3 lightPos;
        private float lightPower;
        private Matrix lightsViewProjectionMatrix;

        public Shader()
        {

        }

        private void UpdateLightData()
        {
            ambientPower = 0.2f;
            lightPos = new Vector3(-18, 5, -2);

            lightPower = 2f;

            Matrix lightsView = Matrix.CreateLookAt(lightPos, new Vector3(-2, 3, -10), Vector3.Up);
            Matrix lightsProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 1f, 5f, 100f);

            lightsViewProjectionMatrix = lightsView * lightsProjection;
        }

        //private void CreateShadedModel(int entityId, Vector3 position, Model model, Texture2D texture)
        //{
        //    var transformComponent = new TransformComponent(entityId);
        //    transformComponent.Position = position;
        //    transformComponent.Scale = 1f;
        //
        //    var modelComponent = new ModelComponent(entityId);
        //    modelComponent.Model = model;
        //    modelComponent.Texture2D = texture;
        //    modelComponent.ObjectWorld = World
        //        * Matrix.CreateScale(new Vector3(transformComponent.Scale))
        //        * Matrix.CreateTranslation(transformComponent.Position);
        //
        //    var effectSettingsComponent = new GameEffectSettingsComponent(entityId);
        //    effectSettingsComponent.Effect = Content.Load<Effect>("Shadow");
        //
        //    ComponentManager.Instance.AddComponent(modelComponent);
        //    ComponentManager.Instance.AddComponent(effectSettingsComponent, typeof(EffectSettingsComponent));
        //    ComponentManager.Instance.AddComponent(transformComponent);
        //}
        //
        //private void CreateCamera(int cameraId)
        //{
        //    var cameraComponent = new CameraComponent(cameraId);
        //    cameraComponent.CameraPosition = new Vector3(40, 30, -30);
        //    cameraComponent.CameraForward = new Vector3(0, -0.4472136f, 0.8944272f);
        //    cameraComponent.BoundingFrustrum = new BoundingFrustum(Matrix.Identity);
        //    cameraComponent.AspectRatio = (float)windowWidth / (float)windowHeight;
        //    cameraComponent.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
        //                                                     cameraComponent.AspectRatio,
        //                                                     1.0f, 200.0f);
        //    cameraComponent.CameraTurnSpeed = 0.01f;
        //    cameraComponent.CameraMoveSpeed = 1f;
        //    ComponentManager.Instance.AddComponent(cameraComponent);
        //}
        //
        //private void CreateLighting(int lightID)
        //{
        //    LightSettingsComponent lightComponent = new LightSettingsComponent(lightID);
        //
        //    lightComponent.LightDirection = new Vector3(-0.3333333f, 0.6666667f, 0.6666667f);
        //    lightComponent.DiffusColor = Color.White.ToVector4();
        //    lightComponent.DiffuseIntensity = 0.5f;
        //    lightComponent.DiffuseLightDirection = lightComponent.LightDirection;
        //    lightComponent.AmbientColor = Color.White.ToVector4();
        //    lightComponent.AmbientIntensity = 0.2f;
        //    lightComponent.RenderTarget = new RenderTarget2D(graphics.GraphicsDevice,
        //            shadowMapWidth,
        //            shadowMapHeight,
        //            true,
        //            SurfaceFormat.Single,
        //            DepthFormat.Depth24);
        //    lightComponent.FogColor = Color.CornflowerBlue.ToVector4();
        //    lightComponent.FogEnabled = true;
        //    lightComponent.FogStart = 80f;
        //    lightComponent.FogEnd = 200f;
        //
        //    ComponentManager.Instance.AddComponent(lightComponent);
        //}
    }
}
