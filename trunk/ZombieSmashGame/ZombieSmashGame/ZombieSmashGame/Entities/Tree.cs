using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieSmashGame.Util;
using Microsoft.Xna.Framework;

namespace ZombieSmashGame.Entities
{
    class Tree : GameObject
    {
        public Tree()
            : base()
        {
        }

        public override void Update()
        {

        }

        public override void Render()
        {
            Utils.DrawModel(m_model, Matrix.Identity * Matrix.CreateScale(2.5f) *
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), 3.14f)) *
                    Matrix.CreateTranslation(m_position));

        }

        public void setBoundingBox(){
            m_box = Utils.GetBoundingBoxFromModel(m_model);
            m_box = Utils.scaleBoundingBox(m_box, 2.5f);
            m_box = Utils.rotationBoundingBox(m_box, new Vector3(1, 0, 0), 3.14f);
            m_box = Utils.translateBoundingBox(m_box, m_position);
        }
    }
}
