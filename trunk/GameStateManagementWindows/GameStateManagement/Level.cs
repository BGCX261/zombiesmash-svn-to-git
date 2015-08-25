using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    class Level
    {
        int level;
        Camera camera;
        Model model;
        Model model2;
        BoundingBox completeLevelBox;
        BoundingBox[] boundingBuilding = new BoundingBox[8];
        BoundingSphere[] boundingEnemy = new BoundingSphere[10];
        Enemy[] enemy = new Enemy[10];
        

        public Level(int i,Camera c, Model m, Model m2,Model e)
        {
            level = i;
            camera = c;
            model = m;
            model2 = m2;

            for (int j = 0; j < enemy.Length; j++)
            {
                
                Vector3 vec = new Vector3(4*j, -3.7f, 4*j+50);
                enemy[j] = new Enemy(e, vec);
                enemy[j].setLevel(this);
            }
            
        }

        public void SetUpBoundingBoxes()
        {
            completeLevelBox = this.getBoundingFromPoints(new Vector3(-190, 30, 490), new Vector3(105, -60, -10));


            
            boundingBuilding[0] = this.getBoundingFromPoints(new Vector3(88, 30, 19), new Vector3(48, -30, -2));
            boundingBuilding[1] = this.getBoundingFromPoints(new Vector3(88, 30, 38), new Vector3(66, -30, 19));
            boundingBuilding[2] = this.getBoundingFromPoints(new Vector3(88, 30, 58), new Vector3(48, -30, 38));
            boundingBuilding[3] = this.getBoundingFromPoints(new Vector3(88, 30, 78), new Vector3(66, -30, 58));
            boundingBuilding[4] = this.getBoundingFromPoints(new Vector3(88, 30, 98), new Vector3(48, -30, 78));

            //boundingEnemy[0] = enemy[].getEnemyBounding();
        }

        public BoundingBox getBoundingFromPoints(Vector3 v1, Vector3 v2)
        {
            Vector3[] boundaryPoints = new Vector3[2];
            boundaryPoints[0] = v1;
            boundaryPoints[1] = v2;

            
            return BoundingBox.CreateFromPoints(boundaryPoints);             
        }

        public BoundingBox getBounding()
        {
            return completeLevelBox;
        }
        public BoundingBox[] getBoundingBuildings()
        {
            return boundingBuilding;
        }
        public BoundingSphere[] getBoundingEnemy()
        {
            for (int j = 0; j < enemy.Length; j++)
            {
                boundingEnemy[j] = enemy[j].getEnemyBounding();
            }
            
            
            return boundingEnemy;
        }

        public void updateLevel()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].UpdateEnemyPosition();
            }
            
        }

        public void drawLevel(){

            if (level == 0)
            {
                DrawModel(model, Matrix.Identity * Matrix.CreateScale(10.0f) *
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
                    Matrix.CreateTranslation(0, -6, 0));

                DrawModel(model2, Matrix.Identity * Matrix.CreateScale(2.0f) *                        
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -1.57f)) *
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -3.14f)) *
                    Matrix.CreateTranslation(70, -4, 60));


                for (int i = 0; i < enemy.Length; i++)
                {
                    DrawModel(enemy[i].getModel(), Matrix.Identity * Matrix.CreateScale(0.2f) *
                    Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 3.14f)) *
                    Matrix.CreateRotationY(enemy[i].getEnemyYaw()) *
                    Matrix.CreateTranslation(enemy[i].getEnemyPosition()));
                }
                
            }
        }

        public void DrawModel(Model mod, Matrix world)
        {
            Vector3[] vec = null;
            foreach (ModelMesh mesh in mod.Meshes)
            {
               
                foreach(ModelMeshPart part in mesh.MeshParts)
                {
                    part.IndexBuffer.GetData(vec);
                }
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.Projection = camera.getProj();
                    be.View = camera.getView();
                    be.World = world;
                    be.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }

        
    }
}
