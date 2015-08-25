using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    

    class Camera
    {
        // Set field of view of the camera in radians (pi/4 is 45 degrees).
        static float viewAngle = MathHelper.PiOver4;

        // Set distance from the camera of the near and far clipping planes.
        //static float nearClip = 5.0f;
        static float nearClip = 1.0f;
        static float farClip = 2000.0f;

        bool cameraStateKeyDown;

        int cameraState=2;

        Player player;

        Matrix view;
        Matrix proj;        

        public Camera(Player player)
        {
            this.player = player;
        }

        public int getCameraState()
        {
            return cameraState;
        }
        public Matrix getView()
        {
            return view;
        }
        public Matrix getProj()
        {
            return proj;
        }

        public void GetCurrentCamera()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Toggle the state of the camera.
            if (keyboardState.IsKeyDown(Keys.Tab) || (currentState.Buttons.LeftShoulder == ButtonState.Pressed))
            {
                cameraStateKeyDown = true;
            }
            else if (cameraStateKeyDown == true)
            {
                cameraStateKeyDown = false;
                cameraState += 1;
                cameraState %= 3;
            }
            
        }

        public void UpdateCamera(Viewport viewport)
        {
            // Calculate the camera's current position.
            Vector3 cameraPosition = player.avatarPosition;

            Matrix rotationMatrix = Matrix.CreateRotationY(player.avatarYaw);

            // Create a vector pointing the direction the camera is facing.
            Vector3 transformedReference = Vector3.Transform(player.cameraReference, rotationMatrix);

            // Calculate the position the camera is looking at.
            Vector3 cameraLookat = cameraPosition + transformedReference;

            // Set up the view matrix and projection matrix.
            view = Matrix.CreateLookAt(cameraPosition, cameraLookat, new Vector3(0.0f, 1.0f, 0.0f));

            //Viewport viewport = scMan.GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;
            
            proj = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearClip, farClip);
        }


        public void UpdateCameraFirstPerson(Viewport viewport)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(player.avatarYaw);

            // Transform the head offset so the camera is positioned properly relative to the avatar.
            Vector3 headOffset = Vector3.Transform(player.avatarHeadOffset, rotationMatrix);

            // Calculate the camera's current position.
            Vector3 cameraPosition = player.avatarPosition + headOffset;

            // Create a vector pointing the direction the camera is facing.
            Vector3 transformedReference = Vector3.Transform(player.cameraReference, rotationMatrix);

            // Calculate the position the camera is looking at.
            Vector3 cameraLookat = transformedReference + cameraPosition;

            // Set up the view matrix and projection matrix.

            view = Matrix.CreateLookAt(cameraPosition, cameraLookat, new Vector3(0.0f, 1.0f, 0.0f));

            //Viewport viewport = scMan.GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;

            proj = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearClip, farClip);

        }

        public void UpdateCameraThirdPerson(Viewport viewport)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(player.avatarYaw);

            // Create a vector pointing the direction the camera is facing.
            Vector3 transformedReference = Vector3.Transform(player.thirdPersonReference, rotationMatrix);

            // Calculate the position the camera is looking from.
            Vector3 cameraPosition = transformedReference + player.avatarPosition;

            // Set up the view matrix and projection matrix.
            view = Matrix.CreateLookAt(cameraPosition, player.avatarPosition, new Vector3(0.0f, 1.0f, 0.0f));

            //Viewport viewport = scMan.GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;

            proj = Matrix.CreatePerspectiveFieldOfView(viewAngle, aspectRatio, nearClip, farClip);

        }
    }
}
