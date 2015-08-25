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
    public class CollisionManager
    {
        static List<GameObject> m_bounds = new List<GameObject>();

        public int m_count;

        /// <summary>
        /// List of all entities in the game
        /// </summary>
        public List<GameObject> Bounds
        {
            get { return m_bounds; }
            set { m_bounds = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CollisionManager()
        {
            m_count = 0;
        }

        /// <summary>
        /// Adds entity to entity list
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        public void AddEntity(GameObject entity)
        {
            m_bounds.Add(entity);
            m_count++;
        }

        public void RemoveAllObjects()
        {
            for (int z = 0; z < m_bounds.Count; z++)
            {
                //m_bounds[z].Dispose();
                m_bounds.RemoveAt(z);
                m_count--;
            }
            m_bounds.Clear();
        }

        public void Update(Entity entity)
        {
            for (int z = 0; z < m_bounds.Count; z++)
            {
                //m_bounds[z].Dispose();
                //entity.CollidedWith(m_bounds[z]);
                if (m_bounds[z].BoundingBox.Intersects(entity.Sphere))
                {
                    entity.Collision = Entity.CollisionType.Building;
                }
                else
                {
                    entity.Collision = Entity.CollisionType.None;
                }

                //m_bounds.RemoveAt(z);
                m_count--;
            }
        }
    }
}
