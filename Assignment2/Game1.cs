using _3D_Rendering_and_Transformation.Systems;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _3D_Rendering_and_Transformation.Components;

namespace Assignment2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private HeightMapSystem heightMapSystem;
        private CameraSystem cameraSystem;

        Texture2D heightMap;
        Effect effect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            heightMapSystem = new HeightMapSystem();
            cameraSystem = new CameraSystem();
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            heightMap = Content.Load<Texture2D>("US_Canyon");
            effect = Content.Load<Effect>("effects");

            CreateEntities();
            cameraSystem.Initialize(graphics);
            heightMapSystem.LoadHeightData(heightMap);
            heightMapSystem.SetUpVertices();
            //  heightMapSystem.SetupVertexBuffer(this);
            heightMapSystem.SetUpIndices();
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

            // TODO: Add your update logic here

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
            heightMapSystem.Draw(gameTime, graphics.GraphicsDevice);
            base.Draw(gameTime);
        }

        private void CreateEntities()
        {
            var id = ComponentManager.Get.NewEntity();

            ComponentManager.Get.AddComponentsToEntity(new CameraComponent() { }, id);
            //ComponentManager.Get.AddComponentsToEntity(new TransformComponent() { Position = position, Axis = axis }, id);
            //ComponentManager.Get.AddComponentsToEntity(new ModelComponent() { Model = model1 }, id);
            ComponentManager.Get.AddComponentsToEntity(new HeightMapComponent() { HeightMap = heightMap, Effect = effect, Width = 1000, Height = 500 }, id);

        }
    }
}
