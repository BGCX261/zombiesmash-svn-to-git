using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ZombieSmashGame.GameScreens;
using ZombieSmashGame.Util;
using ZombieSmashGame.Entities;

namespace ZombieSmashGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameCore : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static Camera m_camera;

        static int unique_id = 0 ;

        public static Camera Camera
        {
            get { return GameCore.m_camera; }
            set { GameCore.m_camera = value; }
        }

        public static int Unique_ID
        {
            get { return GameCore.unique_id ++; }
        }

        public GameCore()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            m_camera = new Camera();

            ScreenManager.AddScreen(new TestScreen(this), "test");
            ScreenManager.AddScreen(new TestAIScreen(this), "TestAI");
            ScreenManager.AddScreen(new TestAIAnimScreen(this), "TestAIAnim");
            ScreenManager.AddScreen(new TestManyAI(this), "TestManyAI");

            ScreenManager.SwitchScreen("TestAI");

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

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) == true)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.E) == true)
            {
                ScreenManager.SwitchScreen("TestAI");
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A) == true)
            {
                ScreenManager.SwitchScreen("TestAIAnim");
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.R) == true)
            {
                ScreenManager.SwitchScreen("test");
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.T) == true)
            {
                ScreenManager.SwitchScreen("TestManyAI");
            }

            ScreenManager.UpdateScreens(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            ScreenManager.RenderScreens(gameTime);

            base.Draw(gameTime);
        }
    }
}
