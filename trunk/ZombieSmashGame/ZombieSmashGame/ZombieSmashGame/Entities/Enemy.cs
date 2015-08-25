using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieSmashGame.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;

namespace ZombieSmashGame.Entities
{
    class Enemy : Entity
    {


        public Enemy()
            : base()
        {
        }

        public Enemy(Model model)
            : base(model)
        {

            m_position = new Vector3((float)(Utils.Random.NextDouble() * 50), 0f, (float)(Utils.Random.NextDouble() * 50));
            //m_yaw = MathHelper.ToRadians(2f);
        }

        public void Update(GameObject target, GameTime gameTime)
        {
            // set the proper rotation
            m_yaw = MathHelper.ToRadians(Utils.ComputeYaw(m_position, target.Position));
            

            //Move towards player until im close enough to attack
            if (Vector3.Distance(target.Position, m_position) > 2f)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(m_yaw);
                Vector3 v2 = new Vector3(0, 0, m_forwardspeed / 10);
                v2 = Vector3.Transform(v2, forwardMovement);
                m_position.Z -= v2.Z;
                m_position.X -= v2.X;
            }
            if (m_anim != null)
            {
                m_anim.Update(gameTime.ElapsedGameTime, true, Matrix.Identity * Matrix.CreateScale(2.7f) *
                                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
                                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 3.14f)) *
                                    Matrix.CreateRotationY(m_yaw) *
                                    Matrix.CreateTranslation(m_position));
            }
        }
        public void Update(GameObject target)
        {
            // set the proper rotation
            m_yaw = MathHelper.ToRadians(Utils.ComputeYaw(m_position, target.Position));


            //Move towards player until im close enough to attack
            if (Vector3.Distance(target.Position, m_position) > 2f)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(m_yaw);
                Vector3 v2 = new Vector3(0, 0, m_forwardspeed / 10);
                v2 = Vector3.Transform(v2, forwardMovement);
                m_position.Z -= v2.Z;
                m_position.X -= v2.X;
            }
            
        }
        public override void Render()
        {
            if (m_anim != null)
            {
                Utils.DrawModelAnim(m_model, Matrix.Identity, this.Anim);
            }
            else
            {
                Utils.DrawModel(m_model, Matrix.Identity * Matrix.CreateScale(0.2f) *
                                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 3.14f)) *
                                    Matrix.CreateRotationY(m_yaw) *
                                    Matrix.CreateTranslation(m_position));
            }
        }

        

        public virtual void CollidedWith(GameObject obj)
        {

        }
    }
}
