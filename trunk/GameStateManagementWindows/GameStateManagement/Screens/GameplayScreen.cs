#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        //HUD hud;
        //HUD2 hud2; 

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);        

        Random random = new Random();

        bool ishud = false;

        Player player;
        Camera camera;
        Level level;
        
        Model model_player;
        Model model_zombie;
        Model model,model2;

        Texture2D heart;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
                        
        }

        public void init()
        {
            this.player = new Player();
            this.camera = new Camera(player);
            this.level = new Level(0,camera,model,model2,model_zombie);
            player.setLevel(level);
            level.SetUpBoundingBoxes();

            hud2 = new HUD2(player, gameFont, heart,ScreenManager.SpriteBatch);
        }
        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            model_player = content.Load<Model>("players_models/soldier2");
            model_zombie = content.Load<Model>("players_models/zombie");
            model = content.Load<Model>("other_models/model2");
            model2 = content.Load<Model>("other_models/model3");

            heart = content.Load<Texture2D>("HUD/heart");

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
            init();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //if (!ishud)
            //{
              //  ScreenManager.AddScreen(hud = new HUD(player), ControllingPlayer);
               // ishud = true;
            //}

            if (IsActive)
            {
                if (player.getLives() <= 0)
                {
                    this.ExitScreen();
                    ScreenManager.AddScreen(new GameOverScreen(), null);
                    //ScreenManager.AddScreen(new MainMenuScreen(),null);
                }
                level.updateLevel();
                camera.GetCurrentCamera();
                player.updatePosition();
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            


            // Our player and enemy are both actually just text strings.
            switch (camera.getCameraState())
            {
                default:
                case 0:
                    camera.UpdateCamera(ScreenManager.GraphicsDevice.Viewport);                    
                    break;
                case 1:
                    camera.UpdateCameraFirstPerson(ScreenManager.GraphicsDevice.Viewport);
                    break;
                case 2:
                    camera.UpdateCameraThirdPerson(ScreenManager.GraphicsDevice.Viewport);
                    break;
            }
            level.drawLevel();
            
            drawAvatar();

            hud2.draw();
            //ScreenManager.GraphicsDevice.RenderState.DepthBufferEnable = true; // xna 3 verze
           // ScreenManager.GraphicsDevice.DepthStencilState.DepthBufferEnable = true;

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
            //base.Draw(gameTime);
        }

        public void drawAvatar()
        {
            Matrix World = Matrix.CreateScale(0.2f) *
                //Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
            Matrix.CreateRotationY(player.avatarYaw) *
            Matrix.CreateTranslation(player.avatarPosition);
            if (camera.getCameraState() == 2)
            {
                DrawModel(model_player, World);
            }
        }

        public void DrawModel(Model model, Matrix world)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.Projection = camera.getProj();
                    be.View = camera.getView();
                    be.World = world;
                    be.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }

        


        #endregion
    }
}
