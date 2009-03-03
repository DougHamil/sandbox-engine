using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sandbox_Engine.GUI
{
    class GUIObject
    {
        //Fields
        //-Interaction
        protected string id;
        protected string command;
        protected bool isClicked;
        protected bool isHovered;
        protected Rectangle rect;
        protected SpriteBatch spriteBatch;
        protected SpriteFont spriteFont;
        protected InputHelper inputHelper;

        #region Properties
        public string Command
        {
            get { return command; }
        }
        public bool IsClicked
        {
            get { return isClicked; }
        }
        public bool IsHovered
        {
            get { return isHovered; }
        }
        public Rectangle Rectangle
        {
            get { return rect; }
        }
        public string ID
        {
            get { return id; }
        }
        #endregion
        //Constructor
        public GUIObject(Game game, string newID, string command)
        {
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            spriteFont = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            inputHelper = (InputHelper)game.Services.GetService(typeof(InputHelper));
            id = newID;
            isClicked = false;
            isHovered = false;
            this.command = command;
            rect = new Rectangle();
        }

        /*
         * Public Methods
         */
        public virtual void Draw(GameTime gameTime)
        {
        }
        public virtual void Update(GameTime gameTime)
        {
            if (rect.Contains(inputHelper.MouseState.X, inputHelper.MouseState.Y))
            {
                isHovered = true;
                if (inputHelper.getLeftMouseClicked())
                {
                    isClicked = true;
                }
                else
                {
                    isClicked = false;
                }
            }
            else
            {
                isClicked = false;
                isHovered = false;
            }
        }

    }
}
