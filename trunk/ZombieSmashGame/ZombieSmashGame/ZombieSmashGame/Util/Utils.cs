using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SkinnedModel;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ZombieSmashGame.Util
{
    class Utils
    {

        public enum CollisionType { None, Building, Boundary, Target, Enemy }

        private static Random m_random = new Random();

        public static Random Random
        {
            get { return Utils.m_random; }
        }

        public static void DrawModel(Model mod, Matrix world)
        {
            //Vector3[] vec = null;
            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.Projection = GameCore.Camera.getProj();
                    be.View = GameCore.Camera.getView();
                    be.World = world;
                    be.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }

        public static void DrawModelAnim(Model mod, Matrix world, AnimationPlayer anim)
        {
            
            Matrix[] bones = anim.GetSkinTransforms();
            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.SetBoneTransforms(bones);

                    effect.View = GameCore.Camera.getView();
                    effect.Projection = GameCore.Camera.getProj(); 

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }

                mesh.Draw();
            }
        }


        

        public static float ComputeYaw(Vector3 origin, Vector3 target)
        {
            return MathHelper.ToDegrees((float)Math.Atan2(origin.X - target.X, origin.Z - target.Z));
        }

        /// <summary>
        /// Calculates the angle that an object should face, given its position, its
        /// target's position, its current angle, and its maximum turning speed.
        /// </summary>
        public static float TurnToFace(Vector3 position, Vector3 faceThis,
                                       float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         B
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // A--------
            //     x
            // 
            // where A is the position of the object, B is the position of the target,
            // and "o" is the angle that the object should be facing in order to
            // point at the target. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            Vector3 dif = position - faceThis;


            // we'll use the Atan2 function. Atan will calculates the arc tangent of
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)(Math.Atan2(dif.X, dif.Z) * 180.0f / Math.PI);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle - difference);
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// <param name="radians">the angle to wrap, in radians.</param>
        /// <returns>the input value expressed in radians from -Pi to Pi.</returns>
        /// </summary>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        //vytvoreni bboxu
        public static BoundingBox GetBoundingBoxFromModel(Model model)
        {
            BoundingBox boundingBox = new BoundingBox();


            foreach (ModelMesh mesh in model.Meshes)
            {
                ModelMeshPart part = mesh.MeshParts[0];
               
                VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[part.NumVertices];
                part.VertexBuffer.GetData<VertexPositionNormalTexture>(vertices);

                Vector3[] vertexs = new Vector3[vertices.Length];

                for (int index = 0; index < vertexs.Length; index++)
                {
                    vertexs[index] = vertices[index].Position;
                }

                boundingBox = BoundingBox.CreateMerged(boundingBox,
                BoundingBox.CreateFromPoints(vertexs));
            }

            return boundingBox;
        }

        //transforamce bboxu
        public static BoundingBox scaleBoundingBox(BoundingBox box, float scale)
        {
            Vector3 min, max;
            min = box.Min;
            max = box.Max;
            min = Vector3.Transform(box.Min, Matrix.CreateScale(scale));
            max = Vector3.Transform(box.Max, Matrix.CreateScale(scale));
            return new BoundingBox(min, max);
        }
        public static BoundingBox rotationBoundingBox(BoundingBox box, Vector3 axis, float uhel)
        {
            Vector3 min, max;
            min = box.Min;
            max = box.Max;
            min = Vector3.Transform(box.Min, Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(axis, uhel)));
            max = Vector3.Transform(box.Max, Matrix.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(axis, uhel)));
            return new BoundingBox(min, max);
        }
        public static BoundingBox translateBoundingBox(BoundingBox box, Vector3 position)
        {
            Vector3 min, max;
            min = box.Min;
            max = box.Max;
            min = Vector3.Transform(box.Min, Matrix.CreateTranslation(position));
            max = Vector3.Transform(box.Max, Matrix.CreateTranslation(position));
            return new BoundingBox(min, max);
        }

        
    }
}
