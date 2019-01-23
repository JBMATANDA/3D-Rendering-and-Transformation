using _3D_Rendering_and_Transformation.Systems;
using _3D_Rendering_and_Transformation.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _3D_Rendering_and_Transformation.Components;

namespace Chopper_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        private CameraSystem cameraSystem;
        private RenderModelSystem renderModelSystem;
        private TransformSystem transformSystem;
        private HeightMapSystem heightMapSystem;

        Texture2D heightMap;
        Model model1;
        
        Effect effect;
        Vector3 position;
       // Quaternion rotation;
        Vector3 axis;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
           
            position = Vector3.Zero;
            //rotation = new Quaternion();
            axis = Vector3.Zero;

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            model1 = Content.Load<Model>("Chopper");
            
            heightMap = Content.Load<Texture2D>("US_Canyon");
            effect = Content.Load<Effect>("effects");

            CreateEntities();

            heightMapSystem.LoadHeightData(heightMap);
            heightMapSystem.SetUpVertices();
            //heightMapSystem.SetupVertexBuffer(this);
            heightMapSystem.SetUpIndices();
            //heightMapSystem.SetupIndexBuffer(this);
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

            // TODO: Add your update logic here
            cameraSystem.Update(graphics, gameTime);
            transformSystem.Update(gameTime);
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
            heightMapSystem.Draw(gameTime, graphics.GraphicsDevice);
            base.Draw(gameTime);
        }
        private void ResetGame()
        {
            ComponentManager.Get.ClearComponents();
            cameraSystem = new CameraSystem();
            renderModelSystem = new RenderModelSystem();
            heightMapSystem = new HeightMapSystem();
            CreateEntities();
        }
        private void CreateEntities()
        {
            var id = ComponentManager.Get.NewEntity();
            
            ComponentManager.Get.AddComponentsToEntity(new CameraComponent() { CamPosition = new Vector3(0,0,20f), CamTarget = new Vector3(0,0,0) }, id);
            ComponentManager.Get.AddComponentsToEntity(new TransformComponent() { Position = position, Axis = axis  }, id);
            ComponentManager.Get.AddComponentsToEntity(new ModelComponent() { Model = model1}, id);
            ComponentManager.Get.AddComponentsToEntity(new HeightMapComponent() { HeightMap = heightMap, Effect = effect, Width = 1000, Height = 500 }, id);

        }
    }
}
