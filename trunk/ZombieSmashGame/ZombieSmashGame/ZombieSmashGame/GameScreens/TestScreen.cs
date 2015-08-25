using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieSmashGame.GameScreens
{
    class TestScreen : GameScreen
    {

        SpriteFont font;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestScreen(GameCore core) : base(core)
        {
           
        }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public override void LoadContent()
        {
            font = m_core.Content.Load<SpriteFont>("gamefont");
            spriteBatch = new SpriteBatch(m_core.GraphicsDevice);
        }

        /// <summary>
        /// Unload content for the screen.
        /// </summary>
        public override void UnloadContent()
        {
            font = null;
        }

        /// <summary>
        /// Render everything in the state
        /// </summary>
        public override void Render(GameTime gameTime)
        {
            m_core.GraphicsDevice.Clear(Color.BlanchedAlmond);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Cannon power: 100", new Vector2(20, 45), Color.White);
            spriteBatch.End();
        }

        /// <summary>
        /// Update everything in the state
        /// </summary>
        public override void Update(GameTime gameTime) { }

    }
}
