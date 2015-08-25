using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagement.Entities
{
    static class EntityManager
    {
        //List of all entities in the game
        List<Entity> m_entities = new List<Entity>();

        int m_count;

        /// <summary>
        /// List of all entities in the game
        /// </summary>
        public List<Entity> Entities
        {
            get { return m_entities; }
            set { m_entities = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityManager()
        {
            m_count = 0;
        }

        /// <summary>
        /// Adds entity to entity list
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        public void AddEntity(Entity entity)
        {
            m_entities.Add(entity);
            m_count++;
        }

        public void RemoveAllEnemies()
        {
            for (int z = 0; z < m_entities.Count; z++)
            {
                m_entities[z].Dispose();
                m_entities.RemoveAt(z);
                m_count--;
            }
            // m_entities.Clear();
        }

        /// <summary>
        /// Updates all entities
        /// </summary>
        public void Update()
        {
            /*
			foreach(GameObject entity in m_entities)
				entity.UpdateEntity();
             * */

            for (int z = 0; z < m_count; z++)
            {
                if (m_entities[z] != null)
                    m_entities[z].Update();
            }
        }

        /// <summary>
        /// Renders all Entities
        /// </summary>
        public void Render()
        {
            for (int z = 0; z < m_count; z++)
            {
                if (m_entities[z] != null)
                    m_entities[z].Render();
            }
        }

        //Remove entity
        public void RemoveEntity(Entity entity)
        {
            entity.Dispose();
            m_entities.Remove(entity);


        }

        public bool EntityExists(String name)
        {
            foreach (Entity entity in m_entities)
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

        /// <summary>
        /// Return all entities with specified name
        /// </summary>
        /// <param name="name">Name of the entities</param>
        /// <returns>List of the entities with specified name</returns>
        public List<Entity> GetObjects(String name)
        {
            List<Entity> objs = null;

            foreach (Entity entity in m_entities)
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
    }
}
