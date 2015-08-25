using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZombieSmashGame.Entities;
using ZombieSmashGame.Util;
using SkinnedModel;

namespace ZombieSmashGame.GameScreens
{
    class TestAIAnimScreen : GameScreen
    {
                
        SpriteFont font;
        SpriteBatch spriteBatch;

        Player m_player;

        Enemy m_enemy;

        AnimationPlayer animationPlayer;

        Building m_building;

        Floor m_floor;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestAIAnimScreen(GameCore core) : base(core)
        {
            
        }

        /// <summary>
        /// Load graphics content for the screen.
        /// </summary>
        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(m_core.GraphicsDevice);
            m_player = new Player();
            m_floor = new Floor();
            m_enemy = new Enemy();
          //  m_building = new Building();
            
            GameCore.Camera.Player = m_player;
            
            font = m_core.Content.Load<SpriteFont>("gamefont");
            //m_player.Model = m_core.Content.Load<Model>("player/soldier2");
            m_player.Model = m_core.Content.Load<Model>("player/vojak_anim");
            m_floor.Model = m_core.Content.Load<Model>("other/model2");
            //m_enemy.Model = m_core.Content.Load<Model>("player/zombie");
          //  m_building.Model = m_core.Content.Load<Model>("other/blok_1");


            // Look up our custom skinning information.
            SkinningData skinningDataPlayer = m_player.Model.Tag as SkinningData;

            if (skinningDataPlayer == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            m_player.Anim = new AnimationPlayer(skinningDataPlayer);


            AnimationClip clip = skinningDataPlayer.AnimationClips["Action"];

            m_player.Anim.StartClip(clip);


            // Load the model.
            m_enemy.Model = m_core.Content.Load<Model>("player/zombie2_bones");

            // Look up our custom skinning information.
            SkinningData skinningData = m_enemy.Model.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            //animationPlayer = new AnimationPlayer(skinningData);
            
            m_enemy.Anim = new AnimationPlayer(skinningData);
            

            AnimationClip clip2 = skinningData.AnimationClips["Animace"];

            m_enemy.Anim.StartClip(clip2);

            
        }

        /// <summary>
        /// Unload content for the screen.
        /// </summary>
        public override void UnloadContent()
        {
            font = null;
            m_player = null;
            GameCore.Camera.Player = null;
            spriteBatch = null;
            m_floor = null;
            m_enemy = null;
          //  m_building = null;
            
        }

        /// <summary>
        /// Render everything in the state
        /// </summary>
        public override void Render(GameTime gameTime)
        {
            m_core.GraphicsDevice.Clear(Color.BlanchedAlmond);

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
            
            m_floor.Render();
            m_player.Render();
            m_enemy.Render();
        //    m_building.Render();

            state = null;
            state = new DepthStencilState();
            state.DepthBufferEnable = false;
            m_core.GraphicsDevice.DepthStencilState = state;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "...Test AI Animace... ", new Vector2(10, 10), Color.Blue);
            spriteBatch.End();

            
        }

        /// <summary>
        /// Update everything in the state
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            m_player.Update(gameTime);            
            m_enemy.Update(m_player,gameTime);
            
        }
    }
}
