using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZombieSmashGame.Util;

namespace ZombieSmashGame.Entities
{
    public class GameObject : IDisposable
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
        protected Vector3 m_position;

        public Vector3 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        protected Model m_model; // m_model kterej se ma pouzit

        public Model Model
        {
            get { return m_model; }
            set { m_model = value; }
        }

      
        protected BoundingSphere m_sphere; // toto ziska automaticky
        protected BoundingBox m_box; // pro budovy lampy a stromy ale potrebujem toto
        
        public BoundingBox BoundingBox
        {
            get { return m_box; }
            set { m_box = value; }
        }

        public GameObject()
        {
            id = GameCore.Unique_ID; // this will be uniq for every entity -> it helps to identify it
            name = this.ToString(); // name corresponds to a collection of object, so in the EntityManager we can update all Enemies,
            //Players or everything what is an entity
        }

        public virtual void Update()
        {
 
        }

        public virtual void Render()
        {

        }


        public void Dispose()
        {
            m_model = null;
        }
    
    }
}
