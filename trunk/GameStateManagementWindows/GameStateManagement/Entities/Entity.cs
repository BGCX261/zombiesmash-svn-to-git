using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement.Util;

namespace GameStateManagement.Entities
{
    class Entity : IDisposable
    {

        private int id;
        private String name;
        
        public int ID
        {
            get { return id; }
        }

 
        public String Name
        {
            get { return name; }
        }

        // Set the avatar position and rotation variables.
        protected Vector3 entityPosition = new Vector3(0, -3.7f, 0);
        protected Vector3 entityHeadOffset = new Vector3(0, 10, 0);
        protected float entityYaw;

        // Set rates in world units per 1/60th second (the default fixed-step interval).
        protected float rotationSpeed = 1f / 60f;
        //float forwardSpeed = 500f / 60f;
        protected float forwardSpeed = 50f / 60f;

        protected float old_z;
        protected float old_x;

        protected int lastMove = 0;
        protected int moveCount = 0;

        protected Model model;

        protected Level level;

        protected Random rnd;
        protected BoundingSphere bsentity;


        Entity()
        {
            id = GameCore.Unique_ID; // this will be uniq for every entity -> it helps to identify it
            name = this.ToString(); // name corresponds to a collection of object, so in the EntityManager we can update all Enemies,
            //Players or everything what is an entity
        }

        private Utils.CollisionType CheckCollision(BoundingSphere sphere)
        {
            return Utils.CollisionType.None;
        }


        public void Update()
        {
 
        }

        public void Render()
        {
        }

        public void Dispose()
        {
            model = null;
            level = null;
            rnd = null;
        }
    }
}
