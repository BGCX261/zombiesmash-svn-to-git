using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZombieSmashGame.Entities;
using ZombieSmashGame.Util;

namespace ZombieSmashGame.GameScreens
{
    class TestAIScreen : GameScreen
    {

        short[] index;
        BasicEffect lineShader; 
        VertexPositionColor[] points;


        SpriteFont font;
        SpriteBatch spriteBatch;

        Player m_player;

        Enemy m_enemy;

        Building m_building1, m_building2, m_building3, 
            m_building4, m_building5, m_building6, 
            m_building7, m_building8, m_building9,
            m_building10, m_building11, m_building12;

        Floor m_floor;

        Car m_car;

        Tree m_tree;

        RoadBlock m_block;

        CollisionManager coll_manager;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestAIScreen(GameCore core) : base(core)
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
            m_car = new Car();
            m_tree = new Tree();
            m_building1 = new Building();
            m_building2 = new Building();
            m_building3 = new Building();
            m_building4 = new Building();
            m_building5 = new Building();
            m_building6 = new Building();
            m_building7 = new Building();
            m_building8 = new Building();
            m_building9 = new Building();
            m_building10 = new Building();
            m_building11 = new Building();
            m_building12 = new Building();

            coll_manager = new CollisionManager();

            m_block = new RoadBlock();

            GameCore.Camera.Player = m_player;
            
            font = m_core.Content.Load<SpriteFont>("gamefont");
            m_player.Model = m_core.Content.Load<Model>("player/soldier2");
            m_floor.Model = m_core.Content.Load<Model>("other/model2");
            m_enemy.Model = m_core.Content.Load<Model>("player/zombie");

            m_car.Model = m_core.Content.Load<Model>("other/skoda");
            m_tree.Model = m_core.Content.Load<Model>("other/strom");
            m_block.Model = m_core.Content.Load<Model>("other/zataras");
            
            m_building1.Model = m_core.Content.Load<Model>("other/budova_skola/blok_a");
            m_building2.Model = m_core.Content.Load<Model>("other/budova_skola/blok_a");
            m_building3.Model = m_core.Content.Load<Model>("other/budova_skola/blok_a");
            m_building4.Model = m_core.Content.Load<Model>("other/budova_skola/blok_a");
            m_building5.Model = m_core.Content.Load<Model>("other/budova_skola/blok_b");
            m_building6.Model = m_core.Content.Load<Model>("other/budova_skola/blok_c");
            m_building7.Model = m_core.Content.Load<Model>("other/budova_skola/blok_c");
            m_building8.Model = m_core.Content.Load<Model>("other/budova_skola/blok_c");
            m_building9.Model = m_core.Content.Load<Model>("other/budova_skola/blok_vchod_a");
            m_building10.Model = m_core.Content.Load<Model>("other/budova_skola/blok_vchod_b");
            m_building11.Model = m_core.Content.Load<Model>("other/budova_skola/blok_vchod_a");
            m_building12.Model = m_core.Content.Load<Model>("other/budova_skola/blok_podstava");


            m_car.Position = new Vector3(0, -6, -20);
            m_car.setBoundingBox();
            coll_manager.AddEntity(m_car);

            m_block.Position = new Vector3(20, -6, -20);
            m_block.setBoundingBox();
            coll_manager.AddEntity(m_block);

            m_tree.Position = new Vector3(20, 5, 0);
            m_tree.setBoundingBox();
            coll_manager.AddEntity(m_tree);

            m_building1.Position = new Vector3(130, 4, 90);
            m_building2.Position = new Vector3(-30, 4, 90);
            m_building3.Position = new Vector3(-190, 4, 90);
            m_building4.Position = new Vector3(290, 4, 90);
            m_building5.Position = new Vector3(-200, 4, 100);
            m_building6.Position = new Vector3(-130, 4, 140);
            m_building7.Position = new Vector3(30, 4, 140);
            m_building8.Position = new Vector3(190, 4, 140);
            m_building9.Position = new Vector3(-130, 4, 80);
            m_building10.Position = new Vector3(30, 4, 80);
            m_building11.Position = new Vector3(190, 4, 80);
            m_building12.Position = new Vector3(250, -5, 30);

            m_building1.setBoundingBox();
            m_building2.setBoundingBox();
            m_building3.setBoundingBox();
            m_building4.setBoundingBox();
            m_building5.setBoundingBox();
            m_building6.setBoundingBox();
            m_building7.setBoundingBox();
            m_building8.setBoundingBox();
            m_building9.setBoundingBox();
            m_building10.setBoundingBox();
            m_building11.setBoundingBox();

            coll_manager.AddEntity(m_building1);
            coll_manager.AddEntity(m_building2);
            coll_manager.AddEntity(m_building3);
            coll_manager.AddEntity(m_building4);
            coll_manager.AddEntity(m_building5);
            coll_manager.AddEntity(m_building6);
            coll_manager.AddEntity(m_building7);
            coll_manager.AddEntity(m_building8);
            coll_manager.AddEntity(m_building9);
            coll_manager.AddEntity(m_building10);

            m_player.Manager = coll_manager;
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
            m_car.Render();
            
            m_building1.Render();
            m_building2.Render();
            m_building3.Render();
            m_building4.Render();
            m_building5.Render();
            m_building6.Render();
            m_building7.Render();
            m_building8.Render();
            m_building9.Render();
            m_building10.Render();
            m_building11.Render();
            m_building12.Render();

            
            m_block.Render();
            //m_tree.Render();           
            //DrawBox(m_building1.BoundingBox);
            //DrawBox(m_building2.BoundingBox);
            //DrawBox(m_building3.BoundingBox);
            //DrawBox(m_building4.BoundingBox);
            //DrawBox(m_building5.BoundingBox);
            //DrawBox(m_building6.BoundingBox);
            //DrawBox(m_building7.BoundingBox);
            //DrawBox(m_building8.BoundingBox);
            //DrawBox(m_building9.BoundingBox);
            //DrawBox(m_building10.BoundingBox);
            //DrawBox(m_building11.BoundingBox);

            //DrawBox(m_car.BoundingBox);
            //DrawBox(m_tree.BoundingBox);
            //DrawBox(m_block.BoundingBox);

            state = null;
            state = new DepthStencilState();
            state.DepthBufferEnable = false;
            m_core.GraphicsDevice.DepthStencilState = state;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "...Test AI... "+coll_manager.m_count, new Vector2(10, 10), Color.Blue);
            spriteBatch.End();
        }

        /// <summary>
        /// Update everything in the state
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            //m_player.CollidedWith(m_car);
            //coll_manager.Update(m_player);
            //m_player.CollidedWith(m_car);
            //m_player.CollidedWith(m_block);
            
            m_player.Update();
            m_enemy.Update(m_player);            
        }
       

        protected void DrawBox(BoundingBox box)
        {
            BuildBox(box, Color.Red);
            DrawLines(12);
        }
        protected void BuildBox(BoundingBox box, Color lineColor)
        {
            points = new VertexPositionColor[8];

            Vector3[] corners = box.GetCorners();

            points[0] = new VertexPositionColor(corners[1], lineColor); // Front Top Right
            points[1] = new VertexPositionColor(corners[0], lineColor); // Front Top Left
            points[2] = new VertexPositionColor(corners[2], lineColor); // Front Bottom Right
            points[3] = new VertexPositionColor(corners[3], lineColor); // Front Bottom Left
            points[4] = new VertexPositionColor(corners[5], lineColor); // Back Top Right
            points[5] = new VertexPositionColor(corners[4], lineColor); // Back Top Left
            points[6] = new VertexPositionColor(corners[6], lineColor); // Back Bottom Right
            points[7] = new VertexPositionColor(corners[7], lineColor); // Bakc Bottom Left

            index = new short[] {
	            0, 1, 0, 2, 1, 3, 2, 3,
	            4, 5, 4, 6, 5, 7, 6, 7,
	            0, 4, 1, 5, 2, 6, 3, 7
                };
        }
        protected void DrawLines(int primativeCount)
        {
            if (lineShader == null)
                lineShader = new BasicEffect(m_core.GraphicsDevice);

            lineShader.World = Matrix.Identity;
            lineShader.View = GameCore.Camera.getView();
            lineShader.Projection = GameCore.Camera.getProj();
            lineShader.VertexColorEnabled = true;

            lineShader.CurrentTechnique.Passes[0].Apply();
            m_core.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, points, 0, points.Length, index, 0, primativeCount);
        }
    }
}
