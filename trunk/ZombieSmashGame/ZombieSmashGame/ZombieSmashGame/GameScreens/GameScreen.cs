using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieSmashGame.GameScreens
{
    public class GameScreen
    {

        protected GameCore m_core;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameScreen(GameCore core)
        {
            m_core = core;
        }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// Unload content for the screen.
        /// </summary>
        public virtual void UnloadContent() { }

        /// <summary>
        /// Render everything in the state
        /// </summary>
        public virtual void Render(GameTime gameTime)
        {

        }

        /// <summary>
        /// Update everything in the state
        /// </summary>
        public virtual void Update(GameTime gameTime) { }
    }
}
