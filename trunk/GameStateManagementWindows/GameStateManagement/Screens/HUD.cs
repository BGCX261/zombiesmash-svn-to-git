#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion


#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    class HUD : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D heart;

        Player player;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public HUD(Player player)
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.player = player;

            //IsPopup = true;

            this.ScreenState = ScreenState.TransitionOff;
            

        }


        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            heart = content.Load<Texture2D>("HUD/heart");
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            //base.Update(gameTime, otherScreenHasFocus, false);
            
        }


        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Rectangle viewport = new Rectangle(5, 5, 40, 40);
            byte fade = TransitionAlpha;
            SpriteFont font = ScreenManager.Font;

            Vector2 fontPos = new Vector2(50, 5);

            spriteBatch.Begin();


            spriteBatch.Draw(heart, viewport,
                             new Color(fade, fade, fade));
            spriteBatch.DrawString(font, "Lives: " + player.getLives(), fontPos,
                Color.Yellow);



            spriteBatch.End();
            //ScreenManager.GraphicsDevice.RenderState.DepthBufferEnable = true; // xna 3 verze
            ScreenManager.GraphicsDevice.DepthStencilState.DepthBufferEnable = true;
        }


        #endregion
    }
}
