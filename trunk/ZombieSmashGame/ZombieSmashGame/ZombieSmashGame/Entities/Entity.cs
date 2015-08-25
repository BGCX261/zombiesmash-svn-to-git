using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZombieSmashGame.Util;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;

namespace ZombieSmashGame.Entities
{

    /// <summary>
    /// Every moving object in the Game World
    /// </summary>
    public class Entity : GameObject
    {
        public enum CollisionType { None, Building, Boundary, Target, Enemy }
        // Set the avatar position and rotation variables.
        protected Vector3 m_headoffset = new Vector3(0, 10, 0);
        protected float m_yaw = 0.0f;

        protected AnimationPlayer m_anim;
        protected CollisionType m_collision;

        

        //protected BoundingSphere m_sphere;

        public Vector3 HeadOffset
        {
            get { return m_headoffset; }
            set { m_headoffset = value; }
        }
        
        public float Yaw
        {
            get { return m_yaw; }
            set { m_yaw = value; }
        }

        public AnimationPlayer Anim
        {
            get { return m_anim; }
            set { m_anim = value; }
        }

        public CollisionType Collision
        {
            get { return m_collision; }
            set { m_collision = value; }
        }
        

        public BoundingSphere Sphere
        {
            get { return m_sphere; }
            set { m_sphere = value; }
        }

        // Set rates in world units per 1/60th second (the default fixed-step interval).
        protected float m_rotationspeed = 1f / 60f;
        //float forwardSpeed = 500f / 60f;
        protected float m_forwardspeed = 50f / 60f;

        protected float m_oldz;
        protected float m_oldx;

        public Entity()
            : base()
        {
            m_position = new Vector3(0, -3.7f, 0);
        }

        public Entity(Model model)
            : base()
        {
            m_model = model;
        }

        public override void Update()
        {

        }

        public override void Render()
        {

        }

        public virtual void CollidedWith(GameObject obj)
        {

        }
    }
}
