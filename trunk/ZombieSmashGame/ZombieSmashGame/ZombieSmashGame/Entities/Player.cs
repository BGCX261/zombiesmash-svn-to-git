using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZombieSmashGame.Util;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieSmashGame.Entities
{
    public class Player : Entity 
    {

        // Set the direction the camera points without rotation.
        private Vector3 m_camerareference = new Vector3(0, 0, 10);
        private Vector3 m_thirdpersonreference = new Vector3(0, 15, -10);

        private CollisionManager m_manager;
        public CollisionManager Manager
        {
            get { return m_manager; }
            set { m_manager = value; }
        }

        float old_x,old_z;

        public Vector3 CameraReference
        {
            get { return m_camerareference; }
            set { m_camerareference = value; }
        }

        public Vector3 ThirdPersonReference
        {
            get { return m_thirdpersonreference; }
            set { m_thirdpersonreference = value; }
        }

        int health = 100;

        public Player()
            : base()
        {
            
        }

        public Player(Model model)
            : base(model)
        {

        }
        public void Update(GameTime gameTime)
        {
            m_sphere = new BoundingSphere(m_position, 1.0f);

            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            /*
            if (CheckCollision(bsplayer) == CollisionType.None)
            {
                old_z = avatarPosition.Z;
                old_x = avatarPosition.X;
            }
             * */

            if (m_collision == CollisionType.None)
            {
                old_z = m_position.Z;
                old_x = m_position.X;
            }
            if (m_collision != CollisionType.None)
            {
                m_position.Z = old_z;
                m_position.X = old_x;
            }

            if (keyboardState.IsKeyDown(Keys.PageUp))
            {
                m_thirdpersonreference.Y += 0.5f;
                m_thirdpersonreference.Z -= 0.5f;
            }
            if (keyboardState.IsKeyDown(Keys.PageDown))
            {
                m_thirdpersonreference.Y -= 0.5f;
                m_thirdpersonreference.Z += 0.5f;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || (currentState.DPad.Left == ButtonState.Pressed))
            {
                // Rotate left.
                m_yaw += m_rotationspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || (currentState.DPad.Right == ButtonState.Pressed))
            {
                // Rotate right.
                m_yaw -= m_rotationspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || (currentState.DPad.Up == ButtonState.Pressed))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(m_yaw);
                Vector3 v = new Vector3(0, 0, m_forwardspeed);
                v = Vector3.Transform(v, forwardMovement);
                m_position.Z += v.Z;
                m_position.X += v.X;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || (currentState.DPad.Down == ButtonState.Pressed))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(m_yaw);
                Vector3 v = new Vector3(0, 0, -m_forwardspeed);
                v = Vector3.Transform(v, forwardMovement);
                m_position.Z += v.Z;
                m_position.X += v.X;
            }

            /*
            if (CheckCollision(bsplayer) != CollisionType.None)
            {
                avatarPosition.Z = old_z;
                avatarPosition.X = old_x;
            }
             * */
            if (m_anim != null)
            {
                m_anim.Update(gameTime.ElapsedGameTime, true, Matrix.Identity * Matrix.CreateScale(0.025f) *
                                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), 1.57f)) *
                                    //Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 3.14f)) *
                                    Matrix.CreateRotationY(m_yaw) *
                                    Matrix.CreateTranslation(m_position));
            }
        }


        public override void Update()
        {
            m_sphere = new BoundingSphere(m_position, 1.0f);

            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);


            if (CheckCollision(m_sphere) == CollisionType.None)
            {
                old_z = m_position.Z;
                old_x = m_position.X;
            }
            if (CheckCollision(m_sphere) != CollisionType.None)
            {                
                m_position.Z = old_z;
                m_position.X = old_x;
            }
            /*
            if (CheckCollision(bsplayer) == CollisionType.None)
            {
                old_z = avatarPosition.Z;
                old_x = avatarPosition.X;
            }
             * */

            if (keyboardState.IsKeyDown(Keys.PageUp))
            {
                m_thirdpersonreference.Y += 0.5f;
                m_thirdpersonreference.Z -= 0.5f;
            }
            if (keyboardState.IsKeyDown(Keys.PageDown))
            {
                m_thirdpersonreference.Y -= 0.5f;
                m_thirdpersonreference.Z += 0.5f;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || (currentState.DPad.Left == ButtonState.Pressed))
            {
                // Rotate left.
                m_yaw += m_rotationspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || (currentState.DPad.Right == ButtonState.Pressed))
            {
                // Rotate right.
                m_yaw -= m_rotationspeed;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || (currentState.DPad.Up == ButtonState.Pressed))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(m_yaw);
                Vector3 v = new Vector3(0, 0, m_forwardspeed);
                v = Vector3.Transform(v, forwardMovement);
                m_position.Z += v.Z;
                m_position.X += v.X;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || (currentState.DPad.Down == ButtonState.Pressed))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(m_yaw);
                Vector3 v = new Vector3(0, 0, -m_forwardspeed);
                v = Vector3.Transform(v, forwardMovement);
                m_position.Z += v.Z;
                m_position.X += v.X;
            }

            

            /*
            if (CheckCollision(bsplayer) != CollisionType.None)
            {
                avatarPosition.Z = old_z;
                avatarPosition.X = old_x;
            }
             * */
            
        }

        public override void Render()
        {
            

            if (m_anim != null)
            {
                Matrix World = Matrix.CreateScale(0.2f) *
                    //Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
           Matrix.CreateRotationY(m_yaw) *
           Matrix.CreateTranslation(m_position);
                if (GameCore.Camera.getCameraState() == 2)
                {
                    Utils.DrawModelAnim(Model, World,m_anim);
                }
            }
            else
            {
                Matrix World = Matrix.CreateScale(0.2f) *
                    //Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
           Matrix.CreateRotationY(m_yaw) *
           Matrix.CreateTranslation(m_position);
                if (GameCore.Camera.getCameraState() == 2)
                {
                    Utils.DrawModel(Model, World);
                }
            }
        }

        private CollisionType CheckCollision(BoundingSphere sphere)
        {
            for (int z = 0; z < m_manager.Bounds.Count; z++)
            {
                if (m_manager.Bounds[z].BoundingBox.Contains(sphere) != ContainmentType.Disjoint)
                    return CollisionType.Building;
            } 
            
           //if (completeCityBox.Contains(sphere) != ContainmentType.Contains)
             //   return CollisionType.Boundary;

            return CollisionType.None;
        }

        public override void CollidedWith(GameObject obj)
        {
            if (obj.BoundingBox.Intersects(m_sphere))
            {
                m_collision = CollisionType.Building;
            }
            else
            {
                m_collision = CollisionType.None;
            }
        }
    }
}
