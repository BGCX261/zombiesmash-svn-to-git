#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// Sample showing how to manage different game states, with transitions
    /// between menu screens, a loading screen, the game itself, and a pause
    /// menu. This main game class is extremely simple: all the interesting
    /// stuff happens in the ScreenManager component.
    /// </summary>
    public class GameCore : Microsoft.Xna.Framework.Game
    {
        private static int unique_id = 0;

        public static int Unique_ID
        {
            get { return GameCore.unique_id++; }
        }

        #region Fields

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;

        #endregion

        #region Initialization


        /// <summary>
        /// The main game constructor.
        /// </summary>
        public GameCore()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 853;
            graphics.PreferredBackBufferHeight = 480;

            //graphics.IsFullScreen = true;

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);            
            screenManager.AddScreen(new MainMenuScreen(), null);
        }


        #endregion

        #region Draw


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }

        public GraphicsDeviceManager getGDM()
        {
            return graphics;
        }

        public void setFullScreen()
        {
            graphics.IsFullScreen = true;
        }


        #endregion
    }


    #region Entry Point

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            using (GameCore game = new GameCore())
            {
                game.Run();
            }
        }
    }

    #endregion
}
