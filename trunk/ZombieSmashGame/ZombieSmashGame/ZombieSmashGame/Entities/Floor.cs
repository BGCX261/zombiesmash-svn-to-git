using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieSmashGame.Util;
using Microsoft.Xna.Framework;

namespace ZombieSmashGame.Entities
{
    class Floor : GameObject
    {
        public Floor()
            : base()
        {}

        public override void Render()
        {
            Utils.DrawModel(m_model, Matrix.Identity * Matrix.CreateScale(15.0f) *
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -1.57f)) *
                    Matrix.CreateTranslation(350, -6, 0));
        }
    }
}
