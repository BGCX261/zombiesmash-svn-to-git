using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace GameStateManagement
{
    class Player
    {
        // Set the avatar position and rotation variables.
        public Vector3 avatarPosition = new Vector3(-10, -3.7f, 0);
        public Vector3 avatarHeadOffset = new Vector3(0, 10, 0);
        public float avatarYaw;

        // Set the direction the camera points without rotation.
        public Vector3 cameraReference = new Vector3(0, 0, 10);

        public Vector3 thirdPersonReference = new Vector3(0, 40, -30);

        // Set rates in world units per 1/60th second (the default fixed-step interval).
        public float rotationSpeed = 1f / 60f;
        //float forwardSpeed = 500f / 60f;
        public float forwardSpeed = 50f / 60f;

        float old_z;
        float old_x;

        enum CollisionType { None, Building, Boundary, Target, Enemy }        

        int lives = 100;

        Level level;

        BoundingSphere bsplayer;

        public Player()
        {

        }

        public void setLevel(Level l)
        {
            level = l;
        }

        public BoundingSphere getBounding()
        {
            return bsplayer;
        }

        public int getLives(){
            return lives;
        }
            

        public void setLives(int lives){
            this.lives = lives;
        }

        public void removeLives(int remove)
        {
            this.lives -= remove;
        }

        private CollisionType CheckCollision(BoundingSphere sphere)
        {
            BoundingBox[] bb = level.getBoundingBuildings();
            for (int i = 0; i < bb.Length; i++)
            {
                if (bb[i].Contains(sphere) != ContainmentType.Disjoint)
                {                    
                    return CollisionType.Building;
                }
            }
            BoundingSphere[] bs = level.getBoundingEnemy();
            for (int i = 0; i < bs.Length; i++)
            {
                if (bs[i].Contains(sphere) != ContainmentType.Disjoint)
                {
                    this.removeLives(1);
                    return CollisionType.Enemy;
                }
            }
            
            if (level.getBounding().Contains(sphere) != ContainmentType.Contains)
            {
                return CollisionType.Boundary;
            }
            return CollisionType.None;
        }

        public void updatePosition()
        {
            bsplayer = new BoundingSphere(avatarPosition, 1.0f);

            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            if (CheckCollision(bsplayer) == CollisionType.None)
            {
                old_z = avatarPosition.Z;
                old_x = avatarPosition.X;
            }
            
            if (keyboardState.IsKeyDown(Keys.PageUp))
            {
                thirdPersonReference.Y += 0.5f;
                thirdPersonReference.Z -= 0.5f;
            }
            if (keyboardState.IsKeyDown(Keys.PageDown))
            {
                thirdPersonReference.Y -= 0.5f;
                thirdPersonReference.Z += 0.5f;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || (currentState.DPad.Left == ButtonState.Pressed))
            {
                // Rotate left.
                avatarYaw += rotationSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || (currentState.DPad.Right == ButtonState.Pressed))
            {
                // Rotate right.
                avatarYaw -= rotationSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || (currentState.DPad.Up == ButtonState.Pressed))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(avatarYaw);
                Vector3 v = new Vector3(0, 0, forwardSpeed);
                v = Vector3.Transform(v, forwardMovement);
                avatarPosition.Z += v.Z;
                avatarPosition.X += v.X;            
            }
            if (keyboardState.IsKeyDown(Keys.Down) || (currentState.DPad.Down == ButtonState.Pressed))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(avatarYaw);
                Vector3 v = new Vector3(0, 0, -forwardSpeed);
                v = Vector3.Transform(v, forwardMovement);
                avatarPosition.Z += v.Z;
                avatarPosition.X += v.X;            
            }


            if (CheckCollision(bsplayer) != CollisionType.None)
            {
                avatarPosition.Z = old_z;
                avatarPosition.X = old_x;            
            }
            
    
        }
    }
}
