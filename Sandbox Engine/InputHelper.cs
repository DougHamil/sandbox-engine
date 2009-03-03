using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sandbox_Engine
{
    class InputHelper
    {
        //Fields
        //-Input
        private KeyboardState keyState;
        private KeyboardState oldKeyState;
        private GamePadState gamePadState;
        private GamePadState oldGamePadState;
        private MouseState oldMouseState;
        private MouseState mouseState;
        private Vector2 mouseVector;

        #region Properties
        public Vector2 MouseVector
        {
            get { return mouseVector; }
        }
        public MouseState MouseState
        {
            get { return mouseState; }
        }
        public MouseState OldMouseState
        {
            get { return oldMouseState; }
        }
        public KeyboardState KeyState
        {
            get { return keyState; }
        }
        public KeyboardState OldKeyState
        {
            get { return oldKeyState; }
        }
        public GamePadState GamePadState
        {
            get { return gamePadState; }
        }
        public GamePadState OldGamePadState
        {
            get { return oldGamePadState; }
        }
        #endregion

        //Constructor
        public InputHelper()
        {
        }

        /*
         * Public Methods
         */
        public bool getKeyPressed(Keys k)
        {
            return (keyState.IsKeyUp(k) && oldKeyState.IsKeyDown(k));
        }
        public bool getButtonPressed(Buttons b)
        {
            return (gamePadState.IsButtonUp(b) && oldGamePadState.IsButtonDown(b));
        }
        public bool getLeftMouseClicked()
        {
            return (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed);
        }
        public bool getRightMouseClicked()
        {
            return (mouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed);
        }
        /*
         * Game Methods
         */
        public void Update(GamePadState gs, KeyboardState ks, MouseState ms)
        {
            //Update States
            oldKeyState = keyState;
            keyState = ks;
            oldGamePadState = gamePadState;
            gamePadState = gs;
            oldMouseState = mouseState;
            mouseState = ms;
            mouseVector.X = (float)mouseState.X;
            mouseVector.Y = (float)mouseState.Y;
        }
    }
}
