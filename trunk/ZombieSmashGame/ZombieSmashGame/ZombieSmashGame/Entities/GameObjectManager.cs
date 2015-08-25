using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieSmashGame.Entities
{
    class GameObjectManager
    {
        //List of all entities in the game
        static List<GameObject> m_objects = new List<GameObject>();

        static int m_count;

        /// <summary>
        /// List of all entities in the game
        /// </summary>
        public static List<GameObject> Entities
        {
            get { return m_objects; }
            set { m_objects = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GameObjectManager()
        {
            m_count = 0;
        }

        /// <summary>
        /// Adds entity to entity list
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        public void AddEntity(GameObject entity)
        {
            m_objects.Add(entity);
            m_count++;
        }

        public void RemoveAllObjects()
        {
            for (int z = 0; z < m_objects.Count; z++)
            {
                m_objects[z].Dispose();
                m_objects.RemoveAt(z);
                m_count--;
            }
             m_objects.Clear();
        }

        /// <summary>
        /// Updates all entities
        /// </summary>
        public void Update()
        {
            /*
			foreach(GameObject entity in m_objects)
				entity.UpdateEntity();
             * */

            for (int z = 0; z < m_objects.Count; z++)
            {
                if (m_objects[z] != null)
                    m_objects[z].Update();
            }
        }


        public void UpdateEnemies(GameObject target)
        {
            foreach (GameObject en in m_objects)
            {
                if (en is Enemy)
                {
                    Enemy m = (Enemy)en;
                    m.Update(target);
                }
                  
                
            }
        }

        public void UpdateEnemies(GameObject target, GameTime gameTime)
        {
            foreach (GameObject en in m_objects)
            {
                if (en is Enemy)
                {
                    Enemy m = (Enemy)en;
                    m.Update(target,gameTime);
                }


            }
        }

        /// <summary>
        /// Renders all Entities
        /// </summary>
        public void Render()
        {
            for (int z = 0; z < m_objects.Count; z++)
            {
                if (m_objects[z] != null)
                    m_objects[z].Render();
            }
        }

        //Remove entity
        public void RemoveObject(GameObject entity)
        {
            entity.Dispose();
            m_objects.Remove(entity);
        }

        public bool ObjectExists(String name)
        {
            foreach (GameObject entity in m_objects)
            {
                if (entity.Name == name)
                {
                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        public GameObject GetObject(String name)
        {
            GameObject obj = null;

            foreach (GameObject entity in m_objects)
            {
                if (entity.Name == name)
                {
                    obj = entity;
                }
            }

            return obj;
        }

        /// <summary>
        /// Return all entities with specified name
        /// </summary>
        /// <param name="name">Name of the entities</param>
        /// <returns>List of the entities with specified name</returns>
        public List<GameObject> GetObjects(String name)
        {
            List<GameObject> objs = new List<GameObject>();

            foreach (GameObject entity in m_objects)
            {
                if (entity.Name == name)
                {
                    objs.Add(entity);
                }
            }
            if (objs.Count > 0)
                return objs;
            else
                throw new Exception("Entity with specified name not found");

        }

        public void RemoveObjects(String name)
        {
            List<GameObject> objs = new List<GameObject>();

            foreach (GameObject entity in m_objects)
            {
                if (entity.Name == name)
                {
                    objs.Add(entity);
                }
            }
            if (objs.Count > 0)
            {
                foreach(GameObject gameobject in objs)
                {
                    gameobject.Dispose();
                    m_objects.Remove(gameobject);
                }
            }
        

        }
    }
}
