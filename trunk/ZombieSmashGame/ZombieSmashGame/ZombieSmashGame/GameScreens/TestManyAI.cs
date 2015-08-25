using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ZombieSmashGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SkinnedModel;

namespace ZombieSmashGame.GameScreens
{
    class TestManyAI : GameScreen
    {
        SpriteFont font;
        SpriteBatch spriteBatch;

        GameObjectManager m_manager;

        CollisionManager man;

        Player m_player;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestManyAI(GameCore core)
            : base(core)
        {

        }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public override void LoadContent()
        {
            m_manager = new GameObjectManager();

            m_player = new Player(m_core.Content.Load<Model>("player/soldier2"));

            m_manager.AddEntity(m_player);


            man = new CollisionManager();
            m_player.Manager = man;


            for (int i = 0; i < 10; i++)
            {
                Enemy e = new Enemy(m_core.Content.Load<Model>("player/zombie_bones"));
                SkinningData skinningData = e.Model.Tag as SkinningData;
                if (skinningData == null)
                    throw new InvalidOperationException
                        ("This model does not contain a SkinningData tag.");
                e.Anim = new AnimationPlayer(skinningData);


                AnimationClip clip = skinningData.AnimationClips["Animace"];

                e.Anim.StartClip(clip);

                m_manager.AddEntity(e);
            }

            GameCore.Camera.Player = (Player)m_manager.GetObject("ZombieSmashGame.Entities.Player");

            spriteBatch = new SpriteBatch(m_core.GraphicsDevice);


            font = m_core.Content.Load<SpriteFont>("gamefont");
        }

        /// <summary>
        /// Unload content for the screen.
        /// </summary>
        public override void UnloadContent()
        {
            font = null;
            m_manager.RemoveAllObjects();
            m_manager = null;

        }

        /// <summary>
        /// Render everything in the state
        /// </summary>
        public override void Render(GameTime gameTime)
        {
            m_core.GraphicsDevice.Clear(Color.BlanchedAlmond);

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) == true)
            {
                Enemy e = new Enemy(m_core.Content.Load<Model>("player/zombie_bones"));
                SkinningData skinningData = e.Model.Tag as SkinningData;
                if (skinningData == null)
                    throw new InvalidOperationException
                        ("This model does not contain a SkinningData tag.");
                e.Anim = new AnimationPlayer(skinningData);


                AnimationClip clip = skinningData.AnimationClips["Animace"];

                e.Anim.StartClip(clip);

                m_manager.AddEntity(e);
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.B) == true)
            {
                m_manager.RemoveObjects("ZombieSmashGame.Entities.Enemy");
            }

            DepthStencilState state = new DepthStencilState();
            state.DepthBufferEnable = true;
            m_core.GraphicsDevice.DepthStencilState = state;


            switch (GameCore.Camera.getCameraState())
            {
                default:
                case 0:
                    GameCore.Camera.UpdateCamera(m_core.GraphicsDevice.Viewport);
                    break;
                case 1:
                    GameCore.Camera.UpdateCameraFirstPerson(m_core.GraphicsDevice.Viewport);
                    break;
                case 2:
                    GameCore.Camera.UpdateCameraThirdPerson(m_core.GraphicsDevice.Viewport);
                    break;
            }

            m_manager.Render();

            state = null;
            state = new DepthStencilState();
            state.DepthBufferEnable = false;
            m_core.GraphicsDevice.DepthStencilState = state;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "V to add enemy", new Vector2(20, 45), Color.Red);
            spriteBatch.DrawString(font, "B to to remove all enemies", new Vector2(20, 65), Color.Red);
            spriteBatch.DrawString(font, "PlayerX: " + m_manager.GetObject("ZombieSmashGame.Entities.Player").Position.X, new Vector2(20, 85), Color.Red);
            spriteBatch.DrawString(font, "PlayerZ: " + m_manager.GetObject("ZombieSmashGame.Entities.Player").Position.Z, new Vector2(20, 105), Color.Red);
            spriteBatch.End();
        }

        /// <summary>
        /// Update everything in the state
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            m_manager.Update();
            m_manager.UpdateEnemies(m_manager.GetObject("ZombieSmashGame.Entities.Player"),gameTime);
        }
    }
}
