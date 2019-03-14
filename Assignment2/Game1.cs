using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _3D_Rendering_and_Transformation.Components;
using _3D_Rendering_and_Transformation.Managers;
using _3D_Rendering_and_Transformation.Systems;
using Assignment2.HumanModel;
using Assignment2.Systems;
using System;
using System.Collections.Generic;

namespace Assignment2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        private CameraSystem cameraSystem;
        private RenderModelSystem renderModelSystem;
        private TransformSystem transformSystem;
        private HeightMapSystem heightMapSystem;
        Texture2D heightMap;
        private Model model2;
        private Matrix view;
        private Matrix projection;
        BasicEffect bEffect;
        
        private Matrix world;
        private Body humanoid;
        private Texture2D texture;
        private Model model1;
        private Effect effect;
        private PlayerCameraSystem playerCameraSystem;
        private Model model;
        private List<Vector3> modelPositions;
        private List<Models> trees;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            cameraSystem = new CameraSystem();
            renderModelSystem = new RenderModelSystem();
            transformSystem = new TransformSystem();
            heightMapSystem = new HeightMapSystem();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            world = Matrix.Identity;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            
            heightMap = Content.Load<Texture2D>("US_Canyon");
            bEffect = new BasicEffect(GraphicsDevice);
            effect = Content.Load<Effect>("effects");
            texture = Content.Load<Texture2D>("quikscopeobama");
            model1 = Content.Load<Model>("lowpolytree");
            model2 = Content.Load<Model>("tree01");

            view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);


            CreateEntities();

            heightMapSystem.SetUpHeightMap(this, heightMap);
            humanoid = new Body(this);

            playerCameraSystem = new PlayerCameraSystem(humanoid);

            CreateRandomEntities();

            //playerRenderSystem = new PlayerRenderSystem(humanoid.effect);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //cameraSystem.Update(graphics, gameTime);
            
            playerCameraSystem.Update(gameTime);
            humanoid.UpdateLimb(gameTime);
            transformSystem.Update(gameTime);
            // TODO: Add your update logic here
            UpdateModels(trees, gameTime);
            //transformSystem.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            renderModelSystem.Draw(gameTime);
            //playerRenderSystem.Update(gameTime);
            heightMapSystem.Draw(gameTime, graphics.GraphicsDevice);
            humanoid.DrawLimb(gameTime, world);
            DrawModels(trees);
            base.Draw(gameTime);
        }
        private void CreateEntities()
        {
            var id = ComponentManager.Get.NewEntity();
            ComponentManager.Get.AddComponentsToEntity(new CameraComponent() { View = view, AspectRatio = GraphicsDevice.Viewport.AspectRatio, Near = 0.1f, Far = 1000.0f }, id);
            //ComponentManager.Get.AddComponentsToEntity(new TransformComponent() { Position = position, Axis = axis }, id);
            //ComponentManager.Get.AddComponentsToEntity(new ModelComponent() { Model = model2 }, id);
            ComponentManager.Get.AddComponentsToEntity(new HeightMapComponent() { HeightMap = heightMap, Texture = texture, Effect = effect, Width = 1081, Height = 1081 }, id);

        }

        private void CreateRandomEntities()
        {
            Model[] modelArray = new[] { model1, model2 };

            trees = CreateTrees(modelArray, 100);

        }


        private List<Models> CreateTrees(Model[] modelArray, int nModels)
        {

            Random r = new Random();
            var heightMap = ComponentManager.Get.EntityComponent<HeightMapComponent>(0);

            List<Models> trees = new List<Models>();
            modelPositions = GeneratePositions(heightMap.heightData, nModels);
            for (int i = 0; i < nModels; i++)
            {
                int randomIndex = r.Next(modelArray.Length);
                Model randomModel = modelArray[randomIndex];
                var tree = new Models(randomModel, modelPositions[i], texture);
                trees.Add(tree);
            }

            return trees;
        }

        private List<Vector3> GeneratePositions(float[,] heightmapData, int nPositions)
        {
            List<Vector3> positions = new List<Vector3>();

            Random rnd = new Random();
            int x = 2;
            int z = 2;
            for (int i = 0; i < nPositions; i++)
            {
                x = rnd.Next(x / 2, x + 5);
                z = rnd.Next(z / 2, z + 5);
                var y = heightmapData[Math.Abs(x), Math.Abs(z)];
                positions.Add(new Vector3(x, y - 240, -z));
                x += 100;
                z += 100;
                Console.WriteLine(x.ToString() + " " + z.ToString());
            }

            return positions;
        }

        public void DrawModels(List<Models> models)
        {

            foreach (CameraComponent cameraComponent in ComponentManager.Get.GetComponents<CameraComponent>().Values)
            {
                foreach (Models model in models)
                {
                    model.Draw(cameraComponent.View, cameraComponent.Projection);
                }
            }
        }

        public void UpdateModels(List<Models> models, GameTime gameTime)
        {

            foreach (CameraComponent cameraComponent in ComponentManager.Get.GetComponents<CameraComponent>().Values)
            {
                foreach (Models model in models)
                {
                    model.Update(gameTime);
                }
            }
        }
    }
}
