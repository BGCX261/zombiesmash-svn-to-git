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
    class Enemy
    {
        // Set the avatar position and rotation variables.
        public Vector3 enemyPosition = new Vector3(0, -3.7f, 0);
        public Vector3 enemyHeadOffset = new Vector3(0, 10, 0);
        public float enemyYaw;

        // Set rates in world units per 1/60th second (the default fixed-step interval).
        public float rotationSpeed = 1f / 60f;
        //float forwardSpeed = 500f / 60f;
        public float forwardSpeed = 50f / 60f;

        float old_z;
        float old_x;

        int lastMove = 0;
        int moveCount = 0;

        Model model;

        Level level;

        Random rnd;
        BoundingSphere bsenemy;

        enum CollisionType { None, Building, Boundary, Target, Enemy }

        public Enemy(Model m,Vector3 pos)
        {
            model = m;
            enemyPosition = pos;
            rnd = RandomGenerator.getRandom();
        }

        public void setLevel(Level l)
        {
            level = l;
        }

        public Model getModel()
        {
            return model;
        }

        public float getEnemyYaw()
        {
            return enemyYaw;
        }
        public Vector3 getEnemyPosition()
        {
            return enemyPosition;
        }
        public BoundingSphere getEnemyBounding()
        {
            return bsenemy;
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
            /*
            BoundingSphere[] bs = level.getBoundingEnemy();
            for (int i = 0; i < bs.Length; i++)
            {
                if (bs[i].Contains(sphere) != ContainmentType.Disjoint)
                {
                    //this.removeLives(1);
                    return CollisionType.Enemy;
                }
            }

            */


            if (level.getBounding().Contains(sphere) != ContainmentType.Contains)
            {
                return CollisionType.Boundary;
            }
            return CollisionType.None;
        }

        public void UpdateEnemyPosition()
        {
            Vector3 v = new Vector3(enemyPosition.X, enemyPosition.Y, enemyPosition.Z);
            bsenemy = new BoundingSphere(v, 1.0f);

            if (CheckCollision(bsenemy) == CollisionType.None)
            {
                old_z = enemyPosition.Z;
                old_x = enemyPosition.X;
            }

            

            int RandomNumber = rnd.Next(10);

            if (lastMove == 0 & moveCount < 50)
            {
                enemyYaw += rotationSpeed;
                moveCount++;
                return;
            }
            if (lastMove == 1 & moveCount < 50)
            {
                enemyYaw -= rotationSpeed;
                moveCount++;
                return;
            }
            if (lastMove == 2 & moveCount < 100)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(enemyYaw);
                Vector3 v2 = new Vector3(0, 0, forwardSpeed / 10);
                v2 = Vector3.Transform(v2, forwardMovement);
                enemyPosition.Z -= v2.Z;
                enemyPosition.X -= v2.X;
                moveCount++;
                if (CheckCollision(bsenemy) != CollisionType.None)
                {
                    enemyPosition.Z = old_z;
                    enemyPosition.X = old_x;
                }
                return;
            }
            moveCount = 0;
            if (RandomNumber == 3)
            {
                lastMove = 0;
                return;
            }
            if (RandomNumber == 6)
            {
                lastMove = 1;
                return;
            }
            lastMove = 2;


        }
    }
}
